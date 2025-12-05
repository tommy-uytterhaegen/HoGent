using HoGentMauiBL.Services;
using HoGentMauiDL;

namespace HoGentMaui
{
    public partial class MainPage : ContentPage
    {
        private JokeService JokeService { get; init; }

        public MainPage(JokeService jokeService)
        {
            InitializeComponent();

            JokeService = jokeService;
        }

        private void RandomJokeButton_Clicked(object sender, EventArgs e)
        {
            JokeLabel.Text = JokeService.GetRandomJoke();
        }

        private void AddJokeButton_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(NewJokeEntry.Text))
            {
                JokeService.AddJoke(NewJokeEntry.Text);

                NewJokeEntry.Text = null;

                DisplayAlert("Toegevoegd", "De grap is toegevoegd", "Sluiten");
            }
            else
                DisplayAlert("Gefaald", "Er is geen grap ingevoerd", "Sluiten");
        }

        private void DeleteJokeButton_Clicked(object sender, EventArgs e)
        {
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
