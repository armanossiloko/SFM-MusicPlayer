using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;
using MusicPlayer.Classes;
using System.IO;
using System.Drawing.Text;
using Mp3Lib;
using TagLib;
using System.Runtime.InteropServices;
using ColorSlider;

namespace MusicPlayer
{
    enum PlayState { OneSong, MultipleSongs, CSVPlaylist, CSVFilePlaylist };
    public partial class MainForm : Form
    {
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        const int PlayKey = 1;
        const int NextKey = 2;
        const int PreviousKey = 3;

        PlayState State;

        WindowsMediaPlayer Choose;

        List<string> OriginalMusic;
        List<string> Music;
        List<string[]> OriginalSongDetails;
        List<string[]> SongDetails;

        string PlaylistPath;
        string GlobalPath;
        int CurrentSong;

        Timer t;

        private bool Shuffle;
        private bool Repeat;
        private bool IsPlaying;
        public static bool CSVFilesList;

        Image Play = Image.FromFile(@"..\..\Assets\Play.png");
        Image Pause = Image.FromFile(@"..\..\Assets\Pause.png");
        Image Next = Image.FromFile(@"..\..\Assets\Next.png");
        Image Previous = Image.FromFile(@"..\..\Assets\Previous.png");
        Image ShuffleOff = Image.FromFile(@"..\..\Assets\Shuffle.png");
        Image ShuffleON = Image.FromFile(@"..\..\Assets\ShuffleON.png");
        Image RepeatOff = Image.FromFile(@"..\..\Assets\Repeat.png");
        Image RepeatON = Image.FromFile(@"..\..\Assets\RepeatON.png");
        Image Settings = Image.FromFile(@"..\..\Assets\Settings.png");
        Image Browse = Image.FromFile(@"..\..\Assets\Browse.png");
        Image DefaultAlbumArt = Image.FromFile(@"..\..\Assets\AlbumArt.png");
        Image Exit = Image.FromFile(@"..\..\Assets\Close.png");
        Image Minimize = Image.FromFile(@"..\..\Assets\Minimize.png");

        private PictureBox TitleBar = new PictureBox();
        private PictureBox CloseForm = new PictureBox();
        private PictureBox MinimizeForm = new PictureBox();

        private bool Drag = false;
        private Point StartPoint = new Point(0, 0);

        public MainForm()
        {
            InitializeComponent();
            SetTitleBar();

            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width - 2, Screen.PrimaryScreen.WorkingArea.Height - this.Height - 2);

            this.ChangeColors();
            this.PrepareButtons();
            this.CurrentSong = -1;

            Choose = new WindowsMediaPlayer();

            Choose.PlayStateChange += new WMPLib._WMPOCXEvents_PlayStateChangeEventHandler(Player_PlayStateChange);
            Choose.MediaError += new WMPLib._WMPOCXEvents_MediaErrorEventHandler(Player_MediaError);

            UseCustomFont(@"..\..\Assets\Fonts\segoeui.ttf", 18, this.lblTitle);
            UseCustomFont(@"..\..\Assets\Fonts\segoeui.ttf", 12, this.lblAlbum);
            UseCustomFont(@"..\..\Assets\Fonts\segoeui.ttf", 14, this.lblPerformer);

            this.picAlbum.Image = DefaultAlbumArt;

            CSVFilesList = false;

            this.volumeSlider.Scroll += new ScrollEventHandler(volumeSlider_Scroll);

            this.volumeSlider.BarInnerColor = Color.FromArgb(87, 94, 110);
            this.volumeSlider.BarPenColorTop = Color.FromArgb(87, 94, 110);
            this.volumeSlider.BarPenColorBottom = Color.FromArgb(87, 94, 110);

            this.volumeSlider.ElapsedInnerColor = Color.White;
            this.volumeSlider.ElapsedPenColorTop = Color.White;
            this.volumeSlider.ElapsedPenColorBottom = Color.White;

            this.volumeSlider.DrawSemitransparentThumb = false;
            this.volumeSlider.Value = 50;

            RegisterHotKey(this.Handle, PlayKey, 0, (int)Keys.MediaPlayPause);
            RegisterHotKey(this.Handle, NextKey, 0, (int)Keys.MediaNextTrack);
            RegisterHotKey(this.Handle, PreviousKey, 0, (int)Keys.MediaPreviousTrack);
        }

        protected override void WndProc(ref Message msg)
        {
            if (msg.Msg == 0x0312 && msg.WParam.ToInt32() == PlayKey)
            {
                PlayPause();
            }
            else if (msg.Msg == 0x0312 && msg.WParam.ToInt32() == NextKey)
            {
                NextSong();
            }
            else if (msg.Msg == 0x0312 && msg.WParam.ToInt32() == PreviousKey)
            {
                PreviousSong();
            }
            base.WndProc(ref msg);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.O)) // Browse for songs
            {
                SelectSong();
                return true;
            }
            else if (keyData == (Keys.Control | Keys.I))
            {
                AboutForm about = new AboutForm();
                about.Show();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        void SetTitleBar()
        {
            this.FormBorderStyle = FormBorderStyle.None;

            this.TitleBar.Location = this.Location;
            this.TitleBar.Width = this.Width;
            this.TitleBar.Height = 20;
            this.TitleBar.BackColor = Color.FromArgb(10, 11, 12);
            this.TitleBar.Image = Image.FromFile(@"..\..\Assets\Headers\Header.png");
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

            this.MinimizeForm.Width = 20;
            this.MinimizeForm.Height = 20;
            this.MinimizeForm.Image = new Bitmap(Minimize, this.MinimizeForm.Size);
            this.MinimizeForm.Location = new Point(this.Width - this.CloseForm.Width - this.MinimizeForm.Width, 0);
            this.MinimizeForm.ForeColor = Color.Gray;
            this.MinimizeForm.BackColor = Color.FromArgb(10, 11, 12);
            this.Controls.Add(this.MinimizeForm);
            this.MinimizeForm.MouseEnter += new EventHandler(Control_MouseEnter);
            this.MinimizeForm.MouseLeave += new EventHandler(Control_MouseLeave);
            this.MinimizeForm.MouseClick += new MouseEventHandler(Control_MouseClick);

            this.TitleBar.MouseDown += new MouseEventHandler(Title_MouseDown);
            this.TitleBar.MouseUp += new MouseEventHandler(Title_MouseUp);
            this.TitleBar.MouseMove += new MouseEventHandler(Title_MouseMove);

            this.TitleBar.BringToFront();
            this.CloseForm.BringToFront();
            this.MinimizeForm.BringToFront();
        }

        private void Control_MouseEnter(object sender, EventArgs e)
        {
            if (sender.Equals(this.CloseForm))
                this.CloseForm.BackColor = Color.FromArgb(240, 71, 71);
            else if (sender.Equals(this.MinimizeForm))
                this.MinimizeForm.BackColor = Color.FromArgb(43, 44, 47);
        }

        private void Control_MouseLeave(object sender, EventArgs e)
        {
            if (sender.Equals(this.CloseForm))
                this.CloseForm.BackColor = Color.FromArgb(10, 11, 12);
            else if (sender.Equals(this.MinimizeForm))
                this.MinimizeForm.BackColor = Color.FromArgb(10, 11, 12);
        }

        private void Control_MouseClick(object sender, MouseEventArgs e)
        {
            if (sender.Equals(this.CloseForm))
            {
                MyAppContext.TrayIcon.Visible = false;
                Application.Exit();
            }
            else if (sender.Equals(this.MinimizeForm))
            {
                this.Hide();
            }
            //this.Close();
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

        void t_Tick(object sender, EventArgs e)
        {
            try
            {
                songProgressBar.Value = (int)this.Choose.controls.currentPosition;
                this.lblCurrentMark.Text = Choose.controls.currentPositionString;
            }
            catch (Exception ex)
            {

            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            SelectSong();
        }

        private void btnActivation_Click(object sender, EventArgs e)
        {
            PlayPause();
        }

        private void InitializeDefaultSettings()
        {
            this.Shuffle = false;
            this.IsPlaying = false;
            this.btnShuffle.Image = ShuffleOff;
            this.btnActivation.Image = Play;
        }

        private void PlayPause()
        {
            if (OriginalMusic != null)
            {
                if (IsPlaying == false)
                {
                    IsPlaying = true;
                    btnActivation.Image = Pause;
                    Choose.controls.play();
                }
                else
                {
                    IsPlaying = false;
                    btnActivation.Image = Play;
                    Choose.controls.pause();
                }
            }
        }

        private void SelectSong()
        {
            OpenFileDialog Open = new OpenFileDialog();
            Open.Filter = "MP3 Files|*.mp3|CSV Files|*.csv" /*+ "|All Files|*.*"*/;
            Open.InitialDirectory = MusicPlayer.SettingsForm.Path();
            Open.Multiselect = true;


            if (Open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (Path.GetExtension(Open.FileName) == ".csv")
                {
                    if (CSV.Import(Open.FileName, ',').ElementAt(0).Length != 1)
                    {
                        State = PlayState.CSVPlaylist;

                        PlaylistPath = Open.FileName;
                        GlobalPath = Path.GetDirectoryName(PlaylistPath) + "\\";
                        OriginalSongDetails = Songs(PlaylistPath); //Take full path to .csv and return values
                        SongDetails = new List<string[]>(OriginalSongDetails);
                        OriginalMusic = new List<string>(Filenames()); //List of direct paths to each song
                        Music = new List<string>(OriginalMusic);


                        CurrentSong = 0;
                        string firstSong = Music[CurrentSong]; //Artist - Album.mp3


                        Choose.URL = firstSong;
                        SetDetails(CurrentSong);
                        Choose.controls.stop();

                        lblDuration.Text = Choose.currentMedia.durationString;

                        InitializeDefaultSettings();
                    }
                    else if (CSV.Import(Open.FileName, ',').ElementAt(0).Length == 1) // NOT TESTED
                    {
                        State = PlayState.CSVFilePlaylist;

                        SongDetails = null;
                        GlobalPath = null;
                        List<string[]> vs = CSV.Import(Open.FileName, ',');

                        OriginalMusic = new List<string>();
                        for (int i = 0; i < vs.Count; i++)
                        {
                            OriginalMusic.Add(vs.ElementAt(i)[0]);
                        }
                        Music = new List<string>(OriginalMusic);


                        CurrentSong = 0;
                        Choose.URL = OriginalMusic.ElementAt(0);
                        SetDetails(OriginalMusic.ElementAt(0));

                        Choose.controls.stop();

                        lblDuration.Text = Choose.currentMedia.durationString;

                        InitializeDefaultSettings();
                    }
                }
                else //.mp3
                {
                    if (Open.FileNames.Length == 1)
                    {
                        State = PlayState.OneSong;
                        SongDetails = null;

                        GlobalPath = Path.GetDirectoryName(Open.FileName) + "\\";
                        OriginalMusic = new List<string>(Open.FileNames);
                        Music = new List<string>(OriginalMusic);

                        CurrentSong = 0;
                        Choose.URL = Open.FileName;
                        SetDetails(Open.FileName);

                        Choose.controls.stop();

                        lblDuration.Text = Choose.currentMedia.durationString;

                        InitializeDefaultSettings();
                    }
                    else
                    {
                        State = PlayState.MultipleSongs;
                        SongDetails = null;

                        GlobalPath = Path.GetDirectoryName(Open.FileName) + "\\";
                        OriginalMusic = new List<string>(Open.FileNames);
                        Music = new List<string>(OriginalMusic);

                        CurrentSong = 0;
                        Choose.URL = Open.FileNames[CurrentSong];
                        SetDetails(Open.FileNames[CurrentSong]);

                        Choose.controls.stop();

                        lblDuration.Text = Choose.currentMedia.durationString;

                        InitializeDefaultSettings();
                    }
                }
            }
        }

        //Take full path to .csv and return array of song details
        List<string[]> Songs(string FullPath)
        {
            List<string[]> songs = CSV.Import(FullPath, ',');
            return songs;
        }

        List<string> Filenames()
        {
            List<string> vs = new List<string>();
            foreach (var item in SongDetails)
            {
                vs.Add(GlobalPath + item[0] + " - " + item[1] + ".mp3");
            }
            return vs;
        }

        void SetDetails(int index)
        {
            if (State == PlayState.MultipleSongs)
            {
                SetDetails(Music.ElementAt(CurrentSong));
                return;
            }
            else if (State == PlayState.CSVFilePlaylist)
            {
                SetDetails(Music.ElementAt(CurrentSong));
                return;
            }

            string performer = SongDetails.ElementAt(index)[0];
            string songName = SongDetails.ElementAt(index)[1];
            string album = SongDetails.ElementAt(index)[2];
            string fileName = Path.GetFileName(Music.ElementAt(index));

            string newTitle;
            string newAlbum;
            string newPerformer;
            string full = Music.ElementAt(index);
            Mp3File mp3 = new Mp3File(@full);
            try
            {
                try
                {
                    //this.picAlbum.Image = mp3.TagHandler.Picture;
                    /*Bitmap bitmap = new Bitmap(mp3.TagHandler.Picture, this.picAlbum.Size);
                    this.picAlbum.Image = bitmap;*/

                    TagLib.File file = TagLib.File.Create(@full);
                    MemoryStream ms = new MemoryStream(file.Tag.Pictures[0].Data.Data);
                    System.Drawing.Image image = System.Drawing.Image.FromStream(ms);
                    this.picAlbum.Image = new Bitmap(image, this.picAlbum.Size);
                }
                catch (Exception x)
                {
                    this.picAlbum.Image = DefaultAlbumArt;
                }

                newTitle = mp3.TagHandler.Title;
                newAlbum = mp3.TagHandler.Album;
                newPerformer = mp3.TagHandler.Artist;

                if (newTitle == "")
                    throw new Exception("Title is empty");
                else
                    this.lblTitle.Text = newTitle;

                if (newPerformer == "")
                    throw new Exception("Performer is empty");
                else
                    this.lblPerformer.Text = newPerformer;

                if (newAlbum == "")
                    throw new Exception("Album is empty");
                else
                    this.lblAlbum.Text = newAlbum;
            }
            catch (Exception e)
            {
                this.lblTitle.Text = songName;
                this.lblPerformer.Text = performer;
                this.lblAlbum.Text = album;
            }

            Choose.settings.volume = this.volumeSlider.Value;
        }

        void SetDetails(string path)
        {
            string title, album, performer;

            try
            {
                string fileName = Path.GetFileNameWithoutExtension(path);
                int minus = fileName.LastIndexOf("-");

                title = fileName.Substring(minus + 2);
                performer = fileName.Substring(0, minus - 1);
                album = Path.GetFileName(path);
            }
            catch (Exception e)
            {
                performer = album = " ";
                title = Path.GetFileName(@path);
            }


            string newTitle;
            string newAlbum;
            string newPerformer;

            Mp3File mp3 = new Mp3File(@path);
            try
            {
                try
                {
                    //this.picAlbum.Image = mp3.TagHandler.Picture;
                    /*Bitmap bitmap = new Bitmap(mp3.TagHandler.Picture, this.picAlbum.Size);
                    this.picAlbum.Image = bitmap;*/

                    TagLib.File file = TagLib.File.Create(@path);
                    MemoryStream ms = new MemoryStream(file.Tag.Pictures[0].Data.Data);
                    System.Drawing.Image image = System.Drawing.Image.FromStream(ms);
                    this.picAlbum.Image = new Bitmap(image, this.picAlbum.Size);
                }
                catch (Exception x)
                {
                    this.picAlbum.Image = DefaultAlbumArt;
                }

                newTitle = mp3.TagHandler.Title;
                newAlbum = mp3.TagHandler.Album;
                newPerformer = mp3.TagHandler.Artist;

                if (newTitle == "")
                    throw new Exception("Title is empty");
                else
                    this.lblTitle.Text = newTitle;

                if (newPerformer == "")
                    throw new Exception("Performer is empty");
                else
                    this.lblPerformer.Text = newPerformer;

                if (newAlbum == "")
                    throw new Exception("Album is empty");
                else
                    this.lblAlbum.Text = newAlbum;

                this.lblTitle.Text = newTitle;
                this.lblPerformer.Text = newPerformer;
                this.lblAlbum.Text = newAlbum;
            }
            catch (Exception e)
            {
                newTitle = title;
                newAlbum = album;
                newPerformer = performer;
                
                this.lblTitle.Text = title;
                this.lblPerformer.Text = performer;
                this.lblAlbum.Text = album;
            }

            Choose.settings.volume = this.volumeSlider.Value;
        }

        public static void UseCustomFont(string name, int size, Label label)
        {
            try
            {
                PrivateFontCollection modernFont = new PrivateFontCollection();
                modernFont.AddFontFile(name);
                label.Font = new Font(modernFont.Families[0], size);
            }
            catch (Exception)
            {

            }
        }

        /* Media stopped event gets called twice when song ends, ultimately trying to skip 2 songs forward instead of one. */
        bool wasCalled = false; 
        private void Player_PlayStateChange(int NewState)
        {
            if ((WMPLib.WMPPlayState)NewState == WMPLib.WMPPlayState.wmppsStopped)
            {
                IsPlaying = false;
                if (wasCalled == true)
                {
                    wasCalled = false;
                    if (State == PlayState.OneSong)
                    {
                        if (Repeat)
                            Choose.controls.play();
                        else
                        {
                            IsPlaying = false;
                            return;
                        }
                    }
                    else
                    {
                        Choose.controls.play(); 
                        IsPlaying = true;
                        btnActivation.Image = Pause;
                        return;
                    }
                }
                else
                {
                    wasCalled = true;
                    NextSong();
                }
            }
            else if ((WMPLib.WMPPlayState)NewState == WMPLib.WMPPlayState.wmppsPaused)
            {
                IsPlaying = false;
                btnActivation.Image = Play;
            }
            else if ((WMPLib.WMPPlayState)NewState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                IsPlaying = true;

                t = new Timer();
                t.Interval = 100;
                t.Tick += new EventHandler(t_Tick);

                btnActivation.Image = Pause;
            }
            else if ((WMPLib.WMPPlayState)NewState == WMPLib.WMPPlayState.wmppsMediaEnded)
            {
                btnActivation.Image = Play;
                IsPlaying = false;
            }

            if (Choose.openState == WMPLib.WMPOpenState.wmposMediaOpen)
            {
                songProgressBar.Maximum = (int)Choose.currentMedia.duration;
                lblDuration.Text = Choose.currentMedia.durationString;
                t.Start();
            }
        }

        private void Player_MediaError(object pMediaObject)
        {
            MessageBox.Show("Cannot play media file.");
            //this.Close();
        }

        void PreviousSong()
        {
            if (CurrentSong > 0)
            {
                Choose.URL = Music.ElementAt(CurrentSong - 1);
                if (IsPlaying == false)
                {
                    IsPlaying = true;
                    btnActivation.Image = Pause;
                }
                CurrentSong--;
                SetDetails(CurrentSong);
            }
        }

        void NextSong()
        {
            if (State != PlayState.OneSong)
            {
                if (Music.Count - 1 > CurrentSong) // Not final song
                {
                    Choose.URL = Music.ElementAt(CurrentSong + 1);
                    Choose.controls.play();
                    CurrentSong++;
                    SetDetails(CurrentSong);
                }
                else //if (Music.Count - 1 == CurrentSong) // Final song
                {
                    if (Repeat)
                    {
                        CurrentSong = -1;
                        Choose.URL = Music.ElementAt(CurrentSong + 1);
                        Choose.controls.play();
                        CurrentSong++;
                        SetDetails(CurrentSong);
                    }
                    else
                    {
                        //IsPlaying = false;
                    }
                }
            }
            else
            {
                if (Repeat)
                    Choose.controls.play();
                else
                    IsPlaying = false; 
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            NextSong();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            PreviousSong();
        }

        private void btnShuffle_Click(object sender, EventArgs e)
        {
            if (State != PlayState.OneSong)
            {
                if (Shuffle) //Turned on
                {
                    Music = new List<string>(OriginalMusic);
                    if (OriginalSongDetails != null)
                        SongDetails = new List<string[]>(OriginalSongDetails);
                    this.btnShuffle.Image = ShuffleOff;
                    Shuffle = false;
                }
                else
                {
                    ShuffleList(Music, SongDetails);
                    this.btnShuffle.Image = ShuffleON;
                    Shuffle = true;
                }
            }
        }

        // Randomize the list
        private Random rng = new Random();
        public void ShuffleList<T>(List<T> list, List<string[]> list2 = null)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;

                if (list2 != null)
                {
                    string[] vs = list2[k];
                    list2[k] = list2[n];
                    list2[n] = vs;
                }
            }
        }
        // End Randomize List

        private void btnRepeat_Click(object sender, EventArgs e)
        {
            if (OriginalMusic != null)
            {
                if (Repeat) //Turned on
                {
                    this.btnRepeat.Image = RepeatOff;
                    Repeat = false;
                }
                else
                {
                    this.btnRepeat.Image = RepeatON;
                    Repeat = true;
                }
            }
        }

        private void CSVCreate(object sender, EventArgs e)
        {
            CSV.CreateCSV();
        }

        private void songProgressBar_MouseClick(object sender, MouseEventArgs e)
        {
            if (Choose.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                float x = (float)e.X / songProgressBar.Width;
                Choose.controls.currentPosition = x * Choose.currentMedia.duration;
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.Show();
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            AboutForm about = new AboutForm();
            about.Show();
        }

        private void volumeSlider_Scroll(object sender, ScrollEventArgs e)
        {
            Choose.settings.volume = this.volumeSlider.Value;
        }
    }
}
