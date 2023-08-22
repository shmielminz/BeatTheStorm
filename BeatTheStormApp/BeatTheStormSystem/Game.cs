using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BeatTheStormSystem
{
    public class Game : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public enum GameModeEnum { DiceWithRandomCard, CardOnly }
        public enum GameStatusEnum { NotStarted, Playing, Winner, Loser }
        GameStatusEnum _gamestatus = GameStatusEnum.NotStarted;
        int _dice = 0;
        int _currentplayerindex = 0;
        string _card = "";
        GameModeEnum _gamemode = GameModeEnum.CardOnly;
        List<Dictionary<string, string>> lstcards;
        private Player _currentplayer = new();
        public Game()
        {
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
        private Stack<Dictionary<string, object>> UndoStack = new();
        private Stack<Dictionary<string, object>> RedoStack = new();


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
        public GameModeEnum GameMode
        {
            get => _gamemode;
            set
            {
                _gamemode = value;
            }
        }
        public string PreviousCard { get; private set; } = "";
        public int PreviousDice { get; private set; }
        public bool PlayAgainstComputer { get; set; }
        public Player Winner { get; private set; } = new();
        public Player Loser { get; private set; } = new();
        public void StartGame(bool playagainstcomputer = false, GameModeEnum gamemode = GameModeEnum.CardOnly)
        {
            if (this.GameStatus == GameStatusEnum.NotStarted)
            {
                this.PlayAgainstComputer = playagainstcomputer;
                this.GameMode = gamemode;
                this.GameStatus = GameStatusEnum.Playing;
                this.CurrentPlayer = this.Players[_currentplayerindex];
                if (playagainstcomputer)
                {
                    this.AddPlayer(new() { PlayerName = "Computer", PlayingPiece = "d" });
                }
                this.Players.ForEach(p =>
                {
                    this.Spots[50].AddPlayerToSpot(p);
                    p.SpotValue = this.Spots[50];
                });
                this.UndoStack.Clear();
                this.RedoStack.Clear();
            }
        }
        public void TakeSpot(int spotnum)
        {
            this.CurrentPlayer.SpotValue.RemovePlayerFromSpot(this.CurrentPlayer);
            this.CurrentPlayer.SpotValue = this.Spots[spotnum];
            this.Spots[spotnum].AddPlayerToSpot(this.CurrentPlayer);
        }
        public void TakeTurn(Spot spot)
        {
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
                }
            }
            int spotnum = this.Spots.IndexOf(spot);
            if (card["Name"].Contains("Hashomrim"))
            {
                if (spotnum == this.Spots.Count - 1)
                {
                    this.Winner = this.CurrentPlayer;
                    this.GameStatus = GameStatusEnum.Winner;
                }
                spotnum += dice;
            }
            else
            {
                if (spotnum == 0)
                {
                    this.Loser = this.CurrentPlayer;
                    this.GameStatus = GameStatusEnum.Loser;
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
            TakeSpot(spotnum);
            UndoStack.Push(new() {
                { "Player", this.CurrentPlayer },
                { "FromSpot", spot },
                { "ToSpot", this.Spots[spotnum] }
            });
            if (this.GameStatus == GameStatusEnum.Playing)
            {
                SwitchPlayer();
                if (IsComputerTurn())
                {
                    TakeTurn(this.CurrentPlayer.SpotValue);
                }
            }
        }
        public void DoTurn()
        {
            if (GameStatus == GameStatusEnum.Playing)
            {
                TakeTurn(this.CurrentPlayer.SpotValue);
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
            Random rnd = new();
            int dice = rnd.Next(1, 7);
            this.DiceValue = dice;
            return dice;
        }
        public Dictionary<string, string> GetRandomCard()
        {
            Random rnd = new();
            int n = rnd.Next(lstcards.Count);
            return lstcards[n];
        }
        public string PlayingCard
        {
            get => _card;
            private set
            {
                _card = value;
                this.InvokePropertyChanged();
            }
        }
        public int DiceValue
        {
            get => _dice;
            private set
            {
                _dice = value;
                this.InvokePropertyChanged();
            }
        }
        public void Undo()
        {
            RedoStack.Push(UndoStack.Pop());
            if (PlayAgainstComputer)
            {
                RedoStack.Push(UndoStack.Pop());
            }
            this.CurrentPlayer = (Player)RedoStack.Peek()["Player"];
            Spot s = (Spot)RedoStack.Peek()["FromSpot"];
            TakeSpot(this.Spots.IndexOf(s));
        }
        public void Redo()
        {
            UndoStack.Push(RedoStack.Pop());
            if (PlayAgainstComputer)
            {
                UndoStack.Push(RedoStack.Pop());
            }
            this.CurrentPlayer = (Player)UndoStack.Peek()["Player"];
            Spot s = (Spot)UndoStack.Peek()["ToSpot"];
            TakeSpot(this.Spots.IndexOf(s));
            SwitchPlayer();
        }
        public string GameStatusDescription
        {
            get
            {
                string s = $"Player {this.CurrentPlayer.PlayerName} ";
                switch (this.GameStatus)
                {
                    case GameStatusEnum.Playing:
                        s += GameMode == GameModeEnum.DiceWithRandomCard ? "Throw the Dice, and pick a Card" : "Pick a Card";
                        break;
                    case GameStatusEnum.Winner:
                        s += "Winner!!!";
                        break;
                    case GameStatusEnum.Loser:
                        s += "Loser";
                        break;
                    case GameStatusEnum.NotStarted:
                        s = "Choose playing mode and click Start";
                        break;
                }
                return s;
            }
        }
        public void RestartGame()
        {
            if (this.GameStatus == GameStatusEnum.Playing)
            {
                this.Spots.ForEach(s =>
                {
                    this.Players.ForEach(p =>
                    {
                        s.RemovePlayerFromSpot(p);
                        p.SpotValue = this.Spots[50];
                    });
                });
                this.GameStatus = GameStatusEnum.NotStarted;
                this.Players.Clear();
            }
        }
        private void InvokePropertyChanged([CallerMemberName] string propertyname = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
    }
}