using RentACarServer.Domain.Users;

namespace RentACarServer.Application.Services;

public interface IJwtProvider
{
    string CreateToken(User user);
}