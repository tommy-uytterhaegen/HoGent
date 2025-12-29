using HoGentMaui.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoGentMaui.Services
{
    internal class NavigationService : INavigationService
    {
        public void GoToJokeDetail(string jokeText)
        {
            // We navigeren naar een route met naam 'DetailPage' (nameof(DetailPage), en we geven het een dictionary van parameters (dit kan bijna elk type zijn)
            GoTo(nameof(DetailPage), new ShellNavigationQueryParameters
            {
                { "JokeText", jokeText }
            });
        }

        private void GoTo(string routeName, ShellNavigationQueryParameters parameters)
        {
            Shell.Current.GoToAsync(routeName, parameters); 
        }

        public void GoBack()
        {
            // We 'poppen' de meest recente pagina
            Shell.Current.Navigation.PopAsync();
        }

        public void ShowPopup(string title, string text)
        {
            App.Current.MainPage.DisplayAlert(title, text, "Sluiten");
        }
    }
}
