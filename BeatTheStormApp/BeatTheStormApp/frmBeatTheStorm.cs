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
            foreach (Label l in tblGame.Controls)
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
        }

        private void SwitchPlayer()
        {
            if (player == PlayerEnum.A)
            {
                player = PlayerEnum.B;
                lblDiceOrCardPlayer1.Text = "Player 1 ";
                lblDiceOrCardPlayer1.Text += gamemode == GameModeEnum.CardOnly ? "Pick a Card" : "Throw the Dice";
                lblDiceOrCardPlayer2.Text = "Player 1 turn";
            }
            else
            {
                player = PlayerEnum.A;
                if (playermode == PlayerModeEnum.TwoPlayers)
                {
                    lblDiceOrCardPlayer2.Text = "Player 2 ";
                    lblDiceOrCardPlayer2.Text += gamemode == GameModeEnum.CardOnly ? "Pick a Card" : "Throw the Dice";
                    lblDiceOrCardPlayer1.Text = "Player 2 turn";
                }
            }
        }

        private int GetRandomCard()
        {
            Random rnd = new(DateTime.Now.Millisecond);
            int num = rnd.Next(0, dctcards.Count());
            return num;
        }

        private void DoTurn()
        {
            if (gamestatus == GameStatusEnum.Playing)
            {
                PlayerEnum currentplayer = player;
                if (gamemode == GameModeEnum.CardOnly)
                {
                    dice = new Random().Next(1, 7);
                }
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
                Label? lbl = lblspots.FirstOrDefault(l => l.Text.Contains(currentplayer.ToString()));
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
                                l.Text += currentplayer.ToString();
                                lbl.Text = lbl.Text.Replace(currentplayer.ToString(), "");
                            }
                        });
                        if (!lblspots.Any(l => l.Name == newspot))
                        {
                            if (d > 101 && d <= 106 && lbl.Name != "lblSpot101")
                            {
                                lblSpot101.Text += currentplayer.ToString();
                                lbl.Text = lbl.Text.Replace(currentplayer.ToString(), "");
                            }
                            else if (d < 1 && lbl.Name != "lblSpot1")
                            {
                                lblSpot1.Text += currentplayer.ToString();
                                lbl.Text = lbl.Text.Replace(currentplayer.ToString(), "");
                            }
                            else if (lbl.Name == "lblSpot101")
                            {
                                lblStatus.Text = "Winner is " + currentplayer.ToString();
                                gamestatus = GameStatusEnum.Winner;
                            }
                            else if (lbl.Name == "lblSpot1")
                            {
                                lblStatus.Text = "Loser is " + currentplayer.ToString();
                                gamestatus = GameStatusEnum.Loser;
                            }
                        }
                    }
                }
            }
            if (gamestatus == GameStatusEnum.Playing)
            {
                if (player == PlayerEnum.A)
                {
                    lblDiceOrCardPlayer1.Text = "Player 2 turn";
                    lblDiceOrCardPlayer2.Text = "Player 2 ";
                    lblDiceOrCardPlayer2.Text += gamemode == GameModeEnum.DiceWithRandomCard ? "Throw the Dice" : "Pick a Card";
                }
                else
                {
                    lblDiceOrCardPlayer2.Text = "Player 1 turn";
                    lblDiceOrCardPlayer1.Text = "Player 1 ";
                    lblDiceOrCardPlayer1.Text += gamemode == GameModeEnum.DiceWithRandomCard ? "Throw the Dice" : "Pick a Card";
                }
            }
            if (playermode == PlayerModeEnum.PlayAgainstComputer && player == PlayerEnum.A && gamestatus == GameStatusEnum.Playing)
            {
                RollDice();
                if (gamemode == GameModeEnum.CardOnly)
                {
                    SwitchPlayer();
                }
                DoTurn();
            }
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
                }
                else if (player == PlayerEnum.B)
                {
                    picDicePlayer2.ImageLocation = imagepath + "dice" + dice.ToString() + ".jpg";
                }
                dicerolled = true;
                if (playermode == PlayerModeEnum.TwoPlayers || player == PlayerEnum.A)
                {
                    if (player == PlayerEnum.A)
                    {
                        lblDiceOrCardPlayer1.Text = "Player 1 Pick a Card";
                        lblDiceOrCardPlayer2.Text = "Player 1 turn";
                    }
                    else if (player == PlayerEnum.B)
                    {
                        lblDiceOrCardPlayer2.Text = "Player 2 Pick a Card";
                        lblDiceOrCardPlayer1.Text = "Player 2 turn";
                    }
                }
            }
        }

        private void StartGame()
        {
            gamemode = optModeCardOnly.Checked ? GameModeEnum.CardOnly : GameModeEnum.DiceWithRandomCard;
            playermode = optMultiplePlayers.Checked ? PlayerModeEnum.TwoPlayers : PlayerModeEnum.PlayAgainstComputer;
            lblStatus.Text = gamemode == GameModeEnum.CardOnly ? "Click on card deck to pick a card." : "Throw the dice by clicking on dice";
            
            lblDiceOrCardPlayer1.Text = "Player 1 ";
            lblDiceOrCardPlayer1.Text += gamemode == GameModeEnum.CardOnly ? "Pick a Card" : "Throw the Dice";
            lblDiceOrCardPlayer2.Text = "Player 1 turn";
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
                RadioButton o = (RadioButton)sender;
                o.Checked = false;
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
            lblStatus.Text = "Choose playing mode and click start";
            lblspots.ForEach(l => l.Text = "");
            lblSpot51.Text = "AB";
            gamestatus = GameStatusEnum.NotStarted;
            picDicePlayer1.ImageLocation = null;
            picCardPlayer1.ImageLocation = null;
            picDicePlayer2.Image = null;
            picCardPlayer2.ImageLocation = null;
            lblDiceOrCardPlayer1.Text = "";
            lblDiceOrCardPlayer2.Text = "";
        }
    }
}
