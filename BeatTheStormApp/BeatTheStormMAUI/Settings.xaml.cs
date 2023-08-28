using BeatTheStormSystem;

namespace BeatTheStormMAUI;

public partial class Settings : ContentPage
{
    List<Player> players = new();
    public Settings()
    {
        InitializeComponent();
        this.Loaded += Settings_Loaded;
        MultiplePlayerRdo.CheckedChanged += MultiplePlayerRdo_CheckedChanged;
    }

    private void Settings_Loaded(object sender, EventArgs e)
    {
        HLayoutPlayer2Name.IsVisible = true;
        MultiplePlayerRdo.IsChecked = true;
    }

    private void MultiplePlayerRdo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        Application.Current.Dispatcher.Dispatch(() =>
        {
            HLayoutPlayer2Name.IsVisible = (sender as RadioButton).IsChecked;
            players.Clear();
        });
    }

    private void StartBtn_Clicked(object sender, EventArgs e)
    {
        ShowGame();
    }

    private bool SetPlayers()
    {
        bool playersadded = false;

        if (Player1NameTxt.Text != "" && PlayingPieceLst.SelectedItem != null)
        {
            Player player = new()
            {
                PlayerName = Player1NameTxt.Text,
                PlayingPiece = PlayingPieceLst.SelectedItem.ToString()
            };
            players.Add(player);
            playersadded = true;
            if (MultiplePlayerRdo.IsChecked)
            {
                playersadded = false;
                if (Player2NameTxt.Text != "" && PlayingPiece2Lst.SelectedItem != null)
                {
                    Player player2 = new()
                    {
                        PlayerName = Player2NameTxt.Text,
                        PlayingPiece = PlayingPiece2Lst.SelectedItem.ToString()
                    };
                    players.Add(player2);
                    playersadded = true;
                }
            }
        }
        return playersadded;
    }

    private async void ShowGame()
    {
        bool playersadded = SetPlayers();
        if (playersadded)
        {
            await Navigation.PushAsync(new BeatTheStorm(players, PlayComputerRdo.IsChecked, ModeCardOnlyRdo.IsChecked));
        }
    }
}