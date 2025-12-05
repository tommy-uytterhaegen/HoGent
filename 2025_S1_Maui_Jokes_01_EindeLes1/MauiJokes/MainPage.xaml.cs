using HoGentMauiBL.Services;
using HoGentMauiDL;

namespace HoGentMaui
{
    public partial class MainPage : ContentPage
    {
        private JokeService JokeService { get; init; }

        public MainPage()
        {
            InitializeComponent();

            JokeService = new JokeService(new JokeRepository());
        }

        private void RandomJokeButton_Clicked(object sender, EventArgs e)
        {
            JokeLabel.Text = JokeService.GetRandomJoke();
        }

        private async void AddJokeButton_Clicked(object sender, EventArgs e)
        {
            JokeService.AddJoke(NewJokeEntry.Text);

            NewJokeEntry.Text = null;

            await DisplayAlert("Toegevoegd", "De grap is toegevoegd", "Sluiten");
            
        }
    }
}
