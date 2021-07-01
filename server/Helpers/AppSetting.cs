namespace server.Helpers
{
    public interface IAppSettings
    {
        public string Secret { get; set; }

        public string SendGridKey { get; set; }
    }
    public class AppSettings : IAppSettings
    {
        public string Secret { get; set; }

        public string SendGridKey { get; set; }
    }
}