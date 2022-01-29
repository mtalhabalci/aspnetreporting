namespace SDIKit.Common.Identity
{
    public static class SDIKitJwtClaimTypes
    {
        public const string CurrentUserId = "CurrentUserId";
        public const string FullName = "FullName";
        public const string Name = "Name";
        public const string Surname = "Surname";
        public const string MailAddress = "MailAddress";
        public const string IsPublicUser = "IsPublicUser";
        public const string IsDashboardUser = "IsDashboardUser";
        public const string IsSystemAdmin = "IsSystemAdmin";
        public const string IsTrustedUser = "IsTrustedUser";
        public const string MustChangePasswordNextLogon = "MustChangePasswordNextLogon";
        public const string LoginDate = "LoginDate";
    }
}