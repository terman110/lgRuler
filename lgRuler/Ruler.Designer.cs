namespace lg.win.ruler
{
    partial class Ruler
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ruler));
            this.MenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.nameItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sizeItem = new System.Windows.Forms.ToolStripMenuItem();
            this.locationItem = new System.Windows.Forms.ToolStripMenuItem();
            this.screenItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setSizeItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuStrip
            // 
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nameItem,
            this.sizeItem,
            this.locationItem,
            this.screenItem,
            this.setSizeItem,
            this.toolStripSeparator2,
            this.toolStripMenuItem1,
            this.toolStripSeparator1,
            this.closeToolStripMenuItem});
            this.MenuStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Table;
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(139, 170);
            // 
            // nameItem
            // 
            this.nameItem.Enabled = false;
            this.nameItem.Font = new System.Drawing.Font("Consolas", 9F);
            this.nameItem.Name = "nameItem";
            this.nameItem.Size = new System.Drawing.Size(138, 22);
            // 
            // sizeItem
            // 
            this.sizeItem.Enabled = false;
            this.sizeItem.Font = new System.Drawing.Font("Consolas", 9F);
            this.sizeItem.Name = "sizeItem";
            this.sizeItem.Size = new System.Drawing.Size(138, 22);
            // 
            // locationItem
            // 
            this.locationItem.Enabled = false;
            this.locationItem.Font = new System.Drawing.Font("Consolas", 9F);
            this.locationItem.Name = "locationItem";
            this.locationItem.Size = new System.Drawing.Size(138, 22);
            // 
            // screenItem
            // 
            this.screenItem.Enabled = false;
            this.screenItem.Font = new System.Drawing.Font("Consolas", 9F);
            this.screenItem.Name = "screenItem";
            this.screenItem.Size = new System.Drawing.Size(138, 22);
            // 
            // setSizeItem
            // 
            this.setSizeItem.Name = "setSizeItem";
            this.setSizeItem.Size = new System.Drawing.Size(138, 22);
            this.setSizeItem.Text = "Set Size ...";
            this.setSizeItem.Click += new System.EventHandler(this.setSizeItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(135, 6);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.CheckOnClick = true;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(138, 22);
            this.toolStripMenuItem1.Text = "Stay On Top";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(135, 6);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click_1);
            // 
            // Ruler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Maroon;
            this.ClientSize = new System.Drawing.Size(219, 38);
            this.ControlBox = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(38, 38);
            this.Name = "Ruler";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.ShowInTaskbar = false;
            this.Text = "Ruler";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Ruler_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Ruler_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Ruler_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Ruler_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Ruler_MouseUp);
            this.MenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nameItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem sizeItem;
        private System.Windows.Forms.ToolStripMenuItem locationItem;
        private System.Windows.Forms.ToolStripMenuItem screenItem;
        private System.Windows.Forms.ToolStripMenuItem setSizeItem;
    }
}