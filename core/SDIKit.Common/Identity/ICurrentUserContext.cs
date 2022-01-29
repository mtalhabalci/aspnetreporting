using System;

namespace SDIKit.Common.Identity
{
    public interface ICurrentUserContext
    {
        long Id { get; set; }
        DateTime LoginDate { get; set; }
        string Email { get; set; }
        string Name { get; set; }
        string Surname { get; set; }
        string Fullname { get; set; }
    }
}