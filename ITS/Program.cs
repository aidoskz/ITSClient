using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;


namespace ITSClient
{
    static class Program
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hwnd);
      
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Проверим, была ли запущеа ранее программа
            Process[] process = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);
            if (process.Count() > 1)
                SetForegroundWindow(process.First().MainWindowHandle);
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
      
                Application.Run(new MainWindow());
             

            }



        }

       


    }
}
