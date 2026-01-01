using RentACarServer.Domain.Abstractions;
using RentACarServer.Domain.Users.ValueObject;

namespace RentACarServer.Domain.Users
{
    public sealed class User : Entity
    {
        public User(FirstName firstName, LastName lastName, Email email, UserName userName, Password password)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            UserName = userName;
            Password = password;
            FullName = new FullName($"{firstName.name} {lastName.lastname}");
        }
        private User() { }
        public FirstName FirstName { get; private set; } = default!;
        public LastName LastName { get; private set; } = default!;
        public FullName FullName { get; private set; } = default!;
        public Email Email { get; private set; } = default!;
        public UserName UserName { get; private set; } = default!;
        public Password Password { get; private set; } = default!;
        public ForgotPasswordId? ForgotPasswordId { get; private set; }
        public ForgotPasswordCreatedAt? ForgotPasswordCreatedAt { get; private set; }
        public IsForgotPasswordCompleted? IsForgotPasswordCompleted { get; set; }
        public bool VerifyPasswordHash(string password)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512(Password.PasswordSalt);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(Password.PasswordHash);
        }

        public void CreateForgotPasswordId()
        {
            ForgotPasswordId = new ForgotPasswordId(Guid.CreateVersion7());
            ForgotPasswordCreatedAt = new ForgotPasswordCreatedAt(DateTime.UtcNow);
            IsForgotPasswordCompleted = new IsForgotPasswordCompleted(false);
        }
        
    }
}
