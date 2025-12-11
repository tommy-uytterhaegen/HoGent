using HoGentMaui.ViewModels;

namespace HoGentMaui;

public partial class DetailPage : ContentPage
{
    // Doordat we 'DetailJokeViewModel' geregistreerd hebben bij de services kan het systeem de DetailPage maken en een DetailJokeViewModel meegeven aan de constructor
    public DetailPage(DetailJokeViewModel viewModel)
	{
		InitializeComponent();

		// Wanneer we MVVM willen gebruiken moet we de viewmodel in de property 'BindingContext' steken.
		// Indien we dit niet doen, dan weer de pagina niet waar hij moet zoeken voor {Binding ...}, deze wordt altijd gezocht in de BindingContext
		BindingContext = viewModel;
	}
}