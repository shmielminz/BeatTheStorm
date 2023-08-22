using BeatTheStormSystem;
using static BeatTheStormSystem.Game;

namespace BeatTheStormApp
{
    public partial class frmBeatTheStorm : Form
    {
        string imagepath = Application.StartupPath + @"Images\";
        Game game = new();
        List<RadioButton> optbtns = new();
        List<Label> lstspots;
        bool checkedhandled = false;
        PictureBox picDicePlayer1 = new() { Dock = DockStyle.Fill, SizeMode = PictureBoxSizeMode.CenterImage };
        PictureBox picDicePlayer2 = new() { Dock = DockStyle.Fill, SizeMode = PictureBoxSizeMode.CenterImage };
        PictureBox picCardPlayer1 = new() { Dock = DockStyle.Fill, SizeMode = PictureBoxSizeMode.Zoom };
        PictureBox picCardPlayer2 = new() { Dock = DockStyle.Fill, SizeMode = PictureBoxSizeMode.Zoom };
        public bool start = false;

        public frmBeatTheStorm(bool startgame = false)
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
            lblPlayer1.DataBindings.Add("Text", game, "GameStatusDescription");
            lstspots = new() {
                lblSpot1, lblSpot2, lblSpot3, lblSpot4, lblSpot5, lblSpot6, lblSpot7, lblSpot8, lblSpot9, lblSpot10,
                lblSpot11, lblSpot12, lblSpot13, lblSpot14, lblSpot15, lblSpot16, lblSpot17, lblSpot18, lblSpot19, lblSpot20,
                lblSpot21, lblSpot22, lblSpot23, lblSpot24, lblSpot25, lblSpot26, lblSpot27, lblSpot28, lblSpot29, lblSpot30,
                lblSpot31, lblSpot32, lblSpot33, lblSpot34, lblSpot35, lblSpot36, lblSpot37, lblSpot38, lblSpot39, lblSpot40,
                lblSpot41, lblSpot42, lblSpot43, lblSpot44, lblSpot45, lblSpot46, lblSpot47, lblSpot48, lblSpot49, lblSpot50,
                lblSpot51, lblSpot52, lblSpot53, lblSpot54, lblSpot55, lblSpot56, lblSpot57, lblSpot58, lblSpot59, lblSpot60,
                lblSpot61, lblSpot62, lblSpot63, lblSpot64, lblSpot65, lblSpot66, lblSpot67, lblSpot68, lblSpot69, lblSpot70,
                lblSpot71, lblSpot72, lblSpot73, lblSpot74, lblSpot75, lblSpot76, lblSpot77, lblSpot78, lblSpot79, lblSpot80,
                lblSpot81, lblSpot82, lblSpot83, lblSpot84, lblSpot85, lblSpot86, lblSpot87, lblSpot88, lblSpot89, lblSpot90,
                lblSpot91, lblSpot92, lblSpot93, lblSpot94, lblSpot95, lblSpot96, lblSpot97, lblSpot98, lblSpot99, lblSpot100, lblSpot101
            };
            lstspots.ForEach(l =>
            {
                Spot spot = game.Spots[lstspots.IndexOf(l)];
                l.DataBindings.Add("Text", spot, "SpotPlayerDescription");
            });
            start = startgame;
            //lblPlayer2.Text = "Player " + PlayerEnum.B;
        }


        private void DoTurn()
        {
            game.DoTurn();
            picCardPlayer1.ImageLocation = imagepath + game.PlayingCard + ".jpg";
            picCardPlayer2.ImageLocation = imagepath + game.PreviousCard + ".jpg";
            picDicePlayer1.ImageLocation = imagepath + "dice" + game.DiceValue + ".jpg";
            picDicePlayer2.ImageLocation = imagepath + "dice" + game.PreviousDice + ".jpg";
        }

        private void RollDice()
        {
            game.RollDice();
            picDicePlayer1.ImageLocation = imagepath + "dice" + game.DiceValue + ".jpg";
            picDicePlayer2.ImageLocation = imagepath + "dice" + game.PreviousDice + ".jpg";
        }

        private void AddPlayersToGame()
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f.GetType() == typeof(frmSettings))
                {
                    frmSettings frm = (frmSettings)f;
                    game.AddPlayer(new() { PlayerName = frm.player1name, PlayingPiece = frm.player1piece });
                    game.AddPlayer(new() { PlayerName = frm.player2name, PlayingPiece = frm.player2piece });
                }
            }
        }

        private void StartGame()
        {
            //game.AddPlayer(new() { PlayerName = "Sam", PlayingPiece = "I" });
            //game.AddPlayer(new() { PlayerName = "Mike", PlayingPiece = "J" });
            AddPlayersToGame();
            game.StartGame(optPlayComputer.Checked, optModeCardOnly.Checked ? GameModeEnum.CardOnly : GameModeEnum.DiceWithRandomCard);
            
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
            if (this.MdiParent != null && this.MdiParent is frmMain)
            {
                ((frmMain)this.MdiParent).OpenForm(typeof(frmSettings), start, optMultiplePlayers.Checked);
            }
            if (start)
            {
                StartGame();
            }
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
            DoTurn();
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
