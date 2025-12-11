using HoGentMaui.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoGentMaui.ViewModels
{
    public class DetailJokeViewModel 
        : ViewModel, 
          IQueryAttributable // We willen de parameters die gegeven waren wanneer we naar de pagina navigeerde die hoort bij deze ViewModel
    {
        private string _jokeText;
        public string JokeText
        {
            get => _jokeText;
            set
            {
                _jokeText = value;

                OnPropertyChanged();
            }
        }

        // Deze methode wordt opgeroepen als er parameters waren voor de pagina dewelke bij deze viewmodel hoort
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            foreach (var p in query)
            {
                if ( p.Key == "JokeText")
                {
                    if ( p.Value is string s)
                        JokeText = s;
                }
            }
        }
    }
}
