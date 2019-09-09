

namespace Security.Configuration
{
    public class AccessTokenSettings
    {
        public string SecretKey { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public int ExpireIn { get; set; }
    }
}
