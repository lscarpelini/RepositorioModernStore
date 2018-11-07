using Microsoft.IdentityModel.Tokens;
using System;
using System.Threading.Tasks;

namespace ModernStore.Api.Security
{
    public class TokenOptions
    {
        //Quem esta pedindo o Token
        public string Issuer { get; set; }

        //Qualquer palavra chave para o Token
        public string Subject { get; set; }

        //Quem esta recebendo o Token
        public string Audience { get; set; }

        public DateTime NotBefore { get; set; } = DateTime.UtcNow;

        public DateTime IssuedAt { get; set; } = DateTime.UtcNow;

        public TimeSpan ValidFor { get; set; } = TimeSpan.FromDays(2);

        public DateTime Expiration => IssuedAt.Add(ValidFor);

        public Func<Task<string>> JtiGenerator =>
            () => Task.FromResult(Guid.NewGuid().ToString());

        public SigningCredentials SigningCredentials { get; set; }
    }
}
