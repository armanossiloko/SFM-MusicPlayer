using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicPlayer
{
    public partial class AboutForm : Form
    {
        Image PlayerLogo = Image.FromFile("..\\..\\Assets\\Logo.png");

        public AboutForm()
        {
            InitializeComponent();
            this.CenterToScreen();
            this.BackColor = System.Drawing.Color.FromArgb(0, 0, 0);
            InitializeStyles();
            AppInfo();
        }

        private void InitializeStyles()
        {
            this.BackColor = System.Drawing.Color.FromArgb(0, 0, 0);
            this.lblVersion.ForeColor = System.Drawing.Color.White;
            this.lblVNum.ForeColor = System.Drawing.Color.White;

            this.lblType.ForeColor = System.Drawing.Color.White;
            this.lblForType.ForeColor = System.Drawing.Color.White;

            this.lblCopyright.ForeColor = System.Drawing.Color.White;
        }

        public void AppInfo()
        {
            string AppVer = "v0.02";
            string Stability = "Beta";
            string Creator = "Arman Ossi Loko";
            string Copyright = "Copyright " + Creator + " © 2018";

            this.lblVNum.Text = AppVer;
            this.lblForType.Text = Stability;
            this.lblCopyright.Text = Copyright;

            this.picLogo.Image = new Bitmap(PlayerLogo, this.picLogo.Size);
        }
    }
}
