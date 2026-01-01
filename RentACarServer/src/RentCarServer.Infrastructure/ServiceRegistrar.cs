using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using GenericRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RentACarServer.Application.Services;
using RentACarServer.Infrastructure.Context;
using RentACarServer.Infrastructure.Services;
using RentACarServer.Infrastructure.Context;
using RentACarServer.Infrastructure.Options;
using Scrutor;

namespace RentACarServer.Infrastructure
{
    public static class ServiceRegistrar
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<JwtOptions>(configuration.GetSection("Jwt"));
            services.ConfigureOptions<JwtSetupOptions>();
            services.Configure<SmtpSettings>(
                configuration.GetSection("SmtpSettings")
            );

            services.AddAuthentication().AddJwtBearer();
            services.AddAuthorization();
            services.AddHttpContextAccessor();

            services.AddFluentEmail("info@rentcar.com", "Rent A Car")
                .AddSmtpSender(() =>
                {
                    var smtpSettings = configuration
                    .GetSection("SmtpSettings")
                    .Get<SmtpSettings>();

                    var smtpClient = new SmtpClient(smtpSettings.Host, smtpSettings.Port)
                    {
                        Credentials = new NetworkCredential(
                            smtpSettings.UserName,
                            smtpSettings.Password
                        ),
                        EnableSsl = smtpSettings.EnableSsl,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Timeout = 10000
                    };
                    services.AddFluentEmail(
                            smtpSettings.FromEmail,
                            smtpSettings.FromName
                        )
                        .AddSmtpSender(smtpClient);
                    return smtpClient;
                });
            services.AddDbContext<ApplicationDbContext>(opt =>
            {
                var connectionString =
                    configuration.GetConnectionString("SqlServer") ??
                    configuration.GetConnectionString("DefaultConnection") ??
                    throw new InvalidOperationException("Database connection string is missing.");

                opt.UseSqlServer(connectionString);
            });
            services.AddScoped<IUnitOfWork>(srv => srv.GetRequiredService<ApplicationDbContext>());
            services.Scan(action => action.FromAssemblies(typeof(ServiceRegistrar).Assembly)
                .AddClasses(publicOnly: false)
                .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                .AsImplementedInterfaces().AsMatchingInterface().WithScopedLifetime());

            return services;
        }
    }
}
