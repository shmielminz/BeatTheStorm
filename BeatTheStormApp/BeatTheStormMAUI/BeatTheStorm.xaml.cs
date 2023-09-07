using BeatTheStormSystem;

namespace BeatTheStormMAUI;

public partial class BeatTheStorm : ContentPage
{
    Game game;
    List<Player> Players = new();
	public BeatTheStorm(Game activegame, List<Player> playerlst)
	{
		InitializeComponent();
        game = activegame;
		this.BindingContext = game;
        Players = playerlst;
        
        if (activegame.GameMode == Game.GameModeEnum.CardOnly)
        {
            DiceBtn.IsVisible = false;
            GameGrd.SetRow(CardBtn, 1);
        }
        AddToLast10List();
    }

    private void AddToLast10List()
    {
        List<Dictionary<string, object>> last10lst = game.Last10Moves;
        Last10MovesGrd.Clear();
        for (int i = 0; i < (last10lst.Count > 10 ? 10 : last10lst.Count); i++)
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
            grd.Add(new Label() { Text = "Player", FontSize = 10 }, 0, 0);
            grd.Add(new Label() { Text = ((Player)last10lst[i]["Player"]).PlayerName, FontSize = 10 }, 1, 0);
            grd.Add(new Label() { Text = "From Spot", FontSize = 10 }, 0, 1);
            grd.Add(new Label() { Text = (game.Spots.IndexOf((Spot)last10lst[i]["FromSpot"]) - 50).ToString(), FontSize = 10 }, 1, 1);
            grd.Add(new Label() { Text = "To Spot", FontSize = 10 }, 0, 2);
            grd.Add(new Label() { Text = (game.Spots.IndexOf((Spot)last10lst[i]["ToSpot"]) - 50).ToString(), FontSize = 10 }, 1, 2);
            brd.Content = grd;
            Last10MovesGrd.Add(brd, 0, i);
        }
    }

    private void RestartGameBtn_Clicked(object sender, EventArgs e)
    {
        game.RestartGame();
        Players.Clear();
        Navigation.PopModalAsync();
    }

    private void DiceBtn_Clicked(object sender, EventArgs e)
    {
        game.RollDice();
    }

    private async void CardBtn_Clicked(object sender, EventArgs e)
    {
        await game.DoTurn();
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

    private async void HelpBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new Help());
    }

    private void PauseGameBtn_Clicked(object sender, EventArgs e)
    {
        Navigation.PopModalAsync();
    }
}