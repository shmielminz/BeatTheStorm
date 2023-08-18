using BeatTheStormSystem;
using static BeatTheStormSystem.Game;

namespace BeatTheStormApp
{
    public partial class frmBeatTheStorm : Form
    {
        string imagepath = Application.StartupPath + @"Images\";
        Game game = new();
        List<RadioButton> optbtns = new();
        bool checkedhandled = false;
        PictureBox picDicePlayer1 = new() { Dock = DockStyle.Fill, SizeMode = PictureBoxSizeMode.CenterImage };
        PictureBox picDicePlayer2 = new() { Dock = DockStyle.Fill, SizeMode = PictureBoxSizeMode.CenterImage };
        PictureBox picCardPlayer1 = new() { Dock = DockStyle.Fill, SizeMode = PictureBoxSizeMode.Zoom };
        PictureBox picCardPlayer2 = new() { Dock = DockStyle.Fill, SizeMode = PictureBoxSizeMode.Zoom };
        
        public frmBeatTheStorm()
        {
            InitializeComponent();
            btnStart.Click += BtnStart_Click;
            picDicePlayer1.Click += PicDiceVal_Click;
            picDicePlayer2.Click += PicDiceVal_Click;
            btnHelp.Click += BtnHelp_Click;
            btnRestart.Click += BtnRestart_Click;
            picCardPlayer1.Click += PicCard_Click;
            picCardPlayer2.Click += PicCard_Click;
            foreach (Control c in tblMain.Controls)
            {
                foreach (Control ctrl in c.Controls)
                {
                    if (ctrl is RadioButton)
                    {
                        RadioButton opt = (RadioButton)ctrl;
                        optbtns.Add(opt);
                    }
                }
            }
            optbtns.ForEach(o => o.CheckedChanged += Opt_CheckedChanged);
            lblPlayer1.Text = "Player " + PlayerEnum.A;
            lblPlayer2.Text = "Player " + PlayerEnum.B;
        }


        private void DoTurn()
        {
            if (gamestatus == GameStatusEnum.Playing)
            {
                if (player == PlayerEnum.A)
                {
                    picCardPlayer1.ImageLocation = imagepath + game.PlayingCard + ".jpg";
                }
                else if (player == PlayerEnum.B)
                {
                    picCardPlayer2.ImageLocation = imagepath + game.PlayingCard + ".jpg";
                }
                
            }

            if (gamestatus == GameStatusEnum.Playing)
            {
                if (player == PlayerEnum.A)
                {
                    tblPlayer2.BackColor = Color.AliceBlue;
                    tblPlayer1.BackColor = DefaultBackColor;
                    lblDiceOrCardPlayer1.Text = $"Player {game.CurrentPlayer.PlayerName} turn";
                    lblDiceOrCardPlayer2.Text = game.GameStatusDescription;
                }
                else
                {
                    tblPlayer1.BackColor = Color.AliceBlue;
                    tblPlayer2.BackColor = DefaultBackColor;
                    lblDiceOrCardPlayer2.Text = $"Player {game.CurrentPlayer.PlayerName} turn";
                    lblDiceOrCardPlayer1.Text = game.GameStatusDescription;
                }
            }

            dicerolled = false;
        }

        private void RollDice()
        {
            if (gamemode == GameModeEnum.DiceWithRandomCard && gamestatus == GameStatusEnum.Playing && !dicerolled)
            {
                if (player == PlayerEnum.A)
                {
                    picDicePlayer1.ImageLocation = imagepath + "dice" + game.DiceValue + ".jpg";
                }
                else if (player == PlayerEnum.B)
                {
                    picDicePlayer2.ImageLocation = imagepath + "dice" + dice.ToString() + ".jpg";
                }
            }
        }

        private void StartGame()
        {
            game.StartGame();
            game.GameMode = optModeCardOnly.Checked ? GameModeEnum.CardOnly : GameModeEnum.DiceWithRandomCard;
            game.PlayAgainstComputer = optPlayComputer.Checked;
            lblStatus.Text = game.GameMode == GameModeEnum.CardOnly ? "Click on card deck to pick a card." : "Throw the dice by clicking on dice";

            switch (game.GameMode)
            {
                case GameModeEnum.DiceWithRandomCard:
                    tblPlayer1.Controls.Add(picDicePlayer1, 0, 2);
                    tblPlayer2.Controls.Add(picDicePlayer2, 0, 2);
                    tblPlayer1.Controls.Add(picCardPlayer1, 0, 3);
                    tblPlayer2.Controls.Add(picCardPlayer2, 0, 3);
                    break;
                case GameModeEnum.CardOnly:
                    tblPlayer1.Controls.Add(picCardPlayer1, 0, 2);
                    tblPlayer2.Controls.Add(picCardPlayer2, 0, 2);
                    tblPlayer1.SetRowSpan(picCardPlayer1, 2);
                    tblPlayer1.SetRowSpan(picCardPlayer2, 2);
                    break;
            }

            lblDiceOrCardPlayer1.Text = $"Player {game.CurrentPlayer.PlayerName} ";
            lblDiceOrCardPlayer1.Text += game.GameMode == GameModeEnum.CardOnly ? "Pick a Card" : "Throw the Dice";
            lblDiceOrCardPlayer2.Text = $"Player {game.CurrentPlayer.PlayerName} turn";
            
            picCardPlayer1.ImageLocation = imagepath + "Hashomrimback.jpg";
            
            picCardPlayer2.ImageLocation = imagepath + "Hashomrimback.jpg";
            if (game.GameMode == GameModeEnum.DiceWithRandomCard)
            {
                picDicePlayer2.ImageLocation = imagepath + "dice1.jpg";
                picDicePlayer1.ImageLocation = imagepath + "dice1.jpg";
            }
            tblPlayer1.BackColor = Color.AliceBlue;
            tblPlayer2.BackColor = DefaultBackColor;

            if (game.PlayAgainstComputer)
            {
                picCardPlayer2.Enabled = false;
                picDicePlayer2.Enabled = false;
            }
        }

        private void RestartGame()
        {
            lblStatus.Text = "Choose playing mode and click start";
            game.RestartGame();
            picDicePlayer1.Image = null;
            picCardPlayer1.Image = null;
            picDicePlayer2.Image = null;
            picCardPlayer2.Image = null;
            lblDiceOrCardPlayer1.Text = "";
            lblDiceOrCardPlayer2.Text = "";
            picCardPlayer2.Enabled = true;
            picDicePlayer2.Enabled = true;
        }

        private void RadioButtonControl(RadioButton opt)
        {
            opt.Checked = false;
            checkedhandled = true;
            switch (game.GameMode)
            {
                case GameModeEnum.DiceWithRandomCard:
                    optModeDiceWithRandomCard.Checked = true;
                    break;
                case GameModeEnum.CardOnly:
                    optModeCardOnly.Checked = true;
                    break;
                default:
                    break;
            }
            switch (game.PlayAgainstComputer)
            {
                case false:
                    optMultiplePlayers.Checked = true;
                    break;
                case true:
                    optPlayComputer.Checked = true;
                    break;
            }
        }

        private void BtnStart_Click(object? sender, EventArgs e)
        {
            StartGame();
        }

        private void Opt_CheckedChanged(object? sender, EventArgs e)
        {
            if (sender != null && game.GameStatus == GameStatusEnum.Playing && !checkedhandled)
            {
                RadioButtonControl((RadioButton)sender);
            }
            checkedhandled = false;
        }

        private void PicDiceVal_Click(object? sender, EventArgs e)
        {
            RollDice();
        }

        private void PicCard_Click(object? sender, EventArgs e)
        {
            //if (gamestatus == GameStatusEnum.Playing && (dicerolled || gamemode == GameModeEnum.CardOnly))
            //{
            //    if (gamemode == GameModeEnum.CardOnly)
            //    {
            //        SwitchPlayer();
            //    }
            //    if (player == PlayerEnum.A || playermode == PlayerModeEnum.TwoPlayers) { 
            DoTurn();
            //    }
            //}
        }

        private void BtnHelp_Click(object? sender, EventArgs e)
        {
            WebBrowser browser = new();
            string str = Application.StartupPath + @"Images\Spec_for_Beat_the_Storm.pdf";
            Uri uri = new(str);
            browser.Navigate(uri, true);
        }

        private void BtnRestart_Click(object? sender, EventArgs e)
        {
            RestartGame();
        }
    }
}
