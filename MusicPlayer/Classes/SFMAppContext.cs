using System;
using System.Windows.Forms;
using MusicPlayer.Properties;

namespace MusicPlayer.Classes
{
    public class SFMAppContext : ApplicationContext
    {
        public static NotifyIcon TrayIcon;
        private readonly MainForm _mainForm;

        public SFMAppContext()
        {
            Application.ApplicationExit += new EventHandler(OnApplicationExit);
            _mainForm = new MainForm();
            TrayIcon = new NotifyIcon()
            {
                Icon = Resources.AppIcon,
                ContextMenu = new ContextMenu(new MenuItem[]
                {
                    new MenuItem("Show", Show),
                    new MenuItem("About", About),
                    new MenuItem("Exit", Exit)
                }),
                Visible = true
            };
            TrayIcon.Click += new EventHandler(NotifyIcon_Click);
            _mainForm.Show();
        }

        private void OnApplicationExit(object sender, EventArgs e)
        {
            TrayIcon.Visible = false;
        }

        void Exit(object sender, EventArgs e)
        {
            //TrayIcon.Visible = false;
            Application.Exit();
        }

        void About(object sender, EventArgs e)
        {
            AboutForm about = new AboutForm();
            about.Show();
        }

        void Show(object sender, EventArgs e)
        {
            _mainForm.Show();
            _mainForm.Activate();
        }

        private void NotifyIcon_Click(object sender, EventArgs e)
        {
            var eventArgs = e as MouseEventArgs;
            if (eventArgs.Button == MouseButtons.Left)
            {
                _mainForm.Show();
                _mainForm.Activate();
            }
        }
    }
}
