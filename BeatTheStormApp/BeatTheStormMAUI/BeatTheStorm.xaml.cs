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
            GameGrd.SetRow(CardBtn, 1);
        }
    }

    private void AddToLast10List()
    {
        List<Dictionary<string, object>> last10lst = game.Last10Moves;
        Last10MovesGrd.Clear();
        for (int i = 0; i < last10lst.Count; i++)
        {
            Border brd = new() { Padding = 1, StrokeThickness = 0.2 };
            Grid grd = new()
            {
                RowDefinitions =
                {
                    new(),
                    new(),
                    new()
                },
                ColumnDefinitions =
                {
                    new(),
                    new()
                },
                RowSpacing = 1
            };
            grd.Add(new Label() { Text = "Player" }, 0, 0);
            grd.Add(new Label() { Text = ((Player)last10lst[i]["Player"]).PlayerName }, 1, 0);
            grd.Add(new Label() { Text = "From Spot" }, 0, 1);
            grd.Add(new Label() { Text = (game.Spots.IndexOf((Spot)last10lst[i]["FromSpot"]) - 50).ToString() }, 1, 1);
            grd.Add(new Label() { Text = "To Spot" }, 0, 2);
            grd.Add(new Label() { Text = (game.Spots.IndexOf((Spot)last10lst[i]["ToSpot"]) - 50).ToString() }, 1, 2);
            brd.Content = grd;
            Last10MovesGrd.Add(brd, 0, i);
        }
        //PlayerLbl.Text = ((Player)last10lst[0]["Player"]).PlayerName;
        //FromSpotLbl.Text = (game.Spots.IndexOf((Spot)last10lst[0]["FromSpot"]) - 50).ToString();
        //ToSpotLbl.Text = (game.Spots.IndexOf((Spot)last10lst[0]["ToSpot"]) - 50).ToString();
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
        //DiceBtn.ImageSource = $"dice{game.DiceValue}.jpg";
    }

    private void CardBtn_Clicked(object sender, EventArgs e)
    {
        game.DoTurn();
        //CardBtn.ImageSource = $"{game.PlayingCard.ToLower()}.jpg";
        AddToLast10List();
    }

    private void UndoBtn_Clicked(object sender, EventArgs e)
    {
        game.Undo();
        AddToLast10List();
    }

    private void RedoBtn_Clicked(object sender, EventArgs e)
    {
        game.Redo();
        AddToLast10List();
    }

    private async void ShowHelp()
    {
        await Navigation.PushAsync(new Help());
    }

    private void HelpBtn_Clicked(object sender, EventArgs e)
    {
        ShowHelp();
    }
}