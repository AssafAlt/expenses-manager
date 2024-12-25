using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Infrastructure.Auth
{
    public class JwtSettings
    {
        public string SigningKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpirationDays { get; set; }
    }
}
