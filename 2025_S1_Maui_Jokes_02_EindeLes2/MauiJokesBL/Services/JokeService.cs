using HoGentMauiBL.Interfaces;

namespace HoGentMauiBL.Services
{
    public class JokeService(IJokeRespository jokeRespository)
    {
        private IJokeRespository JokeRespository { get; } = jokeRespository;

        private string? _previousJoke = null;

        public string? GetRandomJoke()
        {
            var jokeCount = JokeRespository.GetCount();
            
            if (jokeCount == 0)
                return null;

            if (jokeCount == 1)
                return JokeRespository.Get(0);

            string? joke = null;
            do
            {
                joke = JokeRespository.Get(Random.Shared.Next(jokeCount));
            }
            while (joke == _previousJoke); // Making sure the joke is different than the previous one

            // Keeping track of the joke, so we can select a different one next time. (We keep track of the joke and not the index, so that when jokes get added, or resorted, it still keeps track)
            // And return the joke
            return _previousJoke = joke;
        }

        public void AddJoke(string joke)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(joke);

            if (!JokeRespository.Exists(joke))
                JokeRespository.Add(joke);
        }

        public bool DeleteJoke(string joke)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(joke);

            if (JokeRespository.Exists(joke))
                return JokeRespository.Delete(joke);

            return false;
        }
    }
}
