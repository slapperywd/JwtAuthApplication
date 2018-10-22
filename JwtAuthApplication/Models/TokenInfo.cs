using System;

namespace JwtAuthApplication.Models
{
    public class TokenInfo
    {
        public string Token { get; set; }

        public string UserName { get; set; }

        public DateTime ValidFrom { get; set; }

        public DateTime ValidTo { get; set; }
    }
}
