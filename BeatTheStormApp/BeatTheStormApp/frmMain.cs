using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeatTheStormApp
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            this.Load += FrmMain_Load;
        }
        private bool IsFormOpen(Type formtype)
        {
            bool exists = false;
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.GetType() == formtype)
                {
                    if (formtype == typeof(frmBeatTheStorm))
                    {
                        frmBeatTheStorm f = (frmBeatTheStorm)frm;
                        f.start = true;
                    }
                    frm.Activate();
                    exists = true;
                    break;
                }
            }
            return exists;
        }
        public void OpenForm(Type frmtype, bool start = false, bool twoplayers = false)
        {
            bool exists = IsFormOpen(frmtype);
            if (!exists)
            {
                Form? newfrm = null;
                if (frmtype == typeof(frmSettings))
                {
                    frmSettings f = new(twoplayers);
                    newfrm = f;
                }
                else if (frmtype == typeof(frmBeatTheStorm))
                {
                    frmBeatTheStorm f = new(start);
                    newfrm = f;
                }
                if (newfrm != null)
                {
                    newfrm.MdiParent = this;
                    newfrm.WindowState = FormWindowState.Maximized;
                    newfrm.Show();
                }
            }
        }
        private void FrmMain_Load(object? sender, EventArgs e)
        {
            OpenForm(typeof(frmBeatTheStorm));
        }
    }
}
