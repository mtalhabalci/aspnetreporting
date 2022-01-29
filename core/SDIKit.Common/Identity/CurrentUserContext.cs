using System;

namespace SDIKit.Common.Identity
{
    public class CurrentUserContext : ICurrentUserContext
    {
        public long Id { get; set; }
        public DateTime LoginDate { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Fullname { get; set; }
    }
}