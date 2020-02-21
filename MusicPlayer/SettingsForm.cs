using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using MusicPlayer.Classes;

namespace MusicPlayer
{
    public partial class SettingsForm : Form
    {
        public static string DefaultPath = string.Empty;
        public static string FileName = "DefaultSearchPath";
        public static string CSVFileName = string.Empty;
        public static string SongPath = string.Empty;
        public static bool OnlyPaths = false;

        private readonly PictureBox _titleBar = new PictureBox();
        private readonly PictureBox _closeForm = new PictureBox();

        private bool _drag = false;
        private Point _startPoint = new Point(0, 0);

        public SettingsForm()
        {
            InitializeComponent();
            InitializeStyles();
            SetTitleBar();
            CenterToScreen();

            this.txtDefaultPath.Text = GetDefaultPath();
            OnlyPaths = this.checkFileNames.Checked;
        }

        private void SetTitleBar()
        {
            this.FormBorderStyle = FormBorderStyle.None;

            this._titleBar.Location = this.Location;
            this._titleBar.Width = this.Width;
            this._titleBar.Height = 20;
            this._titleBar.BackColor = Color.FromArgb(10, 11, 12);
            this._titleBar.Image = Image.FromFile(@"Assets\Headers\Settings.png");
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

        private void btnBrowsePath_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog folderDialog = new CommonOpenFileDialog
            {
                IsFolderPicker = true,
                InitialDirectory = DefaultPath
            };

            if (folderDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                DefaultPath = folderDialog.FileName;
                txtDefaultPath.Text = DefaultPath;
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
                    string s = string.Empty;
                    if ((s = sr.ReadLine()) != null)
                        return s;
                    return s;
                }
            }
            catch
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
            catch
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
            this.BackColor = Color.FromArgb(0, 0, 0);
            this.lblDefaultPath.ForeColor = Color.White;
            this.lblCreateDB.ForeColor = Color.White;
            this.lblEditSongID3.ForeColor = Color.White;

            this.btnBrowsePath.FlatStyle = FlatStyle.Flat;
            this.btnBrowsePath.FlatAppearance.MouseOverBackColor = Color.Transparent;
            this.btnBrowsePath.FlatAppearance.MouseDownBackColor = Color.Transparent;
            this.btnBrowsePath.FlatAppearance.BorderSize = 0;
            this.btnBrowsePath.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);

            this.btnBrowseSongs.FlatStyle = FlatStyle.Flat;
            this.btnBrowseSongs.FlatAppearance.MouseOverBackColor = Color.Transparent;
            this.btnBrowseSongs.FlatAppearance.MouseDownBackColor = Color.Transparent;
            this.btnBrowseSongs.FlatAppearance.BorderSize = 0;
            this.btnBrowseSongs.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);

            this.btnOpenSong.FlatStyle = FlatStyle.Flat;
            this.btnOpenSong.FlatAppearance.MouseOverBackColor = Color.Transparent;
            this.btnOpenSong.FlatAppearance.MouseDownBackColor = Color.Transparent;
            this.btnOpenSong.FlatAppearance.BorderSize = 0;
            this.btnOpenSong.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);

            this.btnSave.FlatStyle = FlatStyle.Flat;
            this.btnSave.FlatAppearance.MouseOverBackColor = Color.Transparent;
            this.btnSave.FlatAppearance.MouseDownBackColor = Color.Transparent;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);

            this.btnEdit.FlatStyle = FlatStyle.Flat;
            this.btnEdit.FlatAppearance.MouseOverBackColor = Color.Transparent;
            this.btnEdit.FlatAppearance.MouseDownBackColor = Color.Transparent;
            this.btnEdit.FlatAppearance.BorderSize = 0;
            this.btnEdit.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);

            this.txtDefaultPath.BackColor = Color.FromArgb(255, (byte)35, (byte)35, (byte)35);
            this.txtDefaultPath.ForeColor = Color.White;
            this.txtDefaultPath.BorderStyle = BorderStyle.FixedSingle;

            this.txtDBName.BackColor = Color.FromArgb(255, (byte)35, (byte)35, (byte)35);
            this.txtDBName.ForeColor = Color.White;
            this.txtDBName.BorderStyle = BorderStyle.FixedSingle;

            this.txtSongPath.BackColor = Color.FromArgb(255, (byte)35, (byte)35, (byte)35);
            this.txtSongPath.ForeColor = Color.White;
            this.txtSongPath.BorderStyle = BorderStyle.FixedSingle;
        }

        private void txtDBName_Click(object sender, EventArgs e)
        {
            if (this.txtDBName.Text == "Database Name") { this.txtDBName.Text = string.Empty; }
        }

        private void btnBrowseSongs_Click(object sender, EventArgs e)
        {
            CSVFileName = $"{this.txtDBName.Text}.csv";
            CSV.CreateCSV();
        }

        private void btnOpenSong_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog
            {
                InitialDirectory = DefaultPath
            };

            if (open.ShowDialog() == DialogResult.OK)
            {
                SongPath = open.FileName;
                txtSongPath.Text = SongPath;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSongPath.Text))
            {
                ID3EditForm iD3 = new ID3EditForm();
                iD3.Show();
            }
            else
                MessageBox.Show("Please choose an MP3 file first!");
        }

        private void checkFileNames_CheckedChanged(object sender, EventArgs e)
        {
            OnlyPaths = checkFileNames.Checked;
        }
    }
}
