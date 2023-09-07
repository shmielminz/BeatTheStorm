using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BeatTheStormSystem
{
    public class Game : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler? ScoreChanged;
        public enum GameModeEnum { DiceWithRandomCard, CardOnly }
        public enum GameStatusEnum { NotStarted, Playing, Winner, Loser }
        GameStatusEnum _gamestatus = GameStatusEnum.NotStarted;
        int _dice = 0;
        int _currentplayerindex = 0;
        string _card = "";
        List<Dictionary<string, string>> lstcards;
        private Player _currentplayer = new();
        bool _dicerolled = false;
        bool allowclick = true;
        private static int scoreplayerwins;
        private static int scoreplayerloses;
        private static int numgames;
        public Game()
        {
            numgames++;
            this.GameName = "Game " + numgames;
            for (int i = 0; i < 101; i++)
            {
                this.Spots.Add(new Spot());
            }
            lstcards = new() {
                //SM - cards
                new() { { "Name", "Facebook" }, { "Value", "5" } },
                new() { { "Name", "Instagram" }, { "Value", "5" } },
                new() { { "Name", "Twitter" }, { "Value", "5" } },
                new() { { "Name", "Tiktok" }, { "Value", "5" } },
                new() { { "Name", "Netflix" }, { "Value", "5" } },
                new() { { "Name", "Twitch" }, { "Value", "5" } },
                new() { { "Name", "Vimeo" }, { "Value", "5" } },
                new() { { "Name", "Whatsapp" }, { "Value", "4" } },
                new() { { "Name", "Linkedin" }, { "Value", "4" } },
                new() { { "Name", "YouTube" }, { "Value", "3" } },
                new() { { "Name", "Roblox" }, { "Value", "3" } },
                new() { { "Name", "Skype" }, { "Value", "3" } },
                new() { { "Name", "Steam" }, { "Value", "3" } },
                new() { { "Name", "Telegram" }, { "Value", "3" } },
                new() { { "Name", "Pinterest" }, { "Value", "3" } },
                new() { { "Name", "Googleplay" }, { "Value", "3" } },
                new() { { "Name", "Zoom" }, { "Value", "3" } },
                new() { { "Name", "Bing" }, { "Value", "2" } },
                new() { { "Name", "Google" }, { "Value", "2" } },
                new() { { "Name", "Bbcnews" }, { "Value", "2" } },
                new() { { "Name", "Nytimes" }, { "Value", "2" } },
                new() { { "Name", "Gmail" }, { "Value", "1" } },
                new() { { "Name", "Slack" }, { "Value", "1" } },
                new() { { "Name", "Yahoomail" }, { "Value", "1" } },
                new() { { "Name", "Outlook" }, { "Value", "1" } },
                new() { { "Name", "Aolmail" }, { "Value", "1" } },
                //SM + cards
                new() { { "Name", "Hashomrim1" }, { "Value", "5" } },
                new() { { "Name", "Hashomrim2" }, { "Value", "5" } },
                new() { { "Name", "Hashomrim3" }, { "Value", "5" } },
                new() { { "Name", "Hashomrim4" }, { "Value", "5" } },
                new() { { "Name", "Hashomrim5" }, { "Value", "5" } },
                new() { { "Name", "Hashomrim6" }, { "Value", "5" } },
                new() { { "Name", "Hashomrim7" }, { "Value", "4" } },
                new() { { "Name", "Hashomrim8" }, { "Value", "4" } },
                new() { { "Name", "Hashomrim9" }, { "Value", "3" } },
                new() { { "Name", "Hashomrim10" }, { "Value", "3" } },
                new() { { "Name", "Hashomrim11" }, { "Value", "3" } },
                new() { { "Name", "Hashomrim12" }, { "Value", "3" } },
                new() { { "Name", "Hashomrim13" }, { "Value", "3" } },
                new() { { "Name", "Hashomrim14" }, { "Value", "3" } },
                new() { { "Name", "Hashomrim15" }, { "Value", "3" } },
                new() { { "Name", "Hashomrim16" }, { "Value", "3" } },
                new() { { "Name", "Hashomrim17" }, { "Value", "2" } },
                new() { { "Name", "Hashomrim18" }, { "Value", "2" } },
                new() { { "Name", "Hashomrim19" }, { "Value", "2" } },
                new() { { "Name", "Hashomrim20" }, { "Value", "2" } },
                new() { { "Name", "Hashomrim21" }, { "Value", "1" } },
                new() { { "Name", "Hashomrim22" }, { "Value", "1" } },
                new() { { "Name", "Hashomrim23" }, { "Value", "1" } },
                new() { { "Name", "Hashomrim24" }, { "Value", "1" } },
                new() { { "Name", "Hashomrim25" }, { "Value", "1" } },
                new() { { "Name", "Hashomrim26" }, { "Value", "1" } }
            };
        }
        private Stack<Dictionary<string, object>> UndoStack { get; set; } = new();
        private Stack<Dictionary<string, object>> RedoStack { get; set; } = new();
        private Dictionary<string, object> PlayersMovesWhenInComputerMode { get; set; } = new();
        public List<Dictionary<string, object>> Last10Moves { get; private set; } = new();
        public Microsoft.Maui.Graphics.Color BackColorForUndo
        {
            get
            {
                if (this.UndoStack.Count > 0)
                {
                    return Microsoft.Maui.Graphics.Colors.AliceBlue;
                }
                else
                {
                    return Microsoft.Maui.Graphics.Colors.DimGray;
                }
            }
        }
        public Microsoft.Maui.Graphics.Color BackColorForRedo
        {
            get
            {
                if (this.RedoStack.Count > 0)
                {
                    return Microsoft.Maui.Graphics.Colors.AliceBlue;
                }
                else
                {
                    return Microsoft.Maui.Graphics.Colors.DimGray;
                }
            }
        }

        public string GameName { get; private set; }
        public string GameModeHeader { get => "For " + this.GameName; }
        public List<Player> Players { get; private set; } = new();
        public void AddPlayer(Player player)
        {
            this.Players.Add(player);
        }
        public List<Spot> Spots { get; private set; } = new();
        public GameStatusEnum GameStatus
        {
            get => _gamestatus;
            private set
            {
                _gamestatus = value;
                this.InvokePropertyChanged();
                this.InvokePropertyChanged("GameStatusDescription");
                this.InvokePropertyChanged("StartButtonText");
                this.InvokePropertyChanged("StopButtonText");
            }
        }
        public Player CurrentPlayer
        {
            get => _currentplayer;
            private set
            {
                _currentplayer = value;
                this.InvokePropertyChanged();
                this.InvokePropertyChanged("GameStatusDescription");
            }
        }
        public GameModeEnum GameMode { get; set; }

        public string StopButtonText
        {
            get
            {
                string s = "";
                switch (this.GameStatus)
                {
                    case GameStatusEnum.NotStarted:
                        s = "Start ";
                        break;
                    case GameStatusEnum.Playing:
                    case GameStatusEnum.Winner:
                    case GameStatusEnum.Loser:
                        s = "Pause ";
                        break;
                }
                s += this.GameName;
                return s;
            }
        }
        public string StartButtonText
        {
            get
            {
                string s = "";
                switch (this.GameStatus)
                {
                    case GameStatusEnum.NotStarted:
                        s = "Start ";
                        break;
                    case GameStatusEnum.Playing:
                    case GameStatusEnum.Winner:
                    case GameStatusEnum.Loser:
                        s = "Continue ";
                        break;
                }
                s += this.GameName;
                return s;
            }
        }

        public string PlayingCard
        {
            get => _card;
            private set
            {
                _card = value;
                this.InvokePropertyChanged();
                this.InvokePropertyChanged("CardImage");
            }
        }
        public int DiceValue
        {
            get => _dice;
            private set
            {
                _dice = value;
                this.InvokePropertyChanged();
                this.InvokePropertyChanged("DiceImage");
            }
        }
        public string DiceImage
        {
            get=> $"dice{this.DiceValue}.jpg";
        }
        public string CardImage
        {
            get => $"{this.PlayingCard.ToLower()}.jpg";
        }
        //SM Following 2 properties are not used in MAUI and will be removed.
        public string PreviousCard { get; private set; } = "";
        public int PreviousDice { get; private set; }
        public bool PlayAgainstComputer { get; set; }
        public Player Winner { get; private set; } = new();
        public Player Loser { get; private set; } = new();
        public static string Score { get => $"Wins = {scoreplayerwins}: Loses = {scoreplayerloses}"; }
        public void StartGame(bool playagainstcomputer = false, GameModeEnum gamemode = GameModeEnum.CardOnly)
        {
            if (this.GameStatus != GameStatusEnum.Playing)
            {
                _currentplayerindex = 0;
                this.PlayAgainstComputer = playagainstcomputer;
                this.GameMode = gamemode;
                this.GameStatus = GameStatusEnum.Playing;
                this.CurrentPlayer = this.Players[_currentplayerindex];
                if (playagainstcomputer)
                {
                    this.AddPlayer(new() { PlayerName = "Computer", PlayingPiece = "C" });
                }
                this.Players.ForEach(p =>
                {
                    this.Spots[50].AddPlayerToSpot(p);
                    p.SpotValue = this.Spots[50];
                });
                this.UndoStack.Clear();
                this.RedoStack.Clear();
                this.Last10Moves.Clear();
            }
        }
        public async Task TakeSpot(int spotnum, bool pause = true)
        {
            int fromspotnum = this.Spots.IndexOf(this.CurrentPlayer.SpotValue);
            for (int i = fromspotnum; i != (fromspotnum > spotnum ? spotnum - 1 : spotnum + 1); i += fromspotnum < spotnum ? 1 : -1)
            {
                this.CurrentPlayer.SpotValue.RemovePlayerFromSpot(this.CurrentPlayer);
                this.CurrentPlayer.SpotValue = this.Spots[i];
                this.Spots[i].AddPlayerToSpot(this.CurrentPlayer);
                //SM Remove this if statement for test to run.
                if (pause)
                {
                    await System.Threading.Tasks.Task.Delay(1000);
                }
            }
            allowclick = true;
            this.InvokePropertyChanged("BackColorForUndo");
            this.InvokePropertyChanged("BackColorForRedo");
        }
        public async Task TakeTurn(Spot spot)
        {
            if (_dicerolled || this.GameMode == GameModeEnum.CardOnly)
            {
                _dicerolled = false;
                this.PreviousDice = this.DiceValue;
                this.PreviousCard = this.PlayingCard;
                int dice = 0;
                Dictionary<string, string> card = this.GetRandomCard();
                this.PlayingCard = card["Name"];
                if (GameMode == GameModeEnum.DiceWithRandomCard)
                {
                    dice = this.DiceValue;
                }
                else
                {
                    if (int.TryParse(card["Value"], out int n))
                    {
                        dice = n;
                        this.DiceValue = dice;
                    }
                }
                int spotnum = this.Spots.IndexOf(spot);
                if (card["Name"].Contains("Hashomrim"))
                {
                    if (spotnum == this.Spots.Count - 1)
                    {
                        this.Winner = this.CurrentPlayer;
                        this.GameStatus = GameStatusEnum.Winner;
                        scoreplayerwins++;
                        ScoreChanged?.Invoke(this, new EventArgs());
                    }
                    spotnum += dice;
                }
                else
                {
                    if (spotnum == 0)
                    {
                        this.Loser = this.CurrentPlayer;
                        this.GameStatus = GameStatusEnum.Loser;
                        scoreplayerloses++;
                        ScoreChanged?.Invoke(this, new EventArgs());
                    }
                    spotnum -= dice;
                }
                if (spotnum >= this.Spots.Count)
                {
                    spotnum = this.Spots.Count - 1;
                }
                else if (spotnum < 0)
                {
                    spotnum = 0;
                }
                if (this.GameStatus == GameStatusEnum.Playing)
                {
                    await TakeSpot(spotnum);
                }
                RedoStack.Clear();
                UndoStack.Push(new() {
                    { "Player", this.CurrentPlayer },
                    { "FromSpot", spot },
                    { "ToSpot", this.Spots[spotnum] },
                    { "Card", this.PlayingCard },
                    { "Dice", dice }
                });
                Last10Moves.Insert(0, UndoStack.Peek());
                if (this.GameStatus == GameStatusEnum.Playing)
                {
                    SwitchPlayer();
                    if (IsComputerTurn())
                    {
                        PlayersMovesWhenInComputerMode.Clear();
                        PlayersMovesWhenInComputerMode.Add("Card", UndoStack.Peek()["Card"]);
                        PlayersMovesWhenInComputerMode.Add("Dice", UndoStack.Peek()["Dice"]);
                        RollDice();
                        await DoTurn();
                    }
                }
            }
        }
        public async Task DoTurn()
        {
            if (GameStatus == GameStatusEnum.Playing && allowclick)
            {
                allowclick = false;
                await TakeTurn(this.CurrentPlayer.SpotValue);
            }
        }
        private void SwitchPlayer()
        {
            _currentplayerindex = this.Players.IndexOf(this.CurrentPlayer) + 1;
            if (_currentplayerindex >= this.Players.Count)
            {
                _currentplayerindex = 0;
            }
            this.CurrentPlayer = this.Players[_currentplayerindex];
        }
        private bool IsComputerTurn()
        {
            if (this.CurrentPlayer.PlayerName == "Computer")
            {
                return true;
            }
            return false;
        }
        public int RollDice()
        {
            int dice = 0;
            Random rnd = new(DateTime.Now.Microsecond);
            if (this.GameStatus == GameStatusEnum.Playing && allowclick)
            {
                dice = rnd.Next(1, 7);
                if (!_dicerolled)
                {
                    this.DiceValue = dice;
                    _dicerolled = true;
                }
            }
            return dice;
        }
        public Dictionary<string, string> GetRandomCard()
        {
            Random rnd = new(DateTime.Now.Microsecond);
            int n = rnd.Next(lstcards.Count);
            return lstcards[n];
        }
        public async void Undo()
        {
            if (this.GameStatus == GameStatusEnum.Playing)
            {
                if (UndoStack.Count > 0)
                {
                    RedoStack.Push(UndoStack.Pop());
                    //SM Remove this if statement for test to run.
                    if (PlayAgainstComputer)
                    {
                        this.Last10Moves.RemoveAt(0);
                        await TakeSpot(this.Spots.IndexOf(GetPlayerSpotForUndoRedo(RedoStack, "FromSpot")), false);
                        RedoStack.Push(UndoStack.Pop());
                    }
                    this.Last10Moves.RemoveAt(0);
                    await TakeSpot(this.Spots.IndexOf(GetPlayerSpotForUndoRedo(RedoStack, "FromSpot")), false);
                }
            }
        }
        private Spot GetPlayerSpotForUndoRedo(Stack<Dictionary<string, object>> stack, string fromto)
        {
            Spot spot = (Spot)stack.Peek()[fromto];
            this.CurrentPlayer = (Player)stack.Peek()["Player"];
            this.PlayingCard = (string)stack.Peek()["Card"];
            this.DiceValue = (int)stack.Peek()["Dice"];
            return spot;
        }
        public async void Redo()
        {
            if (this.GameStatus == GameStatusEnum.Playing)
            {
                if (RedoStack.Count > 0)
                {
                    UndoStack.Push(RedoStack.Pop());
                    if (PlayAgainstComputer)
                    {
                        await TakeSpot(this.Spots.IndexOf(GetPlayerSpotForUndoRedo(UndoStack, "ToSpot")), false);
                        UndoStack.Push(RedoStack.Pop());
                        Last10Moves.Insert(0, UndoStack.Peek());
                    }
                    await TakeSpot(this.Spots.IndexOf(GetPlayerSpotForUndoRedo(UndoStack, "ToSpot")), false);
                    SwitchPlayer();
                    Last10Moves.Insert(0, UndoStack.Peek());
                }
            }
        }
        public string GameStatusDescription
        {
            get
            {
                string s = $"{this.GameName}: ";
                switch (this.GameStatus)
                {
                    case GameStatusEnum.Playing:
                        s += $"Player {this.CurrentPlayer.PlayerName}: " + (GameMode == GameModeEnum.DiceWithRandomCard ? "Throw the Dice, and pick a Card" : "Pick a Card");
                        break;
                    case GameStatusEnum.Winner:
                        s += $"Player {this.CurrentPlayer.PlayerName}: Winner!!!";
                        break;
                    case GameStatusEnum.Loser:
                        s += $"Player {this.CurrentPlayer.PlayerName}: Loser";
                        break;
                    case GameStatusEnum.NotStarted:
                        s += "Choose playing mode and click Start";
                        break;
                }
                return s;
            }
        }
        public void RestartGame()
        {
            if (this.GameStatus != GameStatusEnum.NotStarted)
            {
                this.Spots.ForEach(s =>
                {
                    if (s.SpotPlayers.Count > 0)
                    {
                        this.Players.ForEach(p =>
                        {
                            s.RemovePlayerFromSpot(p);
                            p.SpotValue = new();
                        });
                    }

                });
                this.GameStatus = GameStatusEnum.NotStarted;
                this.Players.Clear();
                this.DiceValue = 0;
                this.PlayingCard = "";
                allowclick = true;
            }
        }
        private void InvokePropertyChanged([CallerMemberName] string propertyname = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
    }
}