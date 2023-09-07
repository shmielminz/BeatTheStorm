using BeatTheStormSystem;

namespace BeatTheStormMAUI;

public partial class Settings : ContentPage
{
    Game activegame;
    List<Player> players = new();
    List<Game> lstgame = new() { new(), new(), new() };
    public Settings()
    {
        InitializeComponent();
        this.Loaded += Settings_Loaded;
        this.Appearing += Settings_Appearing;
        MultiplePlayerRdo.CheckedChanged += MultiplePlayerRdo_CheckedChanged;
        lstgame.ForEach(g => g.ScoreChanged += G_ScoreChanged);
        Game1Rb.BindingContext = lstgame[0];
        Game2Rb.BindingContext = lstgame[1];
        Game3Rb.BindingContext = lstgame[2];
        activegame = lstgame[0];
        this.BindingContext = activegame;
    }

    private void DisableEnableRadioButtons()
    {
        if (activegame.GameStatus != Game.GameStatusEnum.NotStarted)
        {
            MultiplePlayerRdo.IsEnabled = false;
            PlayComputerRdo.IsEnabled = false;
            ModeCardOnlyRdo.IsEnabled = false;
            ModeDiceWithRandomCardRdo.IsEnabled = false;
            Player1NameTxt.IsEnabled = false;
            Player2NameTxt.IsEnabled = false;
            PlayingPieceLst.IsEnabled = false;
            PlayingPiece2Lst.IsEnabled = false;
        }
        else
        {
            MultiplePlayerRdo.IsEnabled = true;
            PlayComputerRdo.IsEnabled = true;
            ModeCardOnlyRdo.IsEnabled = true;
            ModeDiceWithRandomCardRdo.IsEnabled = true;
            Player1NameTxt.IsEnabled = true;
            Player2NameTxt.IsEnabled = true;
            PlayingPieceLst.IsEnabled = true;
            PlayingPiece2Lst.IsEnabled = true;
        }
    }

    private void Settings_Appearing(object sender, EventArgs e)
    {
        DisableEnableRadioButtons();
    }

    private void G_ScoreChanged(object sender, EventArgs e)
    {
        ScoreLbl.Text = Game.Score;
    }

    private void Settings_Loaded(object sender, EventArgs e)
    {
        HLayoutPlayer2Name.IsVisible = true;
        //SM For some reason the xaml doesn't work to set the checked property to true.
        MultiplePlayerRdo.IsChecked = true;
        Game1Rb.IsChecked = true;
        
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
        ShowGame(PlayComputerRdo.IsChecked, ModeCardOnlyRdo.IsChecked);
    }

    private bool SetPlayers()
    {
        bool playersadded = false;
        players.Clear();
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

    private async void ShowGame(bool playcomputer, bool cardonly)
    {
        bool playersadded = SetPlayers();
        if (playersadded)
        {
            if (activegame.GameStatus == Game.GameStatusEnum.NotStarted)
            {
                players.ForEach(activegame.AddPlayer);
                activegame.StartGame(playcomputer, cardonly ? Game.GameModeEnum.CardOnly : Game.GameModeEnum.DiceWithRandomCard);
            }
            await Navigation.PushModalAsync(new BeatTheStorm(activegame, players));
        }
    }

    private void Game_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        RadioButton rb = (RadioButton)sender;
        if (rb.IsChecked && rb.BindingContext != null)
        {
            activegame = (Game)rb.BindingContext;
            this.BindingContext = activegame;
            DisableEnableRadioButtons();
        }
    }
}