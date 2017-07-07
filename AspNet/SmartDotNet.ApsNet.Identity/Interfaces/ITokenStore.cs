using System;
using System.Threading.Tasks;

namespace SmartDotNet.ApsNet.Identity.Interfaces
{
    public interface ITokenStore<TToken, TUserKey> where TToken : IUserToken<TUserKey> 
        where TUserKey : IEquatable<TUserKey>
    {
        Task AddRefreshTokenAsync(TToken token);

        Task RemoveRefreshTokenAsync(TToken token);

        Task<TToken> GetTokenAsync(string value);
    }
}
