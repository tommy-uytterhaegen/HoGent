using HoGentMaui.Interfaces;
using HoGentMaui.Services;
using HoGentMaui.ViewModels.Base;
using HoGentMauiBL.Interfaces;
using HoGentMauiBL.Services;
using MauiJokesBL.Interfaces;
using MauiJokesBL.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HoGentMaui.ViewModels
{
    public class MainViewModel : ViewModel
    {
        public INavigationService NavigationService { get; init; }
        public IJokeService JokeService { get; init; }

        private string _joke;
        public string Joke 
        {
            get => _joke;
            set
            {
                _joke = value;

                OnPropertyChanged();
            }
        }

        private string _newJoke;
        public string NewJoke
        {
            get => _newJoke;
            set
            {
                _newJoke = value;

                OnPropertyChanged();
            }
        }

        private JokeItemViewModel _selectedJoke;
        public JokeItemViewModel SelectedJoke
        {
            get => _selectedJoke;
            set
            {
                _selectedJoke = value;

                OnPropertyChanged();

                // Wanneer een nieuwe 'Joke' geselecteerd wordt, dan gaan we naar de pagina. Een andere optie is om 'SelectedItemChangedCommand' te gebruiken op de CollectionView
                NavigationService.GoToJokeDetail(_selectedJoke.Text);
            }
        }
        private ObservableCollection<JokeItemViewModel> _jokes;
        public ObservableCollection<JokeItemViewModel> Jokes
        {
            get => _jokes;
            set
            {
                _jokes = value;

                OnPropertyChanged();
            }
        }

        public ICommand RandomJokeCommand { get; init; }
        public ICommand AddJokeCommand { get; init; }

        public MainViewModel(INavigationService navigationService, IJokeService jokeService)
        {
            NavigationService = navigationService;
            JokeService = jokeService;

            RandomJokeCommand = new Command(async () => await OnRandomJoke());
            AddJokeCommand = new Command(() => OnAddJoke());

            var jokes = JokeService.GetAll()
                                   .Select(joke => MapToViewModel(joke));

            // We creëeren een ObservableCollection voor de CollectionView omdat we willen dat deze ook reageert als we items gaan toevoegen of verwijderen
            Jokes = new ObservableCollection<JokeItemViewModel>(jokes);
        }

        private JokeItemViewModel MapToViewModel(Joke joke)
        {
            return new JokeItemViewModel
            {
                Text = joke.Text,
                DateAsString = FormatDate(joke)
            };
        }

        private string FormatDate(Joke joke)
            => DateTime.Now.ToString("dd MMMM yyyy");
        
        private void OnAddJoke()
        {
            if (!string.IsNullOrWhiteSpace(NewJoke))
            {
                JokeService.AddJoke(NewJoke);
                Jokes.Add(MapToViewModel(new Joke(NewJoke)));

                NewJoke = null;

                NavigationService.ShowPopup("Toegevoegd", "De grap is toegevoegd");
            }
            else
                NavigationService.ShowPopup("Gefaald", "Er is geen grap ingevoerd");
        }

        private async Task OnRandomJoke()
        {
            Joke = await JokeService.GetRandomJoke();
        }
    }
}
