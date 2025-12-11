using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoGentMauiBL.Interfaces
{
    public interface IJokeRespository
    {
        void Add(string joke);
        bool Exists(string joke);
        string Get(int jokeIndex);
        int GetCount();
        bool Delete(string joke);
    }
}
