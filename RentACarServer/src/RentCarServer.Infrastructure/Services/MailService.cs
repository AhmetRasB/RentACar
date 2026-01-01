using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentEmail.Core;
using FluentEmail.Core.Models;
using Microsoft.Extensions.Options;
using RentACarServer.Application.Services;
using RentACarServer.Infrastructure.Options;

namespace RentACarServer.Infrastructure.Services
{
    internal sealed class MailService(IFluentEmail fluentEmail, IOptions<SmtpSettings> smtpOptions) : IMailService
    {
        public async Task SendAsync(string to, string subject, string body, CancellationToken cancellationToken)
        {
            var settings = smtpOptions.Value;
            var sendResponse = await fluentEmail
                .SetFrom(settings.FromEmail, settings.FromName)
                .To(to)
                .Subject(subject)
                .Body(body, isHtml: true)
                .SendAsync(cancellationToken);
            if (!sendResponse.Successful)
            {
                throw new ArgumentException(string.Join(", ", sendResponse.ErrorMessages));
            }
        }
    }
}
