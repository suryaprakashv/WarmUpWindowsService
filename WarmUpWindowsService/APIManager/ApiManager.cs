#region


using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

#endregion

namespace WarmUpWindowsService
{
    public class ApiManager : IApiManager
    {
        public async Task<T> GetAsync<T>(string uri,
            string accessToken)
        {

            using (var client = new HttpClient())
            {
                client.SetBearerToken(accessToken);

                var response = await client.GetAsync(uri);
                response.EnsureSuccessStatusCode();

                var result = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<T>(result);
            }
        }

    }
}