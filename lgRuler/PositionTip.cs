using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace lg.win.ruler
{
    public partial class PositionTip : Form
    {
        public PositionTip()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);

            AllowTransparency = true;
            ForeColor = Properties.Settings.Default.RulerForeColor;
            BackColor = Properties.Settings.Default.RulerBackColor;
            Opacity = (double)Properties.Settings.Default.RulerBackAlpha / 255.0;
        }

        public new string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;
                label.Text = base.Text;
                SizeF sTxt;
                using (Graphics g = Graphics.FromHwnd(Handle)) sTxt = g.MeasureString(value, label.Font);
                Size = new Size((int)Math.Ceiling(sTxt.Width + 8f), (int)Math.Ceiling(sTxt.Height + 8f));
            }
        }
    }
}
