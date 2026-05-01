using Application.Interfaces;
using Domain.Entities;

namespace Application.UseCases.Auth;

public class LoginHandler
{
    private readonly IUserRepository _userRepository;

    public LoginHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> Handle(LoginRequest request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user == null)
            return null;

        // VALIDACIÓN de contraseña usando BCrypt
        bool isValid = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);

        if (!isValid)
            return null;

        return user;
    }
}