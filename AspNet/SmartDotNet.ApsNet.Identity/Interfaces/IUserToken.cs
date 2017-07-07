using System;

namespace SmartDotNet.ApsNet.Identity.Interfaces
{
    public interface IUserToken<TUserKey> where TUserKey : IEquatable<TUserKey>
    {
        string Value { get; set; }

        TUserKey UserId { get; set; }

        DateTime IssuedUtc { get; set; }

        DateTime ExpiresUtc { get; set; }

        string ProtectedTicket { get; set; }
    }
}
