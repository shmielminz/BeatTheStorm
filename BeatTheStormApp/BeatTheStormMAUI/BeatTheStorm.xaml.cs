using BeatTheStormSystem;

namespace BeatTheStormMAUI;

public partial class BeatTheStorm : ContentPage
{
    Game game = new();
    List<Player> Players = new();
	public BeatTheStorm(List<Player> playerlst, bool playagainstcomputer = false, bool cardonly = false)
	{
		InitializeComponent();
		this.BindingContext = game;
        Players = playerlst;
        Players.ForEach(game.AddPlayer);
        game.StartGame(playagainstcomputer, cardonly ? Game.GameModeEnum.CardOnly : Game.GameModeEnum.DiceWithRandomCard);
        if (cardonly)
        {
            DiceBtn.IsVisible = false;
        }
    }

    private void RestartGameBtn_Clicked(object sender, EventArgs e)
    {
        game.RestartGame();
        Players.Clear();
        Navigation.PopAsync();
    }

    private void DiceBtn_Clicked(object sender, EventArgs e)
    {
        game.RollDice();
        DiceBtn.ImageSource = $"dice{game.DiceValue}.jpg";
    }

    private void CardBtn_Clicked(object sender, EventArgs e)
    {
        game.DoTurn();
        CardBtn.ImageSource = $"{game.PlayingCard.ToLower()}.jpg";
    }
}