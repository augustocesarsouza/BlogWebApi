using static System.Net.WebRequestMethods;

namespace BlogWeb
{
    public static class Configuration
    {
        public static string JwtKey = "N0c4bDQyS0k5VTV6Ujl0bWI1aEtRQ2E3ZTc2ZDI2";
        public static string ApiKeyName = "api_key";
        public static string ApiKey = "curso_api_8d6a8cd79e29a224afd97e6652d758aac4348cf6";
        public static string ImagemUrl = "https://localhost:0000/images/";
        public static SmtpConfiguration Smtp = new();

        public class SmtpConfiguration 
        {
            public string Host { get; set; } = string.Empty;
            public int Port { get; set; } = 25;
            public string UserName { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }
    }
}
