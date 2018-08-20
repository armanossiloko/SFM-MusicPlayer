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

        Image Save = Image.FromFile("..\\..\\Assets\\Save.png");

        public SettingsForm()
        {
            InitializeComponent();
            InitializeStyles();
            this.CenterToScreen();
            this.txtDefaultPath.Text = GetDefaultPath();
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
            ID3EditForm iD3 = new ID3EditForm();
            iD3.Show();
        }
    }
}
