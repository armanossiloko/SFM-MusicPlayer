using Mp3Lib;
using MusicPlayer.Classes;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using WMPLib;

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
        private PlayState _state;
        private readonly WindowsMediaPlayer _choose;
        private int _songCount;

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

            _choose = new WindowsMediaPlayer();

            _choose.PlayStateChange += new _WMPOCXEvents_PlayStateChangeEventHandler(Player_PlayStateChange);
            _choose.MediaError += new _WMPOCXEvents_MediaErrorEventHandler(Player_MediaError);

            UseCustomFont(@"Assets\Fonts\segoeui.ttf", 18, this.lblTitle);
            UseCustomFont(@"Assets\Fonts\segoeui.ttf", 12, this.lblAlbum);
            UseCustomFont(@"Assets\Fonts\segoeui.ttf", 14, this.lblPerformer);

            _songCount = 0;

            this.picAlbum.Image = Buttons.DefaultAlbumArt;

            CSVFilesList = false;

            this.volumeSlider.Scroll += new ScrollEventHandler(volumeSlider_Scroll);
            this.volumeSlider.DockChanged += volumeSlider_DockChanged;

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
            this.TitleBar.Image = Image.FromFile(@"Assets\Headers\Header.png");
            this.Controls.Add(this.TitleBar);

            this.CloseForm.Width = 20;
            this.CloseForm.Height = 20;
            this.CloseForm.Image = new Bitmap(Buttons.Exit, this.CloseForm.Size);
            this.CloseForm.Location = new Point(this.Width - this.CloseForm.Width, 0);
            this.CloseForm.ForeColor = Color.Gray;
            this.CloseForm.BackColor = Color.FromArgb(10, 11, 12);
            this.Controls.Add(this.CloseForm);
            this.CloseForm.MouseEnter += new EventHandler(Control_MouseEnter);
            this.CloseForm.MouseLeave += new EventHandler(Control_MouseLeave);
            this.CloseForm.MouseClick += new MouseEventHandler(Control_MouseClick);

            this.MinimizeForm.Width = 20;
            this.MinimizeForm.Height = 20;
            this.MinimizeForm.Image = new Bitmap(Buttons.Minimize, this.MinimizeForm.Size);
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
                SFMAppContext.TrayIcon.Visible = false;
                Application.Exit();
            }
            else if (sender.Equals(this.MinimizeForm))
            {
                this.Hide();
            }
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
                songProgressBar.Value = (int)this._choose.controls.currentPosition;
                this.lblCurrentMark.Text = _choose.controls.currentPositionString;
            }
            catch
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
            this.btnShuffle.Image = Buttons.ShuffleOff;
            this.btnActivation.Image = Buttons.Play;
        }

        private void PlayPause()
        {
            if (OriginalMusic != null && OriginalMusic.Any())
            {
                if (IsPlaying == false)
                {
                    IsPlaying = true;
                    btnActivation.Image = Buttons.Pause;
                    _choose.controls.play();
                }
                else
                {
                    IsPlaying = false;
                    btnActivation.Image = Buttons.Play;
                    _choose.controls.pause();
                }
            }
        }

        private void SelectSong()
        {
            OpenFileDialog Open = new OpenFileDialog
            {
                Filter = "MP3 Files|*.mp3|CSV Files|*.csv" /*+ "|All Files|*.*"*/,
                InitialDirectory = SettingsForm.Path(),
                Multiselect = true
            };


            if (Open.ShowDialog() == DialogResult.OK)
            {
                if (Path.GetExtension(Open.FileName) == ".csv")
                {
                    if (CSV.Import(Open.FileName, ',').ElementAt(0).Length != 1)
                    {
                        _state = PlayState.CSVPlaylist;

                        PlaylistPath = Open.FileName;
                        GlobalPath = Path.GetDirectoryName(PlaylistPath) + "\\";
                        OriginalSongDetails = Songs(PlaylistPath); //Take full path to .csv and return values
                        SongDetails = new List<string[]>(OriginalSongDetails);
                        OriginalMusic = new List<string>(Filenames()); //List of direct paths to each song
                        Music = new List<string>(OriginalMusic);


                        CurrentSong = 0;
                        _songCount = Music.Count();
                        string firstSong = Music[CurrentSong]; //Artist - Album.mp3


                        _choose.URL = firstSong;
                        SetDetails(CurrentSong);
                        _choose.controls.stop();

                        lblDuration.Text = _choose.currentMedia.durationString;

                        InitializeDefaultSettings();
                    }
                    else if (CSV.Import(Open.FileName, ',').ElementAt(0).Length == 1) // NOT TESTED
                    {
                        _state = PlayState.CSVFilePlaylist;

                        SongDetails = null;
                        GlobalPath = null;
                        List<string[]> vs = CSV.Import(Open.FileName, ',');

                        OriginalMusic = new List<string>();
                        foreach (var item in vs)
                        {
                            OriginalMusic.Add(item[0]);
                        }
                        Music = new List<string>(OriginalMusic);


                        CurrentSong = 0;
                        _choose.URL = OriginalMusic.ElementAt(0);
                        SetDetails(OriginalMusic.ElementAt(0));

                        _choose.controls.stop();

                        lblDuration.Text = _choose.currentMedia.durationString;

                        InitializeDefaultSettings();
                    }
                }
                else //.mp3
                {
                    if (Open.FileNames.Length == 1)
                    {
                        _state = PlayState.OneSong;
                        SongDetails = null;

                        GlobalPath = Path.GetDirectoryName(Open.FileName) + "\\";
                        OriginalMusic = new List<string>(Open.FileNames);
                        Music = new List<string>(OriginalMusic);

                        CurrentSong = 0;
                        _choose.URL = Open.FileName;
                        SetDetails(Open.FileName);

                        _choose.controls.stop();

                        lblDuration.Text = _choose.currentMedia.durationString;

                        InitializeDefaultSettings();
                    }
                    else
                    {
                        _state = PlayState.MultipleSongs;
                        SongDetails = null;

                        GlobalPath = Path.GetDirectoryName(Open.FileName) + "\\";
                        OriginalMusic = new List<string>(Open.FileNames);
                        Music = new List<string>(OriginalMusic);

                        CurrentSong = 0;
                        _choose.URL = Open.FileNames[CurrentSong];
                        SetDetails(Open.FileNames[CurrentSong]);

                        _choose.controls.stop();

                        lblDuration.Text = _choose.currentMedia.durationString;

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
                vs.Add($"{GlobalPath}{item[0]} - {item[1]}.mp3");
            }
            return vs;
        }

        void SetDetails(int index)
        {
            if (_state == PlayState.MultipleSongs || _state == PlayState.CSVFilePlaylist)
            {
                SetDetails(Music.ElementAt(CurrentSong));
                return;
            }

            string performer = SongDetails.ElementAt(index)[0];
            string songName = SongDetails.ElementAt(index)[1];
            string album = SongDetails.ElementAt(index)[2];

            string newTitle;
            string newAlbum;
            string newPerformer;
            string full = Music.ElementAt(index);
            Mp3File mp3 = new Mp3File(@full);

            try
            {
                try
                {
                    TagLib.File file = TagLib.File.Create(@full);
                    MemoryStream ms = new MemoryStream(file.Tag.Pictures[0].Data.Data);
                    Image image = Image.FromStream(ms);
                    this.picAlbum.Image = new Bitmap(image, this.picAlbum.Size);
                }
                catch
                {
                    this.picAlbum.Image = Buttons.DefaultAlbumArt;
                }

                newTitle = mp3.TagHandler.Title;
                newAlbum = mp3.TagHandler.Album;
                newPerformer = mp3.TagHandler.Artist;

                if (string.IsNullOrEmpty(newTitle))
                    throw new Exception("Title is empty");
                else
                    this.lblTitle.Text = newTitle;

                if (string.IsNullOrEmpty(newPerformer))
                    throw new Exception("Performer is empty");
                else
                    this.lblPerformer.Text = newPerformer;

                if (string.IsNullOrEmpty(newAlbum))
                    throw new Exception("Album is empty");
                else
                    this.lblAlbum.Text = newAlbum;
            }
            catch
            {
                this.lblTitle.Text = songName;
                this.lblPerformer.Text = performer;
                this.lblAlbum.Text = album;
            }

            _choose.settings.volume = this.volumeSlider.Value;
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
            catch
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
                    TagLib.File file = TagLib.File.Create(@path);
                    MemoryStream ms = new MemoryStream(file.Tag.Pictures[0].Data.Data);
                    Image image = Image.FromStream(ms);
                    this.picAlbum.Image = new Bitmap(image, this.picAlbum.Size);
                }
                catch
                {
                    this.picAlbum.Image = Buttons.DefaultAlbumArt;
                }

                newTitle = mp3.TagHandler.Title;
                newAlbum = mp3.TagHandler.Album;
                newPerformer = mp3.TagHandler.Artist;

                if (string.IsNullOrEmpty(newTitle))
                    throw new Exception("Title is not applicable");
                else
                    this.lblTitle.Text = newTitle;

                if (string.IsNullOrEmpty(newPerformer))
                    throw new Exception("Performer is not applicable");
                else
                    this.lblPerformer.Text = newPerformer;

                if (string.IsNullOrEmpty(newAlbum))
                    throw new Exception("Album is not applicable");
                else
                    this.lblAlbum.Text = newAlbum;
            }
            catch
            {
                newTitle = title;
                newAlbum = album;
                newPerformer = performer;
            }
            finally
            {
                this.lblTitle.Text = title;
                this.lblPerformer.Text = performer;
                this.lblAlbum.Text = album;
            }

            _choose.settings.volume = this.volumeSlider.Value;
        }

        public static void UseCustomFont(string name, int size, Label label)
        {
            try
            {
                PrivateFontCollection modernFont = new PrivateFontCollection();
                modernFont.AddFontFile(name);
                label.Font = new Font(modernFont.Families[0], size);
            }
            catch
            {

            }
        }

        /* Media stopped event gets called twice when song ends, ultimately trying to skip 2 songs forward instead of one. */
        bool wasCalled = false;
        private void Player_PlayStateChange(int NewState)
        {
            WMPPlayState playState = (WMPPlayState)NewState;
            switch (playState)
            {
                case WMPPlayState.wmppsStopped:
                    IsPlaying = false;
                    if (wasCalled == true)
                    {
                        wasCalled = false;
                        if (_state == PlayState.OneSong)
                        {
                            if (Repeat)
                                _choose.controls.play();
                            else
                            {
                                IsPlaying = false;
                                return;
                            }
                        }
                        else
                        {
                            _choose.controls.play();
                            IsPlaying = true;
                            btnActivation.Image = Buttons.Pause;
                            return;
                        }
                    }
                    else
                    {
                        wasCalled = true;
                        NextSong();
                    }

                    break;
                case WMPPlayState.wmppsPaused:
                    IsPlaying = false;
                    btnActivation.Image = Buttons.Play;
                    break;
                case WMPPlayState.wmppsPlaying:
                    IsPlaying = true;

                    t = new Timer();
                    t.Interval = 100;
                    t.Tick += new EventHandler(t_Tick);

                    btnActivation.Image = Buttons.Pause;
                    break;
                case WMPPlayState.wmppsMediaEnded:
                    btnActivation.Image = Buttons.Play;
                    IsPlaying = false;
                    break;
            }

            if (_choose.openState == WMPOpenState.wmposMediaOpen)
            {
                songProgressBar.Maximum = (int)_choose.currentMedia.duration;
                lblDuration.Text = _choose.currentMedia.durationString;
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
                _choose.URL = Music.ElementAt(CurrentSong - 1);
                if (IsPlaying == false)
                {
                    IsPlaying = true;
                    btnActivation.Image = Buttons.Pause;
                }
                CurrentSong--;
                SetDetails(CurrentSong);
            }
        }

        void NextSong()
        {
            if (_state != PlayState.OneSong)
            {
                if (Music.Count - 1 > CurrentSong) // Not final song
                {
                    _choose.URL = Music.ElementAt(CurrentSong + 1);
                    _choose.controls.play();
                    CurrentSong++;
                    SetDetails(CurrentSong);
                }
                else //if (Music.Count - 1 == CurrentSong) // Final song
                {
                    if (Repeat)
                    {
                        CurrentSong = -1;
                        _choose.URL = Music.ElementAt(CurrentSong + 1);
                        _choose.controls.play();
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
                    _choose.controls.play();
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
            if (_state != PlayState.OneSong)
            {
                if (Shuffle) //Turned on
                {
                    Music = new List<string>(OriginalMusic);
                    if (OriginalSongDetails != null)
                        SongDetails = new List<string[]>(OriginalSongDetails);
                    this.btnShuffle.Image = Buttons.ShuffleOff;
                    Shuffle = false;
                }
                else
                {
                    ShuffleList(Music, SongDetails);
                    SetDetails(CurrentSong);

                    //To be removed
                    _choose.URL = Music.ElementAt(CurrentSong);
                    IsPlaying = true;

                    this.btnShuffle.Image = Buttons.ShuffleON;
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
                    this.btnRepeat.Image = Buttons.RepeatOff;
                    Repeat = false;
                }
                else
                {
                    this.btnRepeat.Image = Buttons.RepeatON;
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
            if (_choose.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                float x = (float)e.X / songProgressBar.Width;
                _choose.controls.currentPosition = x * _choose.currentMedia.duration;
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
            _choose.settings.volume = this.volumeSlider.Value;
        }

        private void volumeSlider_DockChanged(object sender, EventArgs e)
        {
            _choose.settings.volume = this.volumeSlider.Value;
        }
    }
}
