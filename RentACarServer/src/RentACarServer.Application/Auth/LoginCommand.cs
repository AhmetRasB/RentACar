using FluentValidation;
using RentACarServer.Application.Services;
using RentACarServer.Domain.Users;
using TS.MediatR;
using TS.Result;

namespace RentACarServer.Application.Auth;

public sealed record LoginCommand
(
    string UserNameOrMail,
    string Password) : IRequest<Result<string>>;

public sealed class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(p=> p.UserNameOrMail).NotEmpty().WithMessage("Username or email is required");
        RuleFor(p=> p.Password).NotEmpty().WithMessage("Password is required");
    }
}
public sealed class LoginCommandHandler(
    IUserRepository userRepository, IJwtProvider jwtProvider) : IRequestHandler<LoginCommand, Result<string>>
{
    public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.FirstOrDefaultAsync(p =>
            p.Email.email == request.UserNameOrMail || p.UserName.username == request.UserNameOrMail);
        if (user is null)
        {
            return Result<string>.Failure("Username or password is false");
        }

        var checkpassword = user.VerifyPasswordHash(request.Password);
        if (!checkpassword)
        {
            return Result<string>.Failure("Username or password is false");

        }
        var token = jwtProvider.CreateToken(user);
        return token;
    }
}