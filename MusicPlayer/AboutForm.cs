using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace MusicPlayer
{
    public partial class AboutForm : Form
    {
        private readonly PictureBox _titleBar = new PictureBox();
        private readonly PictureBox _closeForm = new PictureBox();
        private bool _drag = false;
        private Point _startPoint = new Point(0, 0);

        public AboutForm()
        {
            InitializeComponent();
            CenterToScreen();
            InitializeStyles();
            AppInfo();
            SetTitleBar();
        }

        private void InitializeStyles()
        {
            this.BackColor = Color.FromArgb(0, 0, 0);
            this.lblVersion.ForeColor = Color.White;
            this.lblVNum.ForeColor = Color.White;

            this.lblType.ForeColor = Color.White;
            this.lblForType.ForeColor = Color.White;

            this.lblCopyright.ForeColor = Color.White;
        }

        private void SetTitleBar()
        {
            this.FormBorderStyle = FormBorderStyle.None;

            this._titleBar.Location = this.Location;
            this._titleBar.Width = this.Width;
            this._titleBar.Height = 20;
            this._titleBar.BackColor = Color.FromArgb(10, 11, 12);
            this._titleBar.Image = Image.FromFile(@"Assets\Headers\About.png");
            this._titleBar.Location = new Point(0, 0);
            this.Controls.Add(this._titleBar);

            this._closeForm.Width = 20;
            this._closeForm.Height = 20;
            this._closeForm.Image = new Bitmap(Classes.Buttons.Exit, this._closeForm.Size);
            this._closeForm.Location = new Point(this.Width - this._closeForm.Width, 0);
            this._closeForm.ForeColor = Color.Gray;
            this._closeForm.BackColor = Color.FromArgb(10, 11, 12);
            this.Controls.Add(this._closeForm);
            this._closeForm.MouseEnter += new EventHandler(Control_MouseEnter);
            this._closeForm.MouseLeave += new EventHandler(Control_MouseLeave);
            this._closeForm.MouseClick += new MouseEventHandler(Control_MouseClick);

            this._titleBar.MouseDown += new MouseEventHandler(Title_MouseDown);
            this._titleBar.MouseUp += new MouseEventHandler(Title_MouseUp);
            this._titleBar.MouseMove += new MouseEventHandler(Title_MouseMove);

            this._titleBar.BringToFront();
            this._closeForm.BringToFront();
        }

        private void Control_MouseEnter(object sender, EventArgs e)
        {
            if (sender.Equals(this._closeForm))
                this._closeForm.BackColor = Color.FromArgb(240, 71, 71);
        }

        private void Control_MouseLeave(object sender, EventArgs e)
        {
            if (sender.Equals(this._closeForm))
                this._closeForm.BackColor = Color.FromArgb(10, 11, 12);
        }

        private void Control_MouseClick(object sender, MouseEventArgs e)
        {
            if (sender.Equals(this._closeForm))
                this.Close();
        }

        void Title_MouseUp(object sender, MouseEventArgs e)
        {
            this._drag = false;
        }

        void Title_MouseDown(object sender, MouseEventArgs e)
        {
            this._startPoint = e.Location;
            this._drag = true;
        }

        void Title_MouseMove(object sender, MouseEventArgs e)
        {
            if (this._drag)
            {
                Point p1 = new Point(e.X, e.Y);
                Point p2 = this.PointToScreen(p1);
                Point p3 = new Point(p2.X - this._startPoint.X,
                                     p2.Y - this._startPoint.Y);
                this.Location = p3;
            }
        }

        public void AppInfo()
        {
            string AppVer = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            string Stability = "Stable";
            string Creator = "Arman Ossi Loko";
            string Copyright = $"Copyright {Creator} © 2018 - 2020";

            this.lblVNum.Text = AppVer;
            this.lblForType.Text = Stability;
            this.lblCopyright.Text = Copyright;

            this.picLogo.Image = new Bitmap(Classes.Buttons.PlayerLogo, this.picLogo.Size);
        }
    }
}
