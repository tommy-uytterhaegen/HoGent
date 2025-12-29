using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoGentMaui.Interfaces
{
    public interface INavigationService
    {
        void GoToJokeDetail(string jokeText);
        public void GoBack();
        void ShowPopup(string title, string text);
    }
}
