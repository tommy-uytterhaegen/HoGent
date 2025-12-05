using HoGentMauiBL.Interfaces;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiJokesDL
{
    public class LiteDbJokeRepository : IJokeRespository
    {
        public class Joke
        {
            public string Text { get; init; }

            public Joke() { }

            public Joke(string joke)
            {
                Text = joke;
            }
        }

        private Lazy<ILiteDatabase> _database = new Lazy<ILiteDatabase>(() => new LiteDatabase("jokes.litedb"));
        private ILiteCollection<Joke> _jokes;
        private ILiteCollection<Joke> Jokes 
            => _jokes ??= _database.Value.GetCollection<Joke>();
        public void Add(string jokeText)
        {
            var joke = new Joke(jokeText);

            Jokes.Insert(joke);
        }

        public bool Exists(string jokeText)
        {
            return Jokes.Exists(x => x.Text == jokeText);
        }

        public string Get(int jokeIndex)
        {
            var joke = Jokes.Query()
                            .OrderBy(x => x.Text)
                            .Skip(jokeIndex)
                            .FirstOrDefault();

            return joke?.Text ?? string.Empty;
        }

        public int GetCount()
        {
            return Jokes.Count();
        }

        public bool Delete(string joke)
        {
            // We gebruiken de 'DeleteMany' ook al weten we dat we maar 1 zullen verwijderen omdat er een predicate gebruikt wordt. LiteDB heeft geen idee dat
            // dit zal resulteren in maar 1 deleted joke
            var deletionCount = Jokes.DeleteMany(x => x.Text == joke);
            if (deletionCount > 0)
                return true;

            return false;
        }
    }
}
