using HoGentMauiBL.Interfaces;
using MauiJokesBL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiJokesBL.Interfaces
{
    public interface IJokeService
    {
        public string? GetRandomJoke();

        public void AddJoke(string joke);

        public bool DeleteJoke(string joke);

        public IEnumerable<Joke> GetAll();
    }
}
