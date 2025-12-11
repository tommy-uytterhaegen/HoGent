using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiJokesBL.Model
{
    public class Joke(string text)
    {
        public string Text { get; set; } = text;

        public override string ToString()
        {
            return Text;
        }
    }
}
