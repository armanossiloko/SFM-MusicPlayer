using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicPlayer.Classes
{
    public class NewButton : Button
    {
        public NewButton() : base()
        {
            this.SetStyle(ControlStyles.Selectable, false);
            this.FlatAppearance.MouseOverBackColor = Color.Transparent;
        }

        protected override bool ShowFocusCues
        {
            get
            {
                return false;
            }
        }
    }
}
