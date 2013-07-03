using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RottenTomato.Core
{
    public static class RottenTomatoApi
    {
        private const string ApiToken = "h524hezne2gmh29nf5waqtpw";
        private const string MovieListUrl = "http://api.rottentomatoes.com/api/public/v1.0/movies.json?apikey={0}&q={1}";

        public static async Task<RootObject> GetMovieList(string searchTerm)
        {
            var client = new HttpClient();
            var msg = await client.GetAsync(string.Format(MovieListUrl, ApiToken, searchTerm));
            if (msg.IsSuccessStatusCode)
            {
                using (var stream = await msg.Content.ReadAsStreamAsync())
                {
                    using (var streamReader = new StreamReader(stream))
                    {
                        var str = await streamReader.ReadToEndAsync();
                        var obj = await JsonConvert.DeserializeObjectAsync<RootObject>(str);
                        return obj;
                    }
                }
            }
            return null;
        }
    }
}
