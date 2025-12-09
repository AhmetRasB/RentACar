using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentACarServer.Domain.Users;

namespace RentACarServer.Infrastructure.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(i=>i.Id);
            builder.OwnsOne(i=>i.FirstName);
            builder.OwnsOne(i=>i.LastName);
            builder.OwnsOne(i=>i.FullName);
            builder.OwnsOne(i=>i.Email);
            builder.OwnsOne(i=>i.UserName);
            builder.OwnsOne(i=>i.Password);

        }
    }
}
