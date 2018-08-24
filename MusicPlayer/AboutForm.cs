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
        Image PlayerLogo = Image.FromFile(@"..\..\Assets\Logo.png");
        Image Exit       = Image.FromFile(@"..\..\Assets\Close.png");
        
        private PictureBox TitleBar = new PictureBox();
        private PictureBox CloseForm = new PictureBox();

        private bool Drag = false;
        private Point StartPoint = new Point(0, 0);

        public AboutForm()
        {
            InitializeComponent();
            this.CenterToScreen();
            InitializeStyles();
            AppInfo();
            SetTitleBar();
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

        private void SetTitleBar()
        {
            this.FormBorderStyle = FormBorderStyle.None;

            this.TitleBar.Location = this.Location;
            this.TitleBar.Width = this.Width;
            this.TitleBar.Height = 20;
            this.TitleBar.BackColor = Color.FromArgb(10, 11, 12);
            this.TitleBar.Image = Image.FromFile(@"..\..\Assets\Headers\About.png");
            this.TitleBar.Location = new Point(0, 0);
            this.Controls.Add(this.TitleBar);

            this.CloseForm.Width = 20;
            this.CloseForm.Height = 20;
            this.CloseForm.Image = new Bitmap(Exit, this.CloseForm.Size);
            this.CloseForm.Location = new Point(this.Width - this.CloseForm.Width, 0);
            this.CloseForm.ForeColor = Color.Gray;
            this.CloseForm.BackColor = Color.FromArgb(10, 11, 12);
            this.Controls.Add(this.CloseForm);
            this.CloseForm.MouseEnter += new EventHandler(Control_MouseEnter);
            this.CloseForm.MouseLeave += new EventHandler(Control_MouseLeave);
            this.CloseForm.MouseClick += new MouseEventHandler(Control_MouseClick);

            this.TitleBar.MouseDown += new MouseEventHandler(Title_MouseDown);
            this.TitleBar.MouseUp += new MouseEventHandler(Title_MouseUp);
            this.TitleBar.MouseMove += new MouseEventHandler(Title_MouseMove);

            this.TitleBar.BringToFront();
            this.CloseForm.BringToFront();
        }

        private void Control_MouseEnter(object sender, EventArgs e)
        {
            if (sender.Equals(this.CloseForm))
                this.CloseForm.BackColor = Color.FromArgb(240, 71, 71);
        }

        private void Control_MouseLeave(object sender, EventArgs e)
        {
            if (sender.Equals(this.CloseForm))
                this.CloseForm.BackColor = Color.FromArgb(10, 11, 12);
        }

        private void Control_MouseClick(object sender, MouseEventArgs e)
        {
            if (sender.Equals(this.CloseForm))
                this.Close();
        }

        void Title_MouseUp(object sender, MouseEventArgs e)
        {
            this.Drag = false;
        }

        void Title_MouseDown(object sender, MouseEventArgs e)
        {
            this.StartPoint = e.Location;
            this.Drag = true;
        }

        void Title_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.Drag)
            {
                Point p1 = new Point(e.X, e.Y);
                Point p2 = this.PointToScreen(p1);
                Point p3 = new Point(p2.X - this.StartPoint.X,
                                     p2.Y - this.StartPoint.Y);
                this.Location = p3;
            }
        }

        public void AppInfo()
        {
            string AppVer = "v0.07";
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
