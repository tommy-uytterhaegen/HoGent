using HoGentMauiBL.Interfaces;

namespace HoGentMauiDL
{
    public class JokeRepository : IJokeRespository
    {
        private List<string> _jokes;

        public JokeRepository()
        {
            _jokes = 
            [
                "Why was the math book sad? It had too many problems.", 
                "Why don’t programmers like nature? Too many bugs.",
                "I’m reading a book on anti-gravity; it’s impossible to put down.",
                "My code works… as long as nobody runs it."
            ];
        }

        public void Add(string joke)
        {
            _jokes.Add(joke);
        }

        public bool Exists(string joke)
        {
            return _jokes.Contains(joke);
        }

        public string Get(int jokeIndex)
        {
            if (0 <= jokeIndex && jokeIndex < GetCount())
                return _jokes[jokeIndex];

            throw new InvalidDataException($"No joke with index {jokeIndex}");
        }

        public int GetCount()
        {
            return _jokes.Count;
        }

        public bool Delete(string joke)
        {
            _jokes.Remove(joke);

            return true;
        }


    }
}
