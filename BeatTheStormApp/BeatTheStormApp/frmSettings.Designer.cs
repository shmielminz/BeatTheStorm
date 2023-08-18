namespace BeatTheStormApp
{
    partial class frmSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tblMain = new TableLayoutPanel();
            lblPlayer1Name = new Label();
            lblPlayer1Piece = new Label();
            txtPlayer1Name = new TextBox();
            lstPlayer1Piece = new ComboBox();
            lblPlayer2Name = new Label();
            lblPlayer2Piece = new Label();
            txtPlayer2Name = new TextBox();
            lstPlayer2Piece = new ComboBox();
            btnStart = new Button();
            tblMain.SuspendLayout();
            SuspendLayout();
            // 
            // tblMain
            // 
            tblMain.ColumnCount = 2;
            tblMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblMain.Controls.Add(lblPlayer1Name, 0, 0);
            tblMain.Controls.Add(lblPlayer1Piece, 0, 1);
            tblMain.Controls.Add(txtPlayer1Name, 1, 0);
            tblMain.Controls.Add(lstPlayer1Piece, 1, 1);
            tblMain.Controls.Add(txtPlayer2Name, 1, 2);
            tblMain.Controls.Add(lstPlayer2Piece, 1, 3);
            tblMain.Controls.Add(lblPlayer2Name, 0, 2);
            tblMain.Controls.Add(lblPlayer2Piece, 0, 3);
            tblMain.Controls.Add(btnStart, 0, 4);
            tblMain.Dock = DockStyle.Fill;
            tblMain.Location = new Point(0, 0);
            tblMain.Name = "tblMain";
            tblMain.RowCount = 5;
            tblMain.RowStyles.Add(new RowStyle());
            tblMain.RowStyles.Add(new RowStyle());
            tblMain.RowStyles.Add(new RowStyle());
            tblMain.RowStyles.Add(new RowStyle());
            tblMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblMain.Size = new Size(1200, 684);
            tblMain.TabIndex = 0;
            // 
            // lblPlayer1Name
            // 
            lblPlayer1Name.AutoSize = true;
            lblPlayer1Name.Dock = DockStyle.Fill;
            lblPlayer1Name.Location = new Point(3, 0);
            lblPlayer1Name.Name = "lblPlayer1Name";
            lblPlayer1Name.Size = new Size(594, 51);
            lblPlayer1Name.TabIndex = 0;
            lblPlayer1Name.Text = "Player 1 Name";
            // 
            // lblPlayer1Piece
            // 
            lblPlayer1Piece.AutoSize = true;
            lblPlayer1Piece.Dock = DockStyle.Fill;
            lblPlayer1Piece.Location = new Point(3, 51);
            lblPlayer1Piece.Name = "lblPlayer1Piece";
            lblPlayer1Piece.Size = new Size(594, 52);
            lblPlayer1Piece.TabIndex = 1;
            lblPlayer1Piece.Text = "Player 1 Playing Piece";
            // 
            // txtPlayer1Name
            // 
            txtPlayer1Name.Dock = DockStyle.Fill;
            txtPlayer1Name.Location = new Point(603, 3);
            txtPlayer1Name.Name = "txtPlayer1Name";
            txtPlayer1Name.Size = new Size(594, 45);
            txtPlayer1Name.TabIndex = 2;
            // 
            // lstPlayer1Piece
            // 
            lstPlayer1Piece.Dock = DockStyle.Fill;
            lstPlayer1Piece.FormattingEnabled = true;
            lstPlayer1Piece.Location = new Point(603, 54);
            lstPlayer1Piece.Name = "lstPlayer1Piece";
            lstPlayer1Piece.Size = new Size(594, 46);
            lstPlayer1Piece.TabIndex = 3;
            // 
            // lblPlayer2Name
            // 
            lblPlayer2Name.AutoSize = true;
            lblPlayer2Name.Dock = DockStyle.Fill;
            lblPlayer2Name.Location = new Point(3, 103);
            lblPlayer2Name.Name = "lblPlayer2Name";
            lblPlayer2Name.Size = new Size(594, 51);
            lblPlayer2Name.TabIndex = 4;
            lblPlayer2Name.Text = "Player 2 Name";
            // 
            // lblPlayer2Piece
            // 
            lblPlayer2Piece.AutoSize = true;
            lblPlayer2Piece.Dock = DockStyle.Fill;
            lblPlayer2Piece.Location = new Point(3, 154);
            lblPlayer2Piece.Name = "lblPlayer2Piece";
            lblPlayer2Piece.Size = new Size(594, 52);
            lblPlayer2Piece.TabIndex = 5;
            lblPlayer2Piece.Text = "Player 2 Playing Piece";
            // 
            // txtPlayer2Name
            // 
            txtPlayer2Name.Dock = DockStyle.Fill;
            txtPlayer2Name.Location = new Point(603, 106);
            txtPlayer2Name.Name = "txtPlayer2Name";
            txtPlayer2Name.Size = new Size(594, 45);
            txtPlayer2Name.TabIndex = 6;
            // 
            // lstPlayer2Piece
            // 
            lstPlayer2Piece.Dock = DockStyle.Fill;
            lstPlayer2Piece.FormattingEnabled = true;
            lstPlayer2Piece.Location = new Point(603, 157);
            lstPlayer2Piece.Name = "lstPlayer2Piece";
            lstPlayer2Piece.Size = new Size(594, 46);
            lstPlayer2Piece.TabIndex = 7;
            // 
            // btnStart
            // 
            btnStart.Anchor = AnchorStyles.Top;
            btnStart.AutoSize = true;
            tblMain.SetColumnSpan(btnStart, 2);
            btnStart.Location = new Point(495, 226);
            btnStart.Name = "btnStart";
            btnStart.Padding = new Padding(10);
            btnStart.Size = new Size(210, 82);
            btnStart.TabIndex = 8;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = true;
            // 
            // frmSettings
            // 
            AutoScaleDimensions = new SizeF(15F, 38F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1200, 684);
            Controls.Add(tblMain);
            Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            Margin = new Padding(4, 5, 4, 5);
            Name = "frmSettings";
            Text = "Settings";
            tblMain.ResumeLayout(false);
            tblMain.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tblMain;
        private Label lblPlayer1Name;
        private Label lblPlayer1Piece;
        private TextBox txtPlayer1Name;
        private ComboBox lstPlayer1Piece;
        private TextBox txtPlayer2Name;
        private ComboBox lstPlayer2Piece;
        private Label lblPlayer2Name;
        private Label lblPlayer2Piece;
        private Button btnStart;
    }
}