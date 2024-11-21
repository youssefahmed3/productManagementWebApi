using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NotesApi.Models;
using ProductManagement.Dtos;
using ProductManagement.Helpers;
using ProductManagement.Interfaces;
using ProductManagement.Models;

namespace ProductManagement.Repositories
{
    public class AuthRepository : IAuthRepository
    {

        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;
        private readonly AuthHelper _authHelper;
        public AuthRepository(UserManager<User> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
            _authHelper = new AuthHelper(config);
        }
        public async Task<AuthResult> Register(UserRegisterDto userToRegister)
        {
            var userExists = await _userManager.FindByEmailAsync(userToRegister.Email);
            if (userExists != null)
            {
                return new AuthResult()
                {
                    Result = false,
                    Errors = new List<string> { "Email already exists" }
                };
            }

            var newUser = new User()
            {
                Email = userToRegister.Email,
                UserName = userToRegister.Email,
            };

            var isCreated = await _userManager.CreateAsync(newUser, userToRegister.Password);

            if (isCreated.Succeeded)
            {
                string token = _authHelper.GenerateJwtToken(newUser);
                return new AuthResult()
                {
                    Result = true,
                    Token = token
                };
            }

            var errors = isCreated.Errors.Select(e => e.Description).ToList();
            return new AuthResult()
            {
                Result = false,
                Errors = errors
            };
        }

        public async Task<AuthResult> Login(UserLoginDto userToLogin)
        {
            var userExists = await _userManager.FindByEmailAsync(userToLogin.Email);
            if (userExists == null)
            {
                return new AuthResult()
                {
                    Result = false,
                    Errors = new List<string> { "User does not exist" }
                };
            }

            bool isUserCorrect = await _userManager.CheckPasswordAsync(userExists, userToLogin.Password);
            if (!isUserCorrect)
            {
                return new AuthResult()
                {
                    Result = false,
                    Errors = new List<string> { "Invalid Credentials" }
                };
            }

            string token = _authHelper.GenerateJwtToken(userExists);
            return new AuthResult()
            {
                Result = true,
                Token = token
            };
        }
    }
}