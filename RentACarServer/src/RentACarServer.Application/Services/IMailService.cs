using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarServer.Application.Services
{
    public interface IMailService
    {
        Task SendAsync(string to, string subject, string body, CancellationToken cancellationToken = default);
    }
}
