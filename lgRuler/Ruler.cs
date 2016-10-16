using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace lg.win.ruler
{
    public partial class Ruler : Form
    {
        private static int MaxRulerIndex = 0;   // Running number of rulers created

        #region Delegates
        public delegate void CloseRulerHandler(Ruler sender);
        public event CloseRulerHandler CloseRuler;

        public delegate void StayOnTopChangedHandler(Ruler sender, bool bNewValue);
        public event StayOnTopChangedHandler StayOnTopChanged;
        #endregion

        #region Static Pens
        static private Pen penMajorTick = null;
        static private Pen penMinorTick = null;
        static private Brush brushLabel = null;
        #endregion

        #region Construction
        public Ruler()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);

            ++MaxRulerIndex;
            Name = "Ruler " + MaxRulerIndex.ToString();
            Text = Name;
            MenuItem = new ToolStripMenuItem(Name);

            AllowTransparency = true;
            ForeColor = Settings.RulerForeColor;
            BackColor = Settings.RulerBackColor;
            Opacity = (double)Settings.RulerBackAlpha / 255.0;
            if (penMajorTick == null)
                penMajorTick = new Pen(Settings.RulerMajorTickColor);
            if (penMinorTick == null)
                penMinorTick = new Pen(Settings.RulerMinorTickColor);
            if (brushLabel == null)
                brushLabel = new SolidBrush(Settings.RulerLabelColor);
        }
        #endregion

        #region Properties
        new public string Name { get; private set; }
        public ToolStripMenuItem MenuItem { get; private set; }

        private static Properties.Settings Settings { get { return Properties.Settings.Default; } }
        #endregion

        #region Paint
        private void Ruler_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle r = this.ClientRectangle;

            g.Clear(BackColor);

            Rectangle rLeft = r;    // Area left for size label

            // Draw x-axis
            {
                string strLabel;
                SizeF sizeLabel;
                Rectangle rectLabel;
                int lastLabelPos = 0;
                for (int major = 0; major <= r.Width; major += Settings.RulerMajorTickInterval)
                {
                    g.DrawLine(penMajorTick, major, 0, major, Settings.RulerMajorTickSize);
                    if (rLeft.Y < Settings.RulerMajorTickSize)
                        rLeft.Y = Settings.RulerMajorTickSize;

                    strLabel = major.ToString();
                    sizeLabel = g.MeasureString(strLabel, Settings.RulerLabelFont);
                    rectLabel = new Rectangle(major - (int)Math.Ceiling(sizeLabel.Width) / 2, Settings.RulerMajorTickSize + Settings.RulerLabelPadding, (int)Math.Ceiling(sizeLabel.Width), (int)Math.Ceiling(sizeLabel.Height));
                    if (rectLabel.Left >= lastLabelPos + Settings.RulerLabelPadding && rectLabel.Right <= r.Width - Settings.RulerLabelPadding &&
                        rectLabel.Top >= Settings.RulerLabelPadding && rectLabel.Bottom <= r.Height - Settings.RulerLabelPadding)
                    {
                        g.DrawString(strLabel, Settings.RulerLabelFont, brushLabel, rectLabel);
                        lastLabelPos = rectLabel.Right;
                        if (rLeft.Y < rectLabel.Bottom)
                            rLeft.Y = rectLabel.Bottom;
                    }

                    for (int minor = 0; minor < Settings.RulerMajorTickInterval; minor += Settings.RulerMinorTickInterval)
                        g.DrawLine(penMajorTick, major + minor, 0, major + minor, Settings.RulerMinorTickSize);
                }
            }

            // Draw y-label
            {
                string strLabel;
                SizeF sizeLabel;
                Rectangle rectLabel;
                int lastLabelPos = 0;
                for (int major = 0; major <= r.Height; major += Settings.RulerMajorTickInterval)
                {
                    if (major > Settings.RulerMajorTickSize + Settings.RulerLabelPadding)
                    {
                        g.DrawLine(penMajorTick, 0, major, Settings.RulerMajorTickSize, major);
                        if (rLeft.X < Settings.RulerMajorTickSize)
                            rLeft.X = Settings.RulerMajorTickSize;

                        strLabel = major.ToString();
                        sizeLabel = g.MeasureString(strLabel, Settings.RulerLabelFont);
                        rectLabel = new Rectangle(Settings.RulerMajorTickSize + Settings.RulerLabelPadding, major - (int)Math.Ceiling(sizeLabel.Height) / 2, (int)Math.Ceiling(sizeLabel.Width), (int)Math.Ceiling(sizeLabel.Height));
                        if (rectLabel.Left >= Settings.RulerLabelPadding && rectLabel.Right <= r.Width - Settings.RulerLabelPadding &&
                            rectLabel.Top >= lastLabelPos + Settings.RulerLabelPadding && rectLabel.Bottom <= r.Height - Settings.RulerLabelPadding)
                        {
                            g.DrawString(strLabel, Settings.RulerLabelFont, brushLabel, rectLabel);
                            lastLabelPos = rectLabel.Top;
                            if (rLeft.X < rectLabel.Right)
                                rLeft.X = rectLabel.Right;
                        }
                    }

                    for (int minor = 0; minor < Settings.RulerMajorTickInterval; minor += Settings.RulerMinorTickInterval)
                        if (major + minor > Settings.RulerMajorTickSize + Settings.RulerLabelPadding)
                            g.DrawLine(penMajorTick, 0, major + minor, Settings.RulerMinorTickSize, major + minor);
                }
            }

            // Draw size label
            {
                string strLabel = Width.ToString() + "px x " + Height.ToString() + "px";
                SizeF sizeLabel = g.MeasureString(strLabel, Settings.RulerLabelFont);
                Rectangle rectLabel = new Rectangle(0, 0, (int)Math.Ceiling(sizeLabel.Width), (int)Math.Ceiling(sizeLabel.Height));
                rectLabel.X = Width - rectLabel.Width - Settings.RulerLabelPadding;
                rectLabel.Y = Height - rectLabel.Height - Settings.RulerLabelPadding;
                if (rLeft.Contains(rectLabel))
                {
                    g.DrawString(strLabel, Settings.RulerLabelFont, brushLabel, rectLabel);
                }
                else
                {
                    strLabel = Width.ToString() + "px" + Environment.NewLine + Height.ToString() + "px";
                    sizeLabel = g.MeasureString(strLabel, Settings.RulerLabelFont);
                    rectLabel = new Rectangle(0, 0, (int)Math.Ceiling(sizeLabel.Width), (int)Math.Ceiling(sizeLabel.Height));
                    rectLabel.X = Width - rectLabel.Width - Settings.RulerLabelPadding;
                    rectLabel.Y = Height - rectLabel.Height - Settings.RulerLabelPadding;
                    if (rLeft.Contains(rectLabel))
                    {
                        g.DrawString(strLabel, Settings.RulerLabelFont, brushLabel, rectLabel);
                    }
                }
            }
        }
        #endregion

        #region Mouse
        private void Ruler_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                nameItem.Text = Name;
                sizeItem.Text = "Size:     " + Width.ToString() + "px x " + Height.ToString() + "px";
                locationItem.Text = "Location: " + Location.X.ToString() + "px; " + Location.X.ToString() + "px";
                try
                {
                    Screen screen = Screen.AllScreens.First(x => x.Bounds.Contains(Location));
                    screenItem.Text = "Screen:   " + screen.DeviceName.Replace(@"\\.\", "") + (screen.Primary ? " (Primary)" : "");
                }
                catch
                {
                    screenItem.Text = "Unknown";
                }
                MenuStrip.Show(PointToScreen(e.Location));
            }
        }

        private Point lastMousePos;
        private Point? firstMousePos = null;
        private ResizeMode moveMode;
        private void Ruler_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && firstMousePos != null)
            {
                Rectangle rectPrev = DisplayRectangle;

                Point pDelta = new Point(
                    e.Location.X - ((Point)lastMousePos).X,
                    e.Location.Y - ((Point)lastMousePos).Y
                    );

                Point pLocDelta = new Point(
                    Location.X - (((Point)firstMousePos).X - e.Location.X),
                    Location.Y - (((Point)firstMousePos).Y - e.Location.Y)
                    );

                switch (moveMode)
                {
                    case ResizeMode.Right:
                        Width += pDelta.X;
                        break;
                    case ResizeMode.Left:
                        Width += (((Point)firstMousePos).X - e.Location.X);
                        Location = new Point(pLocDelta.X, Location.Y);
                        break;
                    case ResizeMode.Bottom:
                        Height += pDelta.Y;
                        break;
                    case ResizeMode.Top:
                        Height += ((Point)firstMousePos).Y - e.Location.Y;
                        Location = new Point(Location.X, pLocDelta.Y);
                        break;
                    case ResizeMode.LowerRight:
                        Width += pDelta.X;
                        Height += pDelta.Y;
                        break;
                    case ResizeMode.UpperRight:
                        Width += pDelta.X;
                        Height += ((Point)firstMousePos).Y - e.Location.Y;
                        Location = new Point(Location.X, pLocDelta.Y);
                        break;
                    case ResizeMode.LowerLeft:
                        Width += (((Point)firstMousePos).X - e.Location.X);
                        Height += pDelta.Y;
                        Location = new Point(pLocDelta.X, Location.Y);
                        break;
                    case ResizeMode.UpperLeft:
                        Width += (((Point)firstMousePos).X - e.Location.X);
                        Height += ((Point)firstMousePos).Y - e.Location.Y;
                        Location = pLocDelta;
                        break;
                    default: // case ResizeMode.Move;
                        Location = pLocDelta;
                        break;
                }
                if (moveMode != ResizeMode.Move)
                    Refresh();
            }
            else
                UpdateCursor(e.Location);
            lastMousePos = e.Location;
        }

        private void Ruler_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                firstMousePos = e.Location;
                moveMode = UpdateCursor(e.Location);
            }
        }

        private void Ruler_MouseUp(object sender, MouseEventArgs e)
        {
            firstMousePos = null;
        }

        private ResizeMode UpdateCursor(Point p)
        {
            if (p.X < Padding.Left && p.Y < Padding.Top)
            {
                Cursor = Cursors.SizeNWSE;
                return ResizeMode.UpperLeft;
            }
            if (p.X >= Width - Padding.Right && p.Y >= Height - Padding.Bottom)
            {
                Cursor = Cursors.SizeNWSE;
                return ResizeMode.LowerRight;
            }

            if (p.X < Padding.Left && p.Y >= Height - Padding.Bottom)
            {
                Cursor = Cursors.SizeNESW;
                return ResizeMode.LowerLeft;
            }
            if (p.X >= Width - Padding.Right && p.Y < Padding.Top)
            {
                Cursor = Cursors.SizeNESW;
                return ResizeMode.UpperRight;
            }

            if (p.X < Padding.Left)
            {
                Cursor = Cursors.SizeWE;
                return ResizeMode.Left;
            }
            if (p.X >= Width - Padding.Right)
            {
                Cursor = Cursors.SizeWE;
                return ResizeMode.Right;
            }
            if (p.Y < Padding.Top)
            {
                Cursor = Cursors.SizeNS;
                return ResizeMode.Top;
            }
            if (p.Y >= Height - Padding.Bottom)
            {
                Cursor = Cursors.SizeNS;
                return ResizeMode.Bottom;
            }

            Cursor = Cursors.Default;
            return ResizeMode.Move;
        }

        private void setSizeItem_Click(object sender, EventArgs e)
        {
            EditRulerDialog dlg = new EditRulerDialog(this);
            dlg.ShowDialog(this);
            Refresh();
        }
        #endregion

        #region Closing
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TopMost = ((ToolStripMenuItem)sender).Checked;
            if (StayOnTopChanged != null)
                StayOnTopChanged(this, TopMost);
        }

        private void closeToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        new public void Close()
        {
            if (CloseRuler != null)
                CloseRuler(this);
            base.Close();
        }
        #endregion

        #region Property Strings
        public string ToSettingsString()
        {
            return "{" +
                Location.X.ToString() + "," +
                Location.Y.ToString() + "," +
                Size.Width.ToString() + "," +
                Size.Height.ToString() + "," +
                TopMost + "," +
                ShowInTaskbar +
                "}";
        }

        public void FromSettingsString(string settings)
        {
            if (!settings.StartsWith("{") || !settings.EndsWith("}"))
                return;
            try
            {
                settings = settings.Substring(1, settings.Length - 2);
                string[] items = settings.Split(',');
                StartPosition = FormStartPosition.Manual;
                Location = new Point(int.Parse(items[0]), int.Parse(items[1]));
                Size = new Size(int.Parse(items[2]), int.Parse(items[3]));
                TopMost = bool.Parse(items[4]);
                ShowInTaskbar = bool.Parse(items[5]);
            }
            catch { }
        }
        #endregion
    }
}
