using HoGentMauiBL.Interfaces;
using MauiJokesBL.Model;
using MauiJokesDL.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiJokesDL
{
    public class WebApiJokeRepository : IJokeRespository
    {
        public void Add(string joke)
        {
            return;
        }

        public bool Delete(string joke)
        {
            return true;
        }

        public bool Exists(string joke)
        {
            return false;
        }

        public async Task<string> Get(int jokeIndex)
        {
            using (var httpClient = new HttpClient())
            {
                // Request the given url for a response, we need to call this in a async (https://learn.microsoft.com/en-us/dotnet/csharp/asynchronous-programming/) way
                var response = await httpClient.GetAsync("https://v2.jokeapi.dev/joke/Any?blacklistFlags=nsfw,racist,sexist&type=single");

                // Check if the call was succesfull
                if (response.IsSuccessStatusCode)
                {
                    // Read the response as a string, you could also read the response as a stream. The performance is better, but a bit harder to debug.
                    // I've added the code in comment when using streams
                    //var stringResponse = await response.Content.ReadAsStringAsync();
                    //if ( ! string.IsNullOrWhiteSpace(stringResponse))
                    //{
                    //    var apiItem = System.Text.Json.JsonSerializer.Deserialize<JokeApiItem>(stringResponse);
                    //    return apiItem.joke;
                    //}

                    using (var streamResponse = await response.Content.ReadAsStreamAsync())
                    {
                        var apiItem = System.Text.Json.JsonSerializer.Deserialize<JokeApiItem>(streamResponse);
                        return apiItem.joke;
                    }
                }

                return null;
            }
        }

        public IEnumerable<Joke> GetAll()
        {
            return new List<Joke>();
        }

        public int GetCount()
        {
            return 1;
        }
    }
}
