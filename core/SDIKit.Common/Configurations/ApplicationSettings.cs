using System.Collections.Generic;

namespace SDIKit.Common.Configurations
{
    public class ApplicationSettings
    {
        public string Name { get; set; }
        public string Secret { get; set; }
        public string DashboardApiAppUrl { get; set; }
        public string PublicApiAppUrl { get; set; }
        public string DashboardWebAppUrl { get; set; }
        public string PublicWebAppUrl { get; set; }
        public string[] AllowedOrigins { get; set; }
        public string ProductImageFilePath { get; set; }
        public string ProfileImageFilePath { get; set; }
    }
}