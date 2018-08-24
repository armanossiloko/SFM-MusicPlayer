using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TagLib;

namespace MusicPlayer
{
    public partial class ID3EditForm : Form
    {
        MemoryStream ms;
        TagLib.File file;

        Image Exit = Image.FromFile(@"..\..\Assets\Close.png");

        private PictureBox TitleBar = new PictureBox();
        private PictureBox CloseForm = new PictureBox();

        private bool Drag = false;
        private Point StartPoint = new Point(0, 0);

        public ID3EditForm()
        {
            InitializeComponent();
            InitializeStyles();
            SetTitleBar();
            this.CenterToScreen();

            ms = new MemoryStream();
            file = TagLib.File.Create(SettingsForm.SongPath);
            GetImage();

            try
            {
                Mp3Lib.Mp3File mp3 = new Mp3Lib.Mp3File(SettingsForm.SongPath);
                this.txtTitle.Text = mp3.TagHandler.Title;
                this.txtAlbum.Text = mp3.TagHandler.Album;
                this.txtArtist.Text = mp3.TagHandler.Artist;
            }
            catch (Exception e)
            {
                
            }
            
        }

        private void InitializeStyles()
        {
            this.BackColor = System.Drawing.Color.FromArgb(0, 0, 0);

            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblAlbum.ForeColor = System.Drawing.Color.White;
            this.lblArtist.ForeColor = System.Drawing.Color.White;

            this.txtTitle.BackColor = Color.FromArgb(255, (byte)35, (byte)35, (byte)35);
            this.txtTitle.ForeColor = System.Drawing.Color.White;
            this.txtTitle.BorderStyle = BorderStyle.FixedSingle;

            this.txtAlbum.BackColor = Color.FromArgb(255, (byte)35, (byte)35, (byte)35);
            this.txtAlbum.ForeColor = System.Drawing.Color.White;
            this.txtAlbum.BorderStyle = BorderStyle.FixedSingle;

            this.txtArtist.BackColor = Color.FromArgb(255, (byte)35, (byte)35, (byte)35);
            this.txtArtist.ForeColor = System.Drawing.Color.White;
            this.txtArtist.BorderStyle = BorderStyle.FixedSingle;

            this.btnBrowseSong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseSong.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnBrowseSong.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnBrowseSong.FlatAppearance.BorderSize = 0;
            this.btnBrowseSong.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 255, 255, 255);

            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 255, 255, 255);
        }

        private void SetTitleBar()
        {
            this.FormBorderStyle = FormBorderStyle.None;

            this.TitleBar.Location = this.Location;
            this.TitleBar.Width = this.Width;
            this.TitleBar.Height = 20;
            this.TitleBar.BackColor = Color.FromArgb(10, 11, 12);
            this.TitleBar.Image = Image.FromFile(@"..\..\Assets\Headers\ID3Edit.png");
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

        private void btnBrowseSong_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "JPEG Files|*.jpeg" /*+ "|All Files|*.*"*/;

            if (open.ShowDialog() == DialogResult.OK)
            {
                Image currentImage = new Bitmap(open.FileName);

                TagLib.Picture pic = new TagLib.Picture();
                pic.Type = TagLib.PictureType.FrontCover;
                pic.Description = "Cover";
                pic.MimeType = System.Net.Mime.MediaTypeNames.Image.Jpeg;

                currentImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                ms.Position = 0;
                pic.Data = TagLib.ByteVector.FromStream(ms);
                file.Tag.Pictures = new TagLib.IPicture[] { pic };

                this.picAlbum.Image = new Bitmap(currentImage, this.picAlbum.Size);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //file.Save();
            ms.Close();

            Mp3Lib.Mp3File mp3 = new Mp3Lib.Mp3File(SettingsForm.SongPath);
            mp3.TagHandler.Title = txtTitle.Text;
            mp3.TagHandler.Album = txtAlbum.Text;
            mp3.TagHandler.Artist = txtArtist.Text;

            file.Tag.Title = txtTitle.Text;
            file.Tag.Album = txtAlbum.Text;
            file.Tag.Artists = new string[] { txtArtist.Text };
            file.Save();
        }

        void GetImage()
        {
            try
            {
                MemoryStream mems = new MemoryStream(file.Tag.Pictures[0].Data.Data);
                System.Drawing.Image image = System.Drawing.Image.FromStream(mems);
                this.picAlbum.Image = new Bitmap(image, this.picAlbum.Size);
            }
            catch (Exception e)
            {
                
            }
        }
    }
}
