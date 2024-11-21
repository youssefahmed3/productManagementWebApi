using Microsoft.AspNetCore.Mvc;
using NotesApi.Models;
using ProductManagement.Dtos;

namespace ProductManagement.Interfaces;

public interface IAuthRepository {
    public Task<AuthResult> Register(UserRegisterDto userToRegister);
    public Task<AuthResult> Login(UserLoginDto userToLogin);
}