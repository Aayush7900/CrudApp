using App.Application.DTO;
using App.Application.Interfaces;
using App.Core.Entities;
using App.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace App.Infrastructure.Repo {
    public class UserRepo : IUser {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IConfiguration configuration;

        public UserRepo(ApplicationDbContext applicationDbContext, IConfiguration configuration) {
            this.applicationDbContext = applicationDbContext;
            this.configuration = configuration;
        }
        public async Task<LoginResponse> LoginUserAsync(LoginDTO loginDTO) {
            var getUser = await FindUserByEmail(loginDTO.Email!);
            if (getUser == null) {
                return new LoginResponse(false, "User not found");
            }
            bool checkPassword = BCrypt.Net.BCrypt.Verify(loginDTO.Password, getUser.Password);
            if (checkPassword) {
                return new LoginResponse(true, "Login Successful", GenerateJWTToken(getUser));
            }
            else {
                return new LoginResponse(false, "Invalid credentials");
            }
        }

        private string GenerateJWTToken(User user) {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[] {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name!),
                new Claim(ClaimTypes.Email, user.Email!)
            };
            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: userClaims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<User> FindUserByEmail(string email) {
            return await applicationDbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<RegistrationResponse> RegisterUserAsync(RegisterUserDTO registerUserDTO) {
            var getUser = await FindUserByEmail(registerUserDTO.Email!);
            if (getUser != null) {
                return new RegistrationResponse(false, "User already exists");
            }
            applicationDbContext.Users.Add(new User() {
                Name = registerUserDTO.Name,
                Email = registerUserDTO.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(registerUserDTO.Password)

            });
            await applicationDbContext.SaveChangesAsync();
            return new RegistrationResponse(true, "Registration Successful");






        }
    }
}
