using Microsoft.AspNetCore.Mvc;
using server.Dtos.User;
using server.Responses.User;
using server.UseCases.Auth;

namespace Server.Controllers;

public class AuthController(
    CreateUser createUser,
    LoginUser loginUser,
    ConfirmEmail confirmEmail,
    ForgotPassword forgotPassword,
    ResetPassword resetPassword
) : ApiControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult<UserResponse>> Register(CreateUserDto createUserDto)
    {
        UserResponse response = await createUser.Perform(createUserDto);

        return Ok(response);
    }
    
    [HttpPost("login")]
    public async Task<ActionResult<UserResponse>> Login(LoginUserDto userDto)
    {
        UserResponse response = await loginUser.Perform(userDto);
    
        return Ok(response);
    }

    [HttpGet("confirm-email/{userId}/{token}")]
    public async Task<ActionResult<UserResponse>> ConfirmEmail(int userId, string token)
    {
        UserResponse response = await confirmEmail.Perform(userId, token);
    
        return Ok(response);
    }
    
    [HttpPost("forgot-password")]
    public async Task<ActionResult<UserResponse>> ForgotPassword(string email)
    {
        UserResponse response = await forgotPassword.Perform(email);
    
        return Ok(response);
    }
    
    [HttpPost("reset-password")]
    public async Task<ActionResult<UserResponse>> ResetPassword(ResetUserPasswordDto userDto)
    {
        UserResponse response = await resetPassword.Perform(userDto);
    
        return Ok(response);
    }
}