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
using Microsoft.WindowsAPICodePack.Dialogs;
using MusicPlayer.Classes;

namespace MusicPlayer
{
    public partial class SettingsForm : Form
    {
        public static string DefaultPath = "";
        public static string FileName = "DefaultSearchPath";
        public static string CSVFileName = "";
        public static string SongPath = "";
        public static bool OnlyPaths = false;

        Image Save = Image.FromFile(@"..\..\Assets\Save.png");
        Image Exit = Image.FromFile(@"..\..\Assets\Close.png");

        private PictureBox TitleBar = new PictureBox();
        private PictureBox CloseForm = new PictureBox();

        private bool Drag = false;
        private Point StartPoint = new Point(0, 0);

        public SettingsForm()
        {
            InitializeComponent();
            InitializeStyles();
            SetTitleBar();
            this.CenterToScreen();
            this.txtDefaultPath.Text = GetDefaultPath();
            OnlyPaths = this.checkFileNames.Checked;
        }

        private void SetTitleBar()
        {
            this.FormBorderStyle = FormBorderStyle.None;

            this.TitleBar.Location = this.Location;
            this.TitleBar.Width = this.Width;
            this.TitleBar.Height = 20;
            this.TitleBar.BackColor = Color.FromArgb(10, 11, 12);
            this.TitleBar.Image = Image.FromFile(@"..\..\Assets\Headers\Settings.png");
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

        private void btnBrowsePath_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog folderDialog = new CommonOpenFileDialog();
            folderDialog.IsFolderPicker = true;
            folderDialog.InitialDirectory = DefaultPath;
            if (folderDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                DefaultPath = folderDialog.FileName;
                this.txtDefaultPath.Text = DefaultPath;
            }
        }

        private void SetDefaultPath()
        {
            if (File.Exists(FileName))
            {
                File.Delete(FileName);
            }
            using (FileStream fs = File.Create(FileName))
            {
                Byte[] info = new UTF8Encoding(true).GetBytes(DefaultPath);
                fs.Write(info, 0, info.Length);
            }
        }

        private string GetDefaultPath()
        {
            try
            {
                if (!File.Exists(FileName))
                    SetDefaultPath();
                using (StreamReader sr = File.OpenText(FileName))
                {
                    string s = "";
                    if ((s = sr.ReadLine()) != null)
                        return s;
                    return s;
                }
            }
            catch (Exception e)
            {
                return @"C:\";
            }
        }

        public static string Path()
        {
            try
            {
                //if (!File.Exists(FileName))
                    //SetDefaultPath();
                using (StreamReader sr = File.OpenText(FileName))
                {
                    string s = "";
                    if ((s = sr.ReadLine()) != null)
                        return s;
                    return s;
                }
            }
            catch (Exception e)
            {
                return @"C:\";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SetDefaultPath();
        }

        private void InitializeStyles()
        {
            this.BackColor = System.Drawing.Color.FromArgb(0, 0, 0);
            this.lblDefaultPath.ForeColor = System.Drawing.Color.White;
            this.lblCreateDB.ForeColor = System.Drawing.Color.White;
            this.lblEditSongID3.ForeColor = System.Drawing.Color.White;

            this.btnBrowsePath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowsePath.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnBrowsePath.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnBrowsePath.FlatAppearance.BorderSize = 0;
            this.btnBrowsePath.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 255, 255, 255);

            this.btnBrowseSongs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseSongs.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnBrowseSongs.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnBrowseSongs.FlatAppearance.BorderSize = 0;
            this.btnBrowseSongs.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 255, 255, 255);

            this.btnOpenSong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenSong.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnOpenSong.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnOpenSong.FlatAppearance.BorderSize = 0;
            this.btnOpenSong.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 255, 255, 255);

            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 255, 255, 255);

            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnEdit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnEdit.FlatAppearance.BorderSize = 0;
            this.btnEdit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 255, 255, 255);

            this.txtDefaultPath.BackColor = Color.FromArgb(255, (byte)35, (byte)35, (byte)35);
            this.txtDefaultPath.ForeColor = System.Drawing.Color.White;
            this.txtDefaultPath.BorderStyle = BorderStyle.FixedSingle;

            this.txtDBName.BackColor = Color.FromArgb(255, (byte)35, (byte)35, (byte)35);
            this.txtDBName.ForeColor = System.Drawing.Color.White;
            this.txtDBName.BorderStyle = BorderStyle.FixedSingle;

            this.txtSongPath.BackColor = Color.FromArgb(255, (byte)35, (byte)35, (byte)35);
            this.txtSongPath.ForeColor = System.Drawing.Color.White;
            this.txtSongPath.BorderStyle = BorderStyle.FixedSingle;
        }

        private void txtDBName_Click(object sender, EventArgs e)
        {
            if (this.txtDBName.Text == "Database Name") { this.txtDBName.Text = ""; }
        }

        private void btnBrowseSongs_Click(object sender, EventArgs e)
        {
            CSVFileName = this.txtDBName.Text + ".csv";
            CSV.CreateCSV();
        }

        private void btnOpenSong_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.InitialDirectory = DefaultPath;
            if (open.ShowDialog() == DialogResult.OK)
            {
                SongPath = open.FileName;
                this.txtSongPath.Text = SongPath;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtSongPath.Text != "")
            {
                ID3EditForm iD3 = new ID3EditForm();
                iD3.Show();
            }
            else
                MessageBox.Show("Please choose an MP3 file first!");
        }

        private void checkFileNames_CheckedChanged(object sender, EventArgs e)
        {
            OnlyPaths = this.checkFileNames.Checked;
        }
    }
}
