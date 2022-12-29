﻿using System;
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
        string imagepath = Application.StartupPath + @"\Images\";
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
        //List<string> lstcards;
        List<Dictionary<string, string>> dctcards;
        public frmBeatTheStorm()
        {
            InitializeComponent();
            btnStart.Click += BtnStart_Click;
            lblDiceValCurrent.Click += LblDiceValCurrent_Click;
            btnHelp.Click += BtnHelp_Click;
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
            //picCardCurrent.ImageLocation = imagepath + "Bing.jpg";
            //lstcards = new() {
            //    "Bing",
            //    "Facebook",
            //    "Gmail",
            //    "Google",
            //    "Instagram",
            //    "Pinterest",
            //    "Slack",
            //    "Twitter",
            //    "Whatsapp",
            //    "Youtube",
            //    "Hashomrim1",
            //    "Hashomrim2",
            //    "Hashomrim3",
            //    "Hashomrim4",
            //    "Hashomrim5",
            //    "Hashomrim6",
            //    "Hashomrim7",
            //    "Hashomrim8",
            //    "Hashomrim9",
            //    "Hashomrim10"
            //};
            dctcards = new() { 
                new() { { "Name", "Facebook" }, { "Value", "5" } },
                new() { { "Name", "Instagram" }, { "Value", "5" } },
                new() { { "Name", "Twitter" }, { "Value", "5" } },
                new() { { "Name", "Whatsapp" }, { "Value", "4" } },
                new() { { "Name", "YouTube" }, { "Value", "3" } },
                new() { { "Name", "Pinterest" }, { "Value", "3" } },
                new() { { "Name", "Bing" }, { "Value", "2" } },
                new() { { "Name", "Google" }, { "Value", "2" } },
                new() { { "Name", "Gmail" }, { "Value", "1" } },
                new() { { "Name", "Slack" }, { "Value", "1" } },
                new() { { "Name", "Hashomrim1" }, { "Value", "5" } },
                new() { { "Name", "Hashomrim2" }, { "Value", "5" } },
                new() { { "Name", "Hashomrim3" }, { "Value", "4" } },
                new() { { "Name", "Hashomrim4" }, { "Value", "3" } },
                new() { { "Name", "Hashomrim5" }, { "Value", "3" } },
                new() { { "Name", "Hashomrim6" }, { "Value", "2" } },
                new() { { "Name", "Hashomrim7" }, { "Value", "2" } },
                new() { { "Name", "Hashomrim8" }, { "Value", "2" } },
                new() { { "Name", "Hashomrim9" }, { "Value", "1" } },
                new() { { "Name", "Hashomrim10" }, { "Value", "1" } },
            };
        }

        private PlayerEnum DeterminePlayer()
        {
            if (player == PlayerEnum.A)
            {
                player = PlayerEnum.B;
            }
            else
            {
                player = PlayerEnum.A;
            }
            return player;
        }

        private int GetRandomCard()
        {
            Random rnd = new(DateTime.Now.Millisecond);
            //int num = rnd.Next(0, lstcards.Count());
            int num = rnd.Next(0, dctcards.Count());
            return num;
        }

        private void DoTurn()
        {
            if (gamestatus == GameStatusEnum.Playing)
            {
                PlayerEnum currentplayer = DeterminePlayer();
                Random rnd = new();
                int dice = rnd.Next(1, 7);
                int randomcard = GetRandomCard();
                lblDiceValPrevious.Text = lblDiceValCurrent.Text;
                picCardPrevious.ImageLocation = picCardCurrent.ImageLocation;
                //picCardCurrent.ImageLocation = imagepath + lstcards[randomcard] + ".jpg";
                picCardCurrent.ImageLocation = imagepath + dctcards[randomcard]["Name"] + ".jpg";
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
                        lblDiceValCurrent.Text = "Dice Value: " + dice.ToString();
                        //if (lstcards[randomcard].Contains("Hashomrim"))
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
                            if (d > 101 && d < 106 && lbl.Name != "lblSpot101")
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
            if (playermode == PlayerModeEnum.PlayAgainstComputer && player == PlayerEnum.A)
            {
                DoTurn();
            }
        }

        private void StartGame()
        {
            gamemode = optModeCardOnly.Checked ? GameModeEnum.CardOnly : GameModeEnum.DiceWithRandomCard;
            playermode = optMultiplePlayers.Checked ? PlayerModeEnum.TwoPlayers : PlayerModeEnum.PlayAgainstComputer;
            lblStatus.Text = gamemode == GameModeEnum.CardOnly ? "Click on card deck to pick a card." : "Throw the dice by clicking on dice";
            lblDiceValCurrent.Text = gamemode == GameModeEnum.CardOnly ? "Pick a Card" : "Throw the Dice";
            //DoTurn();
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
            if (sender != null && gamestatus != GameStatusEnum.NotStarted)
            {
                RadioButton o = (RadioButton)sender;
                o.Checked = false;
            }
        }

        private void LblDiceValCurrent_Click(object? sender, EventArgs e)
        {
            DoTurn();
        }

        private void BtnHelp_Click(object? sender, EventArgs e)
        {
            Form frmhelp = new();
            TableLayoutPanel tblhelp = new() { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 1 };
            Label lblhelp = new() { Dock = DockStyle.Fill, BackColor = Color.Aqua };
            tblhelp.Controls.Add(lblhelp);
            frmhelp.Controls.Add(tblhelp);
            frmhelp.Show();
        }
    }
}
