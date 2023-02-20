using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeatTheStormApp
{
    public partial class frmBeatTheStorm : Form
    {
        string imagepath = Application.StartupPath + @"Images\";
        private enum PlayerEnum { A, B }
        private enum GameModeEnum { DiceWithRandomCard, CardOnly }
        private enum PlayerModeEnum { TwoPlayers, PlayAgainstComputer }
        private enum GameStatusEnum { NotStarted, Playing, Winner, Loser }
        GameStatusEnum gamestatus = GameStatusEnum.NotStarted;
        PlayerEnum player = PlayerEnum.B;
        GameModeEnum gamemode = GameModeEnum.DiceWithRandomCard;
        PlayerModeEnum playermode = PlayerModeEnum.TwoPlayers;
        List<Label> lblspots = new();
        List<RadioButton> optbtns = new();
        List<Dictionary<string, string>> dctcards;
        bool checkedhandled = false;
        bool dicerolled = false;
        int dice;
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
            foreach (Label l in tblSpots.Controls)
            {
                lblspots.Add(l);
            }
//SM In order to test the game. Comment out the different types of cards, and only leave the + or - cards, and run the game, and you'll see it gets winner and loser.
            dctcards = new() {
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
            lblPlayer1.Text = "Player " + PlayerEnum.A;
            lblPlayer2.Text = "Player " + PlayerEnum.B;
        }

        private void SwitchPlayer()
        {
            if (player == PlayerEnum.A)
            {
                player = PlayerEnum.B;
            }
            else
            {
                player = PlayerEnum.A;
            }
        }

        private int GetRandomCard()
        {
            //AF I'm just curious - why did you pass in the millisecond to Random()?  the seed value is based on the time by default
            //SM The problem was that it didn't look too random (it returned too many times the same card). So I tried it with this and it got more random.
            //AF Great, good idea
            Random rnd = new(DateTime.Now.Millisecond);
            int num = rnd.Next(0, dctcards.Count());
            return num;
        }

        private void DoTurn()
        {
            if (gamestatus == GameStatusEnum.Playing)
            {
                int randomcard = GetRandomCard();
                if (player == PlayerEnum.A)
                {
                    picCardPlayer1.ImageLocation = imagepath + dctcards[randomcard]["Name"] + ".jpg";
                }
                else if (player == PlayerEnum.B)
                {
                    picCardPlayer2.ImageLocation = imagepath + dctcards[randomcard]["Name"] + ".jpg";
                }
                if (gamemode == GameModeEnum.CardOnly)
                {
                    if (int.TryParse(dctcards[randomcard]["Value"], out int n))
                    {
                        dice = n;
                    }
                }
                Label? lbl = lblspots.FirstOrDefault(l => l.Text.Contains(player.ToString()));
                if (lbl != null)
                {
                    string s = lbl.Name.Replace("lblSpot", "");
                    if (int.TryParse(s, out int d))
                    {
                        if (dctcards[randomcard]["Name"].Contains("Hashomrim"))
                        {
                            d += dice;
                        }
                        else
                        {
                            d -= dice;
                        }
                        string newspot = "lblSpot" + d.ToString();
                        lblspots.ForEach(l =>
                        {
                            if (l.Name == newspot)
                            {
                                l.Text += player.ToString();
                                lbl.Text = lbl.Text.Replace(player.ToString(), "");
                            }
                        });
                        if (!lblspots.Any(l => l.Name == newspot))
                        {
                            if (d > 101 && d <= 106 && lbl.Name != "lblSpot101")
                            {
                                lblSpot101.Text += player.ToString();
                                lbl.Text = lbl.Text.Replace(player.ToString(), "");
                            }
                            else if (d < 1 && lbl.Name != "lblSpot1")
                            {
                                lblSpot1.Text += player.ToString();
                                lbl.Text = lbl.Text.Replace(player.ToString(), "");
                            }
                            else if (lbl.Name == "lblSpot101")
                            {
                                lblStatus.Text = "Winner is " + player.ToString();
                                gamestatus = GameStatusEnum.Winner;
                            }
                            else if (lbl.Name == "lblSpot1")
                            {
                                lblStatus.Text = "Loser is " + player.ToString();
                                gamestatus = GameStatusEnum.Loser;
                            }
                        }
                    }
                }
            }
            //AF This second if statement is identical to the one above, they can be combined
            //SM No, this makes sure only to work if it didn't change to loser or winner, and it's still playing. Maybe I can add it in other if statement but checking if it's still playing
            if (gamestatus == GameStatusEnum.Playing)
            {
                if (player == PlayerEnum.A)
                {
                    tblPlayer2.BackColor = Color.AliceBlue;
                    tblPlayer1.BackColor = DefaultBackColor;
                    lblDiceOrCardPlayer1.Text = $"Player {PlayerEnum.B} turn";
                    lblDiceOrCardPlayer2.Text = $"Player {PlayerEnum.B} ";
                    lblDiceOrCardPlayer2.Text += gamemode == GameModeEnum.DiceWithRandomCard ? "Throw the Dice" : "Pick a Card";
                }
                else
                {
                    tblPlayer1.BackColor = Color.AliceBlue;
                    tblPlayer2.BackColor = DefaultBackColor;
                    lblDiceOrCardPlayer2.Text = $"Player {PlayerEnum.A} turn";
                    lblDiceOrCardPlayer1.Text = $"Player {PlayerEnum.A} ";
                    lblDiceOrCardPlayer1.Text += gamemode == GameModeEnum.DiceWithRandomCard ? "Throw the Dice" : "Pick a Card";
                }
                if (playermode == PlayerModeEnum.PlayAgainstComputer && player == PlayerEnum.A)
                {
                    RollDice();
                    if (gamemode == GameModeEnum.CardOnly)
                    {
                        SwitchPlayer();
                    }
                    DoTurn();
                }
            }
            //if (playermode == PlayerModeEnum.PlayAgainstComputer && player == PlayerEnum.A && gamestatus == GameStatusEnum.Playing)
            //{
            //    RollDice();
            //    if (gamemode == GameModeEnum.CardOnly)
            //    {
            //        SwitchPlayer();
            //    }
            //    DoTurn();
            //}
            dicerolled = false;
        }

        private void RollDice()
        {
            if (gamemode == GameModeEnum.DiceWithRandomCard && gamestatus == GameStatusEnum.Playing && !dicerolled)
            {
                SwitchPlayer();
                dice = new Random().Next(1, 7);
                if (player == PlayerEnum.A)
                {
                    picDicePlayer1.ImageLocation = imagepath + "dice" + dice.ToString() + ".jpg";
                    lblDiceOrCardPlayer1.Text = $"Player {player} Pick a Card";
                    lblDiceOrCardPlayer2.Text = $"Player {player} turn";
                }
                else if (player == PlayerEnum.B)
                {
                    picDicePlayer2.ImageLocation = imagepath + "dice" + dice.ToString() + ".jpg";
                    lblDiceOrCardPlayer2.Text = $"Player {player} Pick a Card";
                    lblDiceOrCardPlayer1.Text = $"Player {player} turn";
                }
                dicerolled = true;
            }
        }

        private void StartGame()
        {
            gamemode = optModeCardOnly.Checked ? GameModeEnum.CardOnly : GameModeEnum.DiceWithRandomCard;
            playermode = optMultiplePlayers.Checked ? PlayerModeEnum.TwoPlayers : PlayerModeEnum.PlayAgainstComputer;
            lblStatus.Text = gamemode == GameModeEnum.CardOnly ? "Click on card deck to pick a card." : "Throw the dice by clicking on dice";

            switch (gamemode)
            {
                case GameModeEnum.DiceWithRandomCard:
                    //AF The code can be shortened by taking these first 2 statements out of this case statement and the one below, since they apply to both cases
                    //SM When taking out the card, it gets always assigned at row 3 even with tblPlayer2.Controls.Add(picCardPlayer2), and it won't update as I restart the game in cardonly mode.
                    //See my commented out code later. If you know a way to specify the row later and it should update, please let me know.
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

            //SM This is what I tried and it didn't work.

            //tblPlayer1.Controls.Add(picCardPlayer1);
            //tblPlayer2.Controls.Add(picCardPlayer2);
            //switch (gamemode)
            //{
            //    case GameModeEnum.DiceWithRandomCard:
            //        tblPlayer1.Controls.Add(picDicePlayer1, 0, 2);
            //        tblPlayer2.Controls.Add(picDicePlayer2, 0, 2);
            //        break;
            //    case GameModeEnum.CardOnly:
            //        tblPlayer1.SetRow(picCardPlayer1, 2);
            //        tblPlayer2.SetRow(picCardPlayer2, 2);
            //        tblPlayer1.SetRowSpan(picCardPlayer1, 2);
            //        tblPlayer1.SetRowSpan(picCardPlayer2, 2);
            //        break;
            //}

            lblDiceOrCardPlayer1.Text = $"Player {PlayerEnum.A} ";
            lblDiceOrCardPlayer1.Text += gamemode == GameModeEnum.CardOnly ? "Pick a Card" : "Throw the Dice";
            lblDiceOrCardPlayer2.Text = $"Player {PlayerEnum.A} turn";
            
            picCardPlayer1.ImageLocation = imagepath + "Hashomrimback.jpg";
            
            picCardPlayer2.ImageLocation = imagepath + "Hashomrimback.jpg";
            if (gamemode == GameModeEnum.DiceWithRandomCard)
            {
                picDicePlayer2.ImageLocation = imagepath + "dice1.jpg";
                picDicePlayer1.ImageLocation = imagepath + "dice1.jpg";
            }
            tblPlayer1.BackColor = Color.AliceBlue;
            tblPlayer2.BackColor = DefaultBackColor;

            if (playermode == PlayerModeEnum.PlayAgainstComputer)
            {
                picCardPlayer2.Enabled = false;
                picDicePlayer2.Enabled = false;
            }
        }

        private void RestartGame()
        {
            lblStatus.Text = "Choose playing mode and click start";
            lblspots.ForEach(l => l.Text = "");
            lblSpot51.Text = "AB";
            gamestatus = GameStatusEnum.NotStarted;
            player = PlayerEnum.B;
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
            switch (gamemode)
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
            switch (playermode)
            {
                case PlayerModeEnum.TwoPlayers:
                    optMultiplePlayers.Checked = true;
                    break;
                case PlayerModeEnum.PlayAgainstComputer:
                    optPlayComputer.Checked = true;
                    break;
                default:
                    break;
            }
        }

        private void BtnStart_Click(object? sender, EventArgs e)
        {
            if (gamestatus == GameStatusEnum.NotStarted)
            {
                gamestatus = GameStatusEnum.Playing;
                StartGame();
            }
        }

        private void Opt_CheckedChanged(object? sender, EventArgs e)
        {
            if (sender != null && gamestatus == GameStatusEnum.Playing && !checkedhandled)
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
            if (gamestatus == GameStatusEnum.Playing && (dicerolled || gamemode == GameModeEnum.CardOnly))
            {
                dicerolled = false;
                if (gamemode == GameModeEnum.CardOnly)
                {
                    SwitchPlayer();
                }
                if (player == PlayerEnum.A || playermode == PlayerModeEnum.TwoPlayers) { 
                    DoTurn();
                }
            }
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
