namespace MusicPlayer
{
    partial class AboutForm
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
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.lblForType = new System.Windows.Forms.Label();
            this.lblVNum = new System.Windows.Forms.Label();
            this.lblCopyright = new System.Windows.Forms.Label();
            this.picLogo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.ForeColor = System.Drawing.SystemColors.Window;
            this.lblVersion.Location = new System.Drawing.Point(12, 78);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(71, 20);
            this.lblVersion.TabIndex = 6;
            this.lblVersion.Text = "Version: ";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblType.ForeColor = System.Drawing.SystemColors.Window;
            this.lblType.Location = new System.Drawing.Point(12, 109);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(47, 20);
            this.lblType.TabIndex = 7;
            this.lblType.Text = "Type:";
            this.lblType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblForType
            // 
            this.lblForType.AutoSize = true;
            this.lblForType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblForType.ForeColor = System.Drawing.SystemColors.Window;
            this.lblForType.Location = new System.Drawing.Point(84, 110);
            this.lblForType.Name = "lblForType";
            this.lblForType.Size = new System.Drawing.Size(55, 20);
            this.lblForType.TabIndex = 9;
            this.lblForType.Text = "*Type*";
            this.lblForType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblVNum
            // 
            this.lblVNum.AutoSize = true;
            this.lblVNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVNum.ForeColor = System.Drawing.SystemColors.Window;
            this.lblVNum.Location = new System.Drawing.Point(84, 79);
            this.lblVNum.Name = "lblVNum";
            this.lblVNum.Size = new System.Drawing.Size(77, 20);
            this.lblVNum.TabIndex = 8;
            this.lblVNum.Text = "*Number*";
            this.lblVNum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCopyright
            // 
            this.lblCopyright.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCopyright.ForeColor = System.Drawing.SystemColors.Window;
            this.lblCopyright.Location = new System.Drawing.Point(0, 140);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(291, 27);
            this.lblCopyright.TabIndex = 10;
            this.lblCopyright.Text = "Copyright";
            this.lblCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picLogo
            // 
            this.picLogo.Location = new System.Drawing.Point(16, 12);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(260, 50);
            this.picLogo.TabIndex = 11;
            this.picLogo.TabStop = false;
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.ClientSize = new System.Drawing.Size(291, 167);
            this.Controls.Add(this.picLogo);
            this.Controls.Add(this.lblCopyright);
            this.Controls.Add(this.lblForType);
            this.Controls.Add(this.lblVNum);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.lblVersion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "About";
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label lblForType;
        private System.Windows.Forms.Label lblVNum;
        private System.Windows.Forms.Label lblCopyright;
        private System.Windows.Forms.PictureBox picLogo;
    }
}