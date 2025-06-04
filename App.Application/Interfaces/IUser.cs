using App.Application.DTO;

namespace App.Application.Interfaces {
    public interface IUser {
        Task<RegistrationResponse> RegisterUserAsync(RegisterUserDTO registrUserDTO);
        Task<LoginResponse> LoginUserAsync(LoginDTO loginDTO);
    }
}
