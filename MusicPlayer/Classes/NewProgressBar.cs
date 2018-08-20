using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicPlayer
{
    public class NewProgressBar : ProgressBar
    {
        public NewProgressBar()
        {
            this.SetStyle(ControlStyles.UserPaint, true);

            this.BackColor = Color.FromArgb(255, (byte)35, (byte)35, (byte)35);
            this.ForeColor = Color.White;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.Green, 0, 0, e.ClipRectangle.Width, e.ClipRectangle.Height);

            Rectangle rec = e.ClipRectangle;

            rec.Width = (int)(rec.Width * ((double)Value / Maximum));
            if (ProgressBarRenderer.IsSupported)
                ProgressBarRenderer.DrawHorizontalBar(e.Graphics, e.ClipRectangle);
            var DarkGray = new SolidBrush(Color.FromArgb(255, (byte)35, (byte)35, (byte)35));

            e.Graphics.FillRectangle(Brushes.White, 0, 0, rec.Width, rec.Height); //Progress
            e.Graphics.FillRectangle(DarkGray, rec.Width, 0, e.ClipRectangle.Width, e.ClipRectangle.Height); //The rest

            this.BackColor = Color.FromArgb(255, (byte)35, (byte)35, (byte)35);
            this.ForeColor = Color.White;
        }
    }
}