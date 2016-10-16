using System;
using System.Windows.Forms;

namespace lg.win.ruler
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            NotificationForm notificationIcon = new NotificationForm();
            notificationIcon.Visible = false;
            Application.Run(notificationIcon);
        }
    }
}
