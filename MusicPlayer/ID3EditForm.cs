using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MusicPlayer
{
    public partial class ID3EditForm : Form
    {
        readonly MemoryStream _ms;
        private TagLib.File _file;

        private readonly PictureBox _titleBar = new PictureBox();
        private readonly PictureBox _closeForm = new PictureBox();

        private bool _drag = false;
        private Point _startPoint = new Point(0, 0);

        public ID3EditForm()
        {
            InitializeComponent();
            InitializeStyles();
            SetTitleBar();
            CenterToScreen();

            _ms = new MemoryStream();
            _file = TagLib.File.Create(SettingsForm.SongPath);
            GetImage();

            try
            {
                Mp3Lib.Mp3File mp3 = new Mp3Lib.Mp3File(SettingsForm.SongPath);
                this.txtTitle.Text = mp3.TagHandler.Title;
                this.txtAlbum.Text = mp3.TagHandler.Album;
                this.txtArtist.Text = mp3.TagHandler.Artist;
            }
            catch
            {

            }

        }

        private void InitializeStyles()
        {
            this.BackColor = Color.FromArgb(0, 0, 0);

            this.lblTitle.ForeColor = Color.White;
            this.lblAlbum.ForeColor = Color.White;
            this.lblArtist.ForeColor = Color.White;

            this.txtTitle.BackColor = Color.FromArgb(255, (byte)35, (byte)35, (byte)35);
            this.txtTitle.ForeColor = Color.White;
            this.txtTitle.BorderStyle = BorderStyle.FixedSingle;

            this.txtAlbum.BackColor = Color.FromArgb(255, (byte)35, (byte)35, (byte)35);
            this.txtAlbum.ForeColor = Color.White;
            this.txtAlbum.BorderStyle = BorderStyle.FixedSingle;

            this.txtArtist.BackColor = Color.FromArgb(255, (byte)35, (byte)35, (byte)35);
            this.txtArtist.ForeColor = Color.White;
            this.txtArtist.BorderStyle = BorderStyle.FixedSingle;

            this.btnBrowseSong.FlatStyle = FlatStyle.Flat;
            this.btnBrowseSong.FlatAppearance.MouseOverBackColor = Color.Transparent;
            this.btnBrowseSong.FlatAppearance.MouseDownBackColor = Color.Transparent;
            this.btnBrowseSong.FlatAppearance.BorderSize = 0;
            this.btnBrowseSong.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);

            this.btnSave.FlatStyle = FlatStyle.Flat;
            this.btnSave.FlatAppearance.MouseOverBackColor = Color.Transparent;
            this.btnSave.FlatAppearance.MouseDownBackColor = Color.Transparent;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }

        private void SetTitleBar()
        {
            this.FormBorderStyle = FormBorderStyle.None;

            this._titleBar.Location = this.Location;
            this._titleBar.Width = this.Width;
            this._titleBar.Height = 20;
            this._titleBar.BackColor = Color.FromArgb(10, 11, 12);
            this._titleBar.Image = Image.FromFile(@"Assets\Headers\ID3Edit.png");
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

        private void btnBrowseSong_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog
            {
                Filter = "JPEG Files|*.jpeg" /*+ "|All Files|*.*"*/
            };

            if (open.ShowDialog() == DialogResult.OK)
            {
                Image currentImage = new Bitmap(open.FileName);

                TagLib.Picture pic = new TagLib.Picture
                {
                    Type = TagLib.PictureType.FrontCover,
                    Description = "Cover",
                    MimeType = System.Net.Mime.MediaTypeNames.Image.Jpeg
                };

                currentImage.Save(_ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                _ms.Position = 0;
                pic.Data = TagLib.ByteVector.FromStream(_ms);
                _file.Tag.Pictures = new TagLib.IPicture[] { pic };

                this.picAlbum.Image = new Bitmap(currentImage, this.picAlbum.Size);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //file.Save();
            _ms.Close();

            Mp3Lib.Mp3File mp3 = new Mp3Lib.Mp3File(SettingsForm.SongPath);
            mp3.TagHandler.Title = txtTitle.Text;
            mp3.TagHandler.Album = txtAlbum.Text;
            mp3.TagHandler.Artist = txtArtist.Text;

            _file.Tag.Title = txtTitle.Text;
            _file.Tag.Album = txtAlbum.Text;
            _file.Tag.Artists = new string[] { txtArtist.Text };
            _file.Save();
        }

        void GetImage()
        {
            try
            {
                MemoryStream mems = new MemoryStream(_file.Tag.Pictures[0].Data.Data);
                Image image = Image.FromStream(mems);
                this.picAlbum.Image = new Bitmap(image, this.picAlbum.Size);
            }
            catch 
            {

            }
        }
    }
}
