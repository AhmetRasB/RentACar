using GenericRepository;
using RentACarServer.Domain.Users;
using RentACarServer.Domain.Users.ValueObject;

namespace RentACarServer.WebAPI;

public static class ExtensionMethods
{
    public static async Task CreateFirstUser(this WebApplication app)
    {
        using var scoped = app.Services.CreateScope();
        var userRepository = scoped.ServiceProvider.GetRequiredService<IUserRepository>();
        var unitOfWork = scoped.ServiceProvider.GetRequiredService<IUnitOfWork>();

        if (!(await userRepository.AnyAsync(p => p.UserName.username == "admin")))
        {
            FirstName firstName = new FirstName("Ahmet");
            LastName lastName = new LastName("Beyhan");
            Email email = new("ahmetrasimbayhan@gmail.com");
            UserName userName = new UserName("admin");
            Password password = new Password("1");

            var user = new User(firstName = firstName,
                lastName = lastName,
                email = email,
                userName = userName,
                password = password);

            userRepository.Add(user);
            await unitOfWork.SaveChangesAsync();

        }
    }
}