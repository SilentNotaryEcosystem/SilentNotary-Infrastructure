using System.Threading.Tasks;

namespace SmartDotNet.ApsNet.Identity.Interfaces
{
    public interface IExternalPreRegistrationStore<TPreRegistration> where TPreRegistration: IExternalPreRegistration
    {
        Task AddPreRegistrationAsync(TPreRegistration preRegistration);
        Task<TPreRegistration> GetPreRegistrationAsync(string provider, string sid);
    }
}
