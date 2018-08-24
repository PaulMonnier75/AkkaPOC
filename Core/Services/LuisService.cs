using System.Net.Http;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Services
{
    public interface ILuisService
    {
        Task<string> GetIntentFromMessage(string message);
    }

    public class LuisService : ILuisService
    {
        private readonly LuisSecrets LuisSecrets;
       

        public LuisService(LuisSecrets luisSecrets)
        {
            LuisSecrets = luisSecrets;
        }

        public async Task<string> GetIntentFromMessage(string message)
        {
            var query = $"{LuisSecrets.LuisEndpoint}/apps/{LuisSecrets.LuisAppId}/?q={message}";
            var client = new HttpClient();
            var subscriptionKey = LuisSecrets.LuisKey;

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

            var response = await client.GetAsync(query);
            var strResponseContent = await response.Content.ReadAsStringAsync();

            return strResponseContent;
        }
    }
}