using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Forms;

namespace lg.win.ruler
{
    public partial class NotificationForm : Form
    {
        #region Declaraion
        private List<Ruler> _Ruler = new List<Ruler>();
        #endregion

        #region Construction
        public NotificationForm()
        {
            InitializeComponent();
            NIcon.Text = AboutBox.GetAssemblyName();

            showInTaskBarStripMenuItem.Checked = Settings.RulerShowInTaskBar;

            LoadRuler();
        }
        #endregion

        #region Properties
        private static Properties.Settings Settings { get { return Properties.Settings.Default; } }
        #endregion

        #region Settings
        private void LoadRuler()
        {
            if (Settings.RulerList == null)
                Settings.RulerList = new StringCollection();
            if (Settings.RulerList.Count < 1)
                addRulerToolStripMenuItem_Click(this, null);
            else
            {
                foreach (string serialized in Settings.RulerList)
                {
                    try
                    {
                        Ruler ruler = new Ruler();
                        ruler.FromSettingsString(serialized);
                        if (ruler != null)
                            AddRuler(ruler);
                    }
                    catch { }
                }
            }
        }

        private void SaveRuler()
        {
            if (Settings.RulerList == null)
                Settings.RulerList = new StringCollection();
            Settings.RulerList.Clear();
            foreach (Ruler ruler in _Ruler)
            {
                string serialized = ruler.ToSettingsString();
                if (!string.IsNullOrWhiteSpace(serialized))
                    Settings.RulerList.Add(serialized);
            }
            Settings.Save();
        }
        #endregion

        #region Events
        private void NotificationForm_Load(object sender, EventArgs e)
        {
            // Hide frame on load
            // We only want the tray info icon
            Hide();
        }

        private void removeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            while (_Ruler.Count > 0)
                _Ruler.First().Close();
        }

        private void addRulerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ruler ruler = new Ruler();
            AddRuler(ruler);
        }

        private void AddRuler(Ruler ruler)
        {
            ruler.ShowInTaskbar = Settings.RulerShowInTaskBar;
            ruler.CloseRuler += Ruler_CloseRuler;
            if (MenuStrip.Items.Contains(noRulerToolStripMenuItem))
                MenuStrip.Items.Remove(noRulerToolStripMenuItem);
            MenuStrip.Items.Insert(_Ruler.Count, ruler.MenuItem);
            MenuStrip.Items[_Ruler.Count].Click += NotificationForm_Click;
            _Ruler.Add(ruler);
            ruler.Show(this);
        }

        private void NotificationForm_Click(object sender, EventArgs e)
        {
            if (!(sender is ToolStripItem))
                return;

            try
            {
                Ruler ruler = _Ruler.First(x => x.Name == ((ToolStripItem)sender).Text);
                if (ruler != null)
                {
                    ruler.Focus();
                    if (ruler.Size.Width < ruler.MinimumSize.Width || ruler.Size.Height < ruler.MinimumSize.Height)
                        ruler.Size = ruler.MinimumSize;
                    bool isInAnyScreen = false;
                    foreach (Screen screen in Screen.AllScreens)
                        isInAnyScreen |= screen.Bounds.Contains(ruler.Location);
                    if (!isInAnyScreen)
                        ruler.Location = Screen.PrimaryScreen.Bounds.Location;
                }
            }
            catch { }
        }

        private void Ruler_CloseRuler(Ruler sender)
        {
            if (!(sender is Ruler))
                return;

            _Ruler.Remove(sender);
            if (MenuStrip.Items.Contains(((Ruler)sender).MenuItem))
                MenuStrip.Items.Remove(((Ruler)sender).MenuItem);
            if (_Ruler.Count < 1)
                MenuStrip.Items.Insert(0, noRulerToolStripMenuItem);
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveRuler();
            Settings.Save();
            Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Form about = new AboutBox())
                about.ShowDialog(this);
        }

        private void NIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (_Ruler.Count > 0)
                _Ruler.Last().BringToFront();
        }

        private void showInTaskBarStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(sender is ToolStripMenuItem))
                return;

            Settings.RulerShowInTaskBar = ((ToolStripMenuItem)sender).Checked;
            Settings.Save();
            foreach (Ruler ruler in _Ruler)
                ruler.ShowInTaskbar = Settings.RulerShowInTaskBar;
        }
        #endregion
    }
}
