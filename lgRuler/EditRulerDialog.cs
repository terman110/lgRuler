using System;
using System.Drawing;
using System.Windows.Forms;

namespace lg.win.ruler
{
    public partial class EditRulerDialog : Form
    {
        Rectangle rectValid;
        Ruler parentRuler;

        public EditRulerDialog(Ruler ruler)
        {
            InitializeComponent();
            parentRuler = ruler;

            Text += " [" + ruler.Name + "]";

            rectValid = new Rectangle(ruler.Location, ruler.Size);

            Rectangle rectMax = new Rectangle(0, 0, 0, 0);
            foreach (Screen screen in Screen.AllScreens)
                rectMax = Rectangle.Union(rectMax, screen.Bounds);

            numWidth.Minimum = ruler.MinimumSize.Width;
            numWidth.Maximum = rectMax.Right - rectMax.Left;
            numX.Minimum = rectMax.Left;
            numX.Maximum = rectMax.Right;

            numHeight.Minimum = ruler.MinimumSize.Height;
            numHeight.Maximum = rectMax.Bottom - rectMax.Top;
            numY.Minimum = rectMax.Top;
            numY.Maximum = rectMax.Bottom;

            try { numWidth.Value = ruler.Width; } catch { }
            try { numHeight.Value = ruler.Height; } catch { }
            try { numX.Value = ruler.Location.X; } catch { }
            try { numY.Value = ruler.Location.Y; } catch { }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            parentRuler.StartPosition = FormStartPosition.Manual;
            parentRuler.Width = (int)numWidth.Value;
            parentRuler.Height = (int)numHeight.Value;
            parentRuler.Location = new Point((int)numX.Value, (int)numY.Value);
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void numWidth_ValueChanged(object sender, EventArgs e)
        {
            if (!CheckDimensions())
                numWidth.Value = rectValid.Width;
        }

        private void numHeight_ValueChanged(object sender, EventArgs e)
        {
            if (!CheckDimensions())
                numHeight.Value = rectValid.Height;
        }

        private void numX_ValueChanged(object sender, EventArgs e)
        {
            if (!CheckDimensions())
                numX.Value = rectValid.X;
        }

        private void numY_ValueChanged(object sender, EventArgs e)
        {
            if (!CheckDimensions())
                numY.Value = rectValid.Y;
        }

        private bool CheckDimensions()
        {
            return CheckDimensions((int)numX.Value, (int)numY.Value, (int)numWidth.Value, (int)numHeight.Value);
        }

        private bool CheckDimensions(int x, int y, int width, int height)
        {
            return CheckDimensions(new Rectangle(x, y, width, height));
        }

        private bool CheckDimensions(Rectangle r)
        {
            bool isValid = false;
            foreach (Screen screen in Screen.AllScreens)
                isValid |= screen.Bounds.Contains(r);
            return isValid;
        }
    }
}
