namespace Assignment.Repository.Common
{
    public class AppSettingsConfiguration
    {
        public string Secret { get; set; }
        public int RefreshTokenTTL { get; set; }
    }
}
