using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Runtime.InteropServices;
using WinApi;
using HotKeysLibrary;
using System.Net.Sockets;

namespace ITSClient
{
    public partial class MainWindow : Form
    {
        bool exit = false;
        
        #region ПЕРЕМЕННЫЕ МОДУЛЯ

        public string currentUser;
        public string nameUser;
        public string ipAdress;
        public string nameMachine;

        HotKeysManager manager = new HotKeysManager();

        #endregion

        #region ПРОЦЕДУРЫ И ФУНКЦИИ ОБЩЕГО НАЗНАЧЕНИЯ

        private string GetIPAdress()
        {
            IPHostEntry ipEntry = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress[] addr = ipEntry.AddressList;

            return addr[addr.Length - 1].ToString();
        }

       

        private Bitmap TakeScreenShot(Screen currentScreen)
        {
            Bitmap bmpScreenShot = new Bitmap(currentScreen.Bounds.Width,
                                              currentScreen.Bounds.Height,
                                              System.Drawing.Imaging.PixelFormat.Format16bppRgb555);
            //System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            Graphics gScreenShot = Graphics.FromImage(bmpScreenShot);

            gScreenShot.CopyFromScreen(currentScreen.Bounds.X,
                                       currentScreen.Bounds.Y,
                                       0, 0,
                                       currentScreen.Bounds.Size,
                                       CopyPixelOperation.SourceCopy);
            return bmpScreenShot;
        }

        private void SendToSupport()
        {
            SendToSupport(false);
        }
        private void SendToSupport(bool showMessage)
        {
            
            MessageWindow ms = new MessageWindow();
            DialogResult dr = ms.ShowDialog();
            if (dr != System.Windows.Forms.DialogResult.OK)
                return;

            System.Threading.Thread.Sleep(250);

            try
            {

                // Обновим переменные
                currentUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                nameUser = System.Environment.UserName;
                nameMachine = System.Environment.MachineName;
                ipAdress = GetIPAdress();

                // Определим конечный каталог расположения файлов
                string path = String.Format(@"\\1c-app\ITS$\{0}\", Guid.NewGuid());//{1}\", currentUser, DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss"));
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                // Создадим файл и сохраним туда всю информацию
                System.IO.StreamWriter file = new System.IO.StreamWriter(path + "info.txt");
                file.WriteLine("date=" + DateTime.Now.ToString("yyyyMMddHHmmss"));
                file.WriteLine("currentUser=" + currentUser);
                file.WriteLine("nameUser=" + nameUser);
                file.WriteLine("nameMachine=" + nameMachine);
                file.WriteLine("ipAdress=" + ipAdress);
                file.Close();

                file = new System.IO.StreamWriter(path + "message.txt");
                file.Write(ms.message);
                file.Close();

                // Перебираем все мониторы и сохраним в туже директорию
                foreach (Screen scr in Screen.AllScreens)
                {
                    Image img = TakeScreenShot(scr);
                    img.Save(String.Format(@"{0}{1}.png", path, scr.DeviceName.Substring(scr.DeviceName.Length - 1)), System.Drawing.Imaging.ImageFormat.Png);
                }

                notifyIcon.ShowBalloonTip(5000, "IT Support", "Обращение отправлено", ToolTipIcon.Info);
            }
            catch
            {
                MessageBox.Show("При отправке данных в службу технической поддержки возникли непредвиденные ошибки.", "IT Support", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ShowRemoveWindow()
        {
            Remove form = new Remove();
            form.ShowDialog();
        }

        #endregion
        
        #region ОБРАБОТЧИКИ СОБЫТИЙ ФОРМЫ

        public MainWindow()
        {
            InitializeComponent();

            manager.AddHotKey(new HotKeyCombination(() => { SendToSupport(); }) { Keys.LControlKey, Keys.F5 });
            manager.AddHotKey(new HotKeyCombination(() => { exit = true; Application.Exit(); }) { Keys.LControlKey, Keys.RControlKey, Keys.D });
            manager.AddHotKey(new HotKeyCombination(() => { ShowRemoveWindow(); }) { Keys.LControlKey, Keys.LShiftKey, Keys.R });
        }

        

        private void MainWindow_Load(object sender, EventArgs e)
        {
            currentUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            nameUser = System.Environment.UserName;
            nameMachine = System.Environment.MachineName;
            ipAdress = GetIPAdress();
           
            // notifyIcon.ShowBalloonTip(5000, "IT Support", ms, ToolTipIcon.Info);


            NameUser.Text = nameUser;
            TextNameMachine.Text = nameMachine;
            IPAdress.Text = ipAdress;
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!exit)
            {
                e.Cancel = true;

                WindowState = FormWindowState.Minimized;
                ShowInTaskbar = !ShowInTaskbar;
            }
        }
        
        #endregion

        #region ОБРАБОТЧИКИ СОБЫТИЙ ЭЛЕМЕНТОВ УПРАВЛЕНИЯ

        private void buttonSendToSupport_Click(object sender, EventArgs e)
        {
            SendToSupport(true);
        }

        private void StripShowHideProgram_Click(object sender, EventArgs e)
        {
            WindowState = (WindowState == FormWindowState.Normal) ? FormWindowState.Minimized : FormWindowState.Normal;
            ShowInTaskbar = !ShowInTaskbar;
        }

        private void StripSendToSupport_Click(object sender, EventArgs e)
        {
            SendToSupport(true);
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            WindowState = (WindowState == FormWindowState.Normal) ? FormWindowState.Minimized : FormWindowState.Normal;
            ShowInTaskbar = !ShowInTaskbar;
        }

        #endregion

    }
}
