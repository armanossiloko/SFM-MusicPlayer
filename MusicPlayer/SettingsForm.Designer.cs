namespace MusicPlayer
{
    partial class SettingsForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtDefaultPath = new System.Windows.Forms.TextBox();
            this.lblDefaultPath = new System.Windows.Forms.Label();
            this.lblCreateDB = new System.Windows.Forms.Label();
            this.txtDBName = new System.Windows.Forms.TextBox();
            this.txtSongPath = new System.Windows.Forms.TextBox();
            this.lblEditSongID3 = new System.Windows.Forms.Label();
            this.btnOpenSong = new MusicPlayer.Classes.NewButton();
            this.btnEdit = new MusicPlayer.Classes.NewButton();
            this.btnBrowseSongs = new MusicPlayer.Classes.NewButton();
            this.btnSave = new MusicPlayer.Classes.NewButton();
            this.btnBrowsePath = new MusicPlayer.Classes.NewButton();
            this.checkFileNames = new System.Windows.Forms.CheckBox();
            this.lblCheckFilenames = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtDefaultPath
            // 
            this.txtDefaultPath.BackColor = System.Drawing.SystemColors.Window;
            this.txtDefaultPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDefaultPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDefaultPath.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtDefaultPath.Location = new System.Drawing.Point(192, 25);
            this.txtDefaultPath.Name = "txtDefaultPath";
            this.txtDefaultPath.Size = new System.Drawing.Size(137, 26);
            this.txtDefaultPath.TabIndex = 2;
            // 
            // lblDefaultPath
            // 
            this.lblDefaultPath.AutoSize = true;
            this.lblDefaultPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDefaultPath.ForeColor = System.Drawing.SystemColors.Window;
            this.lblDefaultPath.Location = new System.Drawing.Point(12, 30);
            this.lblDefaultPath.Name = "lblDefaultPath";
            this.lblDefaultPath.Size = new System.Drawing.Size(157, 20);
            this.lblDefaultPath.TabIndex = 3;
            this.lblDefaultPath.Text = "Default Search Path:";
            this.lblDefaultPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCreateDB
            // 
            this.lblCreateDB.AutoSize = true;
            this.lblCreateDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreateDB.ForeColor = System.Drawing.SystemColors.Window;
            this.lblCreateDB.Location = new System.Drawing.Point(12, 70);
            this.lblCreateDB.Name = "lblCreateDB";
            this.lblCreateDB.Size = new System.Drawing.Size(172, 20);
            this.lblCreateDB.TabIndex = 5;
            this.lblCreateDB.Text = "Create CSV Database:";
            this.lblCreateDB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDBName
            // 
            this.txtDBName.BackColor = System.Drawing.SystemColors.Window;
            this.txtDBName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDBName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDBName.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtDBName.Location = new System.Drawing.Point(192, 65);
            this.txtDBName.Name = "txtDBName";
            this.txtDBName.Size = new System.Drawing.Size(137, 26);
            this.txtDBName.TabIndex = 7;
            this.txtDBName.Text = "Database Name";
            this.txtDBName.Click += new System.EventHandler(this.txtDBName_Click);
            // 
            // txtSongPath
            // 
            this.txtSongPath.BackColor = System.Drawing.SystemColors.Window;
            this.txtSongPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSongPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSongPath.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtSongPath.Location = new System.Drawing.Point(192, 134);
            this.txtSongPath.Name = "txtSongPath";
            this.txtSongPath.Size = new System.Drawing.Size(137, 26);
            this.txtSongPath.TabIndex = 11;
            // 
            // lblEditSongID3
            // 
            this.lblEditSongID3.AutoSize = true;
            this.lblEditSongID3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEditSongID3.ForeColor = System.Drawing.SystemColors.Window;
            this.lblEditSongID3.Location = new System.Drawing.Point(12, 136);
            this.lblEditSongID3.Name = "lblEditSongID3";
            this.lblEditSongID3.Size = new System.Drawing.Size(110, 20);
            this.lblEditSongID3.TabIndex = 10;
            this.lblEditSongID3.Text = "Edit ID3 Tags:";
            this.lblEditSongID3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnOpenSong
            // 
            this.btnOpenSong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOpenSong.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnOpenSong.FlatAppearance.BorderSize = 0;
            this.btnOpenSong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenSong.Image = global::MusicPlayer.Properties.Resources.Browse;
            this.btnOpenSong.Location = new System.Drawing.Point(335, 130);
            this.btnOpenSong.Name = "btnOpenSong";
            this.btnOpenSong.Size = new System.Drawing.Size(32, 32);
            this.btnOpenSong.TabIndex = 12;
            this.btnOpenSong.UseVisualStyleBackColor = true;
            this.btnOpenSong.Click += new System.EventHandler(this.btnOpenSong_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEdit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnEdit.FlatAppearance.BorderSize = 0;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Image = global::MusicPlayer.Properties.Resources.Edit;
            this.btnEdit.Location = new System.Drawing.Point(373, 130);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(32, 32);
            this.btnEdit.TabIndex = 9;
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnBrowseSongs
            // 
            this.btnBrowseSongs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBrowseSongs.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnBrowseSongs.FlatAppearance.BorderSize = 0;
            this.btnBrowseSongs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseSongs.Image = global::MusicPlayer.Properties.Resources.Browse;
            this.btnBrowseSongs.Location = new System.Drawing.Point(335, 61);
            this.btnBrowseSongs.Name = "btnBrowseSongs";
            this.btnBrowseSongs.Size = new System.Drawing.Size(32, 32);
            this.btnBrowseSongs.TabIndex = 8;
            this.btnBrowseSongs.UseVisualStyleBackColor = true;
            this.btnBrowseSongs.Click += new System.EventHandler(this.btnBrowseSongs_Click);
            // 
            // btnSave
            // 
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Image = global::MusicPlayer.Properties.Resources.Save;
            this.btnSave.Location = new System.Drawing.Point(373, 22);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(32, 32);
            this.btnSave.TabIndex = 4;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnBrowsePath
            // 
            this.btnBrowsePath.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBrowsePath.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnBrowsePath.FlatAppearance.BorderSize = 0;
            this.btnBrowsePath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowsePath.Image = global::MusicPlayer.Properties.Resources.Browse;
            this.btnBrowsePath.Location = new System.Drawing.Point(335, 22);
            this.btnBrowsePath.Name = "btnBrowsePath";
            this.btnBrowsePath.Size = new System.Drawing.Size(32, 32);
            this.btnBrowsePath.TabIndex = 1;
            this.btnBrowsePath.UseVisualStyleBackColor = true;
            this.btnBrowsePath.Click += new System.EventHandler(this.btnBrowsePath_Click);
            // 
            // checkFileNames
            // 
            this.checkFileNames.AutoSize = true;
            this.checkFileNames.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkFileNames.ForeColor = System.Drawing.SystemColors.Window;
            this.checkFileNames.Location = new System.Drawing.Point(192, 105);
            this.checkFileNames.Name = "checkFileNames";
            this.checkFileNames.Size = new System.Drawing.Size(15, 14);
            this.checkFileNames.TabIndex = 13;
            this.checkFileNames.UseVisualStyleBackColor = true;
            this.checkFileNames.CheckedChanged += new System.EventHandler(this.checkFileNames_CheckedChanged);
            // 
            // lblCheckFilenames
            // 
            this.lblCheckFilenames.AutoSize = true;
            this.lblCheckFilenames.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckFilenames.ForeColor = System.Drawing.SystemColors.Window;
            this.lblCheckFilenames.Location = new System.Drawing.Point(12, 99);
            this.lblCheckFilenames.Name = "lblCheckFilenames";
            this.lblCheckFilenames.Size = new System.Drawing.Size(162, 20);
            this.lblCheckFilenames.TabIndex = 14;
            this.lblCheckFilenames.Text = "Include paths in CSV:";
            this.lblCheckFilenames.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.ClientSize = new System.Drawing.Size(419, 179);
            this.Controls.Add(this.lblCheckFilenames);
            this.Controls.Add(this.checkFileNames);
            this.Controls.Add(this.btnOpenSong);
            this.Controls.Add(this.txtSongPath);
            this.Controls.Add(this.lblEditSongID3);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnBrowseSongs);
            this.Controls.Add(this.txtDBName);
            this.Controls.Add(this.lblCreateDB);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblDefaultPath);
            this.Controls.Add(this.txtDefaultPath);
            this.Controls.Add(this.btnBrowsePath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Classes.NewButton btnBrowsePath;
        private System.Windows.Forms.TextBox txtDefaultPath;
        private System.Windows.Forms.Label lblDefaultPath;
        private Classes.NewButton btnSave;
        private System.Windows.Forms.Label lblCreateDB;
        private System.Windows.Forms.TextBox txtDBName;
        private Classes.NewButton btnBrowseSongs;
        private Classes.NewButton btnEdit;
        private System.Windows.Forms.TextBox txtSongPath;
        private System.Windows.Forms.Label lblEditSongID3;
        private Classes.NewButton btnOpenSong;
        private System.Windows.Forms.CheckBox checkFileNames;
        private System.Windows.Forms.Label lblCheckFilenames;
    }
}