//using System.Drawing;
//using System.Windows.Forms;

namespace MusicPlayer
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void ChangeColors()
        {
            this.BackColor = System.Drawing.Color.FromArgb(0, 0, 0);
            this.lblTitle.BackColor = this.BackColor;
            this.lblPerformer.BackColor = this.BackColor;
            this.lblAlbum.BackColor = this.BackColor;
        }

        private void PrepareButtons()
        {
            this.btnActivation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActivation.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnActivation.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnActivation.FlatAppearance.BorderSize = 0;
            this.btnActivation.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 255, 255, 255);

            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnNext.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnNext.FlatAppearance.BorderSize = 0;
            this.btnNext.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 255, 255, 255);

            this.btnShuffle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShuffle.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnShuffle.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnShuffle.FlatAppearance.BorderSize = 0;
            this.btnShuffle.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 255, 255, 255);

            this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSettings.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSettings.FlatAppearance.BorderSize = 0;
            this.btnSettings.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 255, 255, 255);

            this.btnPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrevious.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPrevious.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPrevious.FlatAppearance.BorderSize = 0;
            this.btnPrevious.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 255, 255, 255);

            this.btnRepeat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRepeat.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnRepeat.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnRepeat.FlatAppearance.BorderSize = 0;
            this.btnRepeat.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 255, 255, 255);

            this.btnInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInfo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnInfo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnInfo.FlatAppearance.BorderSize = 0;
            this.btnInfo.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 255, 255, 255);

            this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowse.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnBrowse.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnBrowse.FlatAppearance.BorderSize = 0;
            this.btnBrowse.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 255, 255, 255);

            IsPlaying = false;
            Shuffle = false;
            Repeat = false;
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblPerformer = new System.Windows.Forms.Label();
            this.lblAlbum = new System.Windows.Forms.Label();
            this.picAlbum = new System.Windows.Forms.PictureBox();
            this.lblDuration = new System.Windows.Forms.Label();
            this.songProgressBar = new MusicPlayer.NewProgressBar();
            this.btnInfo = new MusicPlayer.Classes.NewButton();
            this.btnSettings = new MusicPlayer.Classes.NewButton();
            this.btnShuffle = new MusicPlayer.Classes.NewButton();
            this.btnNext = new MusicPlayer.Classes.NewButton();
            this.btnBrowse = new MusicPlayer.Classes.NewButton();
            this.btnRepeat = new MusicPlayer.Classes.NewButton();
            this.btnPrevious = new MusicPlayer.Classes.NewButton();
            this.btnActivation = new MusicPlayer.Classes.NewButton();
            ((System.ComponentModel.ISupportInitialize)(this.picAlbum)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.SystemColors.Desktop;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(135, 13);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(285, 45);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Title";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPerformer
            // 
            this.lblPerformer.BackColor = System.Drawing.SystemColors.Desktop;
            this.lblPerformer.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPerformer.Location = new System.Drawing.Point(135, 51);
            this.lblPerformer.Name = "lblPerformer";
            this.lblPerformer.Size = new System.Drawing.Size(285, 25);
            this.lblPerformer.TabIndex = 1;
            this.lblPerformer.Text = "Performer";
            this.lblPerformer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAlbum
            // 
            this.lblAlbum.BackColor = System.Drawing.SystemColors.Desktop;
            this.lblAlbum.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlbum.Location = new System.Drawing.Point(135, 71);
            this.lblAlbum.Name = "lblAlbum";
            this.lblAlbum.Size = new System.Drawing.Size(285, 25);
            this.lblAlbum.TabIndex = 1;
            this.lblAlbum.Text = "Album";
            this.lblAlbum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picAlbum
            // 
            this.picAlbum.ErrorImage = null;
            this.picAlbum.InitialImage = null;
            this.picAlbum.Location = new System.Drawing.Point(2, 22);
            this.picAlbum.Name = "picAlbum";
            this.picAlbum.Size = new System.Drawing.Size(116, 116);
            this.picAlbum.TabIndex = 2;
            this.picAlbum.TabStop = false;
            this.picAlbum.WaitOnLoad = true;
            // 
            // lblDuration
            // 
            this.lblDuration.AutoSize = true;
            this.lblDuration.Location = new System.Drawing.Point(389, 125);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(40, 13);
            this.lblDuration.TabIndex = 4;
            this.lblDuration.Text = "Length";
            // 
            // songProgressBar
            // 
            this.songProgressBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.songProgressBar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.songProgressBar.ForeColor = System.Drawing.Color.White;
            this.songProgressBar.Location = new System.Drawing.Point(128, 130);
            this.songProgressBar.Name = "songProgressBar";
            this.songProgressBar.Size = new System.Drawing.Size(260, 7);
            this.songProgressBar.TabIndex = 5;
            this.songProgressBar.MouseClick += new System.Windows.Forms.MouseEventHandler(this.songProgressBar_MouseClick);
            // 
            // btnInfo
            // 
            this.btnInfo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInfo.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnInfo.FlatAppearance.BorderSize = 0;
            this.btnInfo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnInfo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInfo.Image = global::MusicPlayer.Properties.Resources.About;
            this.btnInfo.Location = new System.Drawing.Point(390, 55);
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(32, 32);
            this.btnInfo.TabIndex = 0;
            this.btnInfo.UseVisualStyleBackColor = true;
            this.btnInfo.Click += new System.EventHandler(this.btnInfo_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSettings.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnSettings.FlatAppearance.BorderSize = 0;
            this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettings.Image = global::MusicPlayer.Properties.Resources.Settings;
            this.btnSettings.Location = new System.Drawing.Point(390, 95);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(32, 32);
            this.btnSettings.TabIndex = 0;
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnShuffle
            // 
            this.btnShuffle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShuffle.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnShuffle.FlatAppearance.BorderSize = 0;
            this.btnShuffle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShuffle.Image = global::MusicPlayer.Properties.Resources.Shuffle;
            this.btnShuffle.Location = new System.Drawing.Point(335, 95);
            this.btnShuffle.Name = "btnShuffle";
            this.btnShuffle.Size = new System.Drawing.Size(32, 32);
            this.btnShuffle.TabIndex = 0;
            this.btnShuffle.UseVisualStyleBackColor = true;
            this.btnShuffle.Click += new System.EventHandler(this.btnShuffle_Click);
            // 
            // btnNext
            // 
            this.btnNext.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNext.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnNext.FlatAppearance.BorderSize = 0;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Image = global::MusicPlayer.Properties.Resources.Next;
            this.btnNext.Location = new System.Drawing.Point(295, 95);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(32, 32);
            this.btnNext.TabIndex = 0;
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBrowse.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnBrowse.FlatAppearance.BorderSize = 0;
            this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowse.Image = global::MusicPlayer.Properties.Resources.Browse;
            this.btnBrowse.Location = new System.Drawing.Point(135, 95);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(32, 32);
            this.btnBrowse.TabIndex = 0;
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnRepeat
            // 
            this.btnRepeat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRepeat.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnRepeat.FlatAppearance.BorderSize = 0;
            this.btnRepeat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRepeat.Image = global::MusicPlayer.Properties.Resources.Repeat;
            this.btnRepeat.Location = new System.Drawing.Point(175, 95);
            this.btnRepeat.Name = "btnRepeat";
            this.btnRepeat.Size = new System.Drawing.Size(32, 32);
            this.btnRepeat.TabIndex = 0;
            this.btnRepeat.UseVisualStyleBackColor = true;
            this.btnRepeat.Click += new System.EventHandler(this.btnRepeat_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrevious.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnPrevious.FlatAppearance.BorderSize = 0;
            this.btnPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrevious.Image = global::MusicPlayer.Properties.Resources.Previous;
            this.btnPrevious.Location = new System.Drawing.Point(215, 95);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(32, 32);
            this.btnPrevious.TabIndex = 0;
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnActivation
            // 
            this.btnActivation.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActivation.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnActivation.FlatAppearance.BorderSize = 0;
            this.btnActivation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActivation.Image = global::MusicPlayer.Properties.Resources.Play;
            this.btnActivation.Location = new System.Drawing.Point(255, 95);
            this.btnActivation.Name = "btnActivation";
            this.btnActivation.Size = new System.Drawing.Size(32, 32);
            this.btnActivation.TabIndex = 0;
            this.btnActivation.UseVisualStyleBackColor = true;
            this.btnActivation.Click += new System.EventHandler(this.btnActivation_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.ClientSize = new System.Drawing.Size(432, 140);
            this.Controls.Add(this.songProgressBar);
            this.Controls.Add(this.lblDuration);
            this.Controls.Add(this.btnInfo);
            this.Controls.Add(this.picAlbum);
            this.Controls.Add(this.lblPerformer);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnShuffle);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.btnRepeat);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.btnActivation);
            this.Controls.Add(this.lblAlbum);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.ShowInTaskbar = false;
            this.Text = "SFM Music Player Beta";
            ((System.ComponentModel.ISupportInitialize)(this.picAlbum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Classes.NewButton btnActivation;
        private Classes.NewButton btnNext;
        private Classes.NewButton btnShuffle;
        private Classes.NewButton btnSettings;
        private Classes.NewButton btnPrevious;
        private Classes.NewButton btnRepeat;
        private Classes.NewButton btnInfo;
        private Classes.NewButton btnBrowse;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblPerformer;
        private System.Windows.Forms.Label lblAlbum;
        private System.Windows.Forms.PictureBox picAlbum;
        private System.Windows.Forms.Label lblDuration;
        private NewProgressBar songProgressBar;
    }
}

