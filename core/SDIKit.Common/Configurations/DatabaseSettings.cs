namespace SDIKit.Common
{
    public class DatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DefaultScheme { get; set; }
        public int? CommandTimeout { get; set; }
        public bool EnableAutoHistory { get; set; }
    }
}