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

        public ID3EditForm()
        {
            InitializeComponent();
            InitializeStyles();
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
