using System.Threading.Tasks;

namespace SmartDotNet.ApsNet.Identity.Interfaces
{
    public interface IClientApplicationStore<TClientApplication> where TClientApplication : IClientApplication
    {
        Task<TClientApplication> GetClientApplicationAsync(string id);
    }
}
