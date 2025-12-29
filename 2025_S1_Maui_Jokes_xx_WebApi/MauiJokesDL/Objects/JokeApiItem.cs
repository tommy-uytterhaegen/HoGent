using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiJokesDL.Objects
{
    // Can be generated from the resulting json: https://json2csharp.com/. Make sure that the supplied example json contains all fields needed
    public class JokeApiItem
    {
        public bool error { get; set; }
        public string category { get; set; }
        public string type { get; set; }
        public string joke { get; set; }
        public JokeApiItemFlags flags { get; set; }
        public int id { get; set; }
        public bool safe { get; set; }
        public string lang { get; set; }
    }
}
