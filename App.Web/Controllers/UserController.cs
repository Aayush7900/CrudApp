﻿using App.Application.DTO;
using App.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.Web.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase {
        private readonly IUser user;
        public UserController(IUser user) {
            this.user = user;
        }
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>>LogUserIn(LoginDTO loginDTO) {
            var result = await user.LoginUserAsync(loginDTO);
            return Ok(result);
        }
        [HttpPost("register")]
        public async Task<ActionResult<LoginResponse>> RegisterUser(RegisterUserDTO registerUserDTO) {
            var result = await user.RegisterUserAsync(registerUserDTO);
            return Ok(result);
        }
    }
}
