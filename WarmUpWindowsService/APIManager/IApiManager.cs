#region

using System.Threading.Tasks;

#endregion

namespace WarmUpWindowsService
{
    public interface IApiManager
    {
        Task<T> GetAsync<T>(string uri,string accessToken);
        
    }
}