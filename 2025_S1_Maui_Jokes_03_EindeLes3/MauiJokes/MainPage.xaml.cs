using HoGentMaui.ViewModels;
using HoGentMauiBL.Services;

namespace HoGentMaui
{
    public partial class MainPage : ContentPage
    {
        private JokeService JokeService { get; init; }

        // TODO: De constructor is de enige code die hier mag overblijven
        public MainPage(MainViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = viewModel;
        }

        private void AddJokeButton_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(NewJokeEntry.Text))
            {
                // TODO: Deze zal niet meer werken gezien we geen JokeService binnenkrijgen. Deze hoort ook hier niet meer. Verplaats deze naar het ViewModel en zet de click event om 
                // naar een Command
                JokeService.AddJoke(NewJokeEntry.Text);

                NewJokeEntry.Text = null;
               
                DisplayAlert("Toegevoegd", "De grap is toegevoegd", "Sluiten");
            }
            else
                DisplayAlert("Gefaald", "Er is geen grap ingevoerd", "Sluiten");
        }

        private void DeleteJokeButton_Clicked(object sender, EventArgs e)
        {
            // TODO: Deze zal niet meer werken gezien we geen JokeService binnenkrijgen. Deze hoort ook hier niet meer. Verplaats deze naar het ViewModel en zet de click event om 
            // naar een Command
            var isDeleted = JokeService.DeleteJoke(JokeLabel.Text);
            if (isDeleted)
            {
                DisplayAlert("Verwijderd", "De grap is verwijderd", "Sluiten");
                JokeLabel.Text = "";
            }
            else
                DisplayAlert("Gefaald", "De grap is NIET verwijderd", "Sluiten");
        }
    }
}
