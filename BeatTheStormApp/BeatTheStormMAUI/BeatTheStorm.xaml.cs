using BeatTheStormSystem;

namespace BeatTheStormMAUI;

public partial class BeatTheStorm : ContentPage
{
	Game game = new();
	public BeatTheStorm()
	{
		InitializeComponent();
		this.BindingContext = game;
	}
}