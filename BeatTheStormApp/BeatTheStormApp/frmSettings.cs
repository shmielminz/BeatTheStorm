using BeatTheStormSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeatTheStormApp
{
    public partial class frmSettings : Form
    {
        public string player1name = "";
        public string player2name = "";
        public string player1piece = "";
        public string player2piece = "";
        List<ComboBox> lstcombo;
        public frmSettings(bool twoplayers = true)
        {
            InitializeComponent();
            btnStart.Click += BtnStart_Click;
            if (!twoplayers)
            {
                txtPlayer2Name.Visible = false;
                lstPlayer2Piece.Visible = false;
            }
            lstcombo = new() { lstPlayer1Piece, lstPlayer2Piece };
            foreach (ComboBox lst in lstcombo)
            {
                if (lst.Visible)
                {
                    AddToDropdown(lst);
                }
            }
        }

        private void AddToDropdown(ComboBox lst)
        {
            PlayingPiece piece = new();
            lst.DataSource = piece.Piece;
        }

        private void BtnStart_Click(object? sender, EventArgs e)
        {
            player1name = txtPlayer1Name.Text;
            player2name = txtPlayer2Name.Text;
            player1piece = lstPlayer1Piece.Text;
            player2piece = lstPlayer2Piece.Text;
            if (this.MdiParent != null && this.MdiParent is frmMain)
            {
                ((frmMain)this.MdiParent).OpenForm(typeof(frmBeatTheStorm));
            }
        }
    }
}
