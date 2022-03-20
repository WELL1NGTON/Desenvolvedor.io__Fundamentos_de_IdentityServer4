using IdentityModel.Client;

using Newtonsoft.Json.Linq;

using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Parceiro.BackgroundService
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new HttpClient();

            var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }

            // request token
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "195ba83285124a92b6dd73e0c15f11c3",
                ClientSecret = "64ef900581c74546b340000677625601",
                Scope = "api_frete.read_only"
            });

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine("\n\n");

            // call api
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            var response = await apiClient.GetAsync($"https://localhost:5007/fretes/para/{-23.3489854d},{-51.1484878}/calcular?altura={9.6d}&largura={0.83d}&comprimento={31.09d}&peso={3}");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }

            Console.ReadKey();
        }

    }
}
