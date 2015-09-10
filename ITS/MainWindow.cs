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
using System.IO;
using System.Security.Cryptography;
using WebSocket4Net;
using Newtonsoft.Json;
using Microsoft.CSharp.RuntimeBinder;
using System.Threading;

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
        public string ScreenLink;
        public string ScreenShotPath;


        public string websocketStatus;

        public WebSocket websocket = new WebSocket("ws://storage.ktga.kz:8001/");

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


        static string getMd5Hash(string input)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        public string GetScreenShotLink(string path,string link,string display)
        {
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            byte[] data = new byte[fs.Length];
            fs.Read(data, 0, data.Length);
            fs.Close();

            if (link == "")
            {
                link = "/ITSClient_" + getMd5Hash(DateTime.Now.ToLongTimeString());
            } 
            // Generate HASH of links


            // Generate post objects
            Dictionary<string, object> postParameters = new Dictionary<string, object>();
            postParameters.Add("filename", display+".png");
            postParameters.Add("fileformat", "png");
            postParameters.Add("file[0]", new FormUpload.FileParameter(data, display+".png", "image/png"));

            //host
            string host = "http://storage.ktga.kz";

            // Create request and receive response
            string postURL = host + "/upload.php";
            string userAgent = "ITSClient";
            string referer = host + link;
            HttpWebResponse webResponse = FormUpload.MultipartFormDataPost(postURL, userAgent, postParameters, referer);

            // Process response
            StreamReader responseReader = new StreamReader(webResponse.GetResponseStream());
            string fullResponse = responseReader.ReadToEnd();
            webResponse.Close();

            //WebRequest wrs = WebRequest.Create(referer + ".json");

            //WebResponse wr = wrs.GetResponse() as HttpWebResponse;

            //StreamReader rr = new StreamReader(wr.GetResponseStream());
            //string res = rr.ReadToEnd();
            //wr.Close();

            return referer;

        }

        public string GetJsonFromLink(string link) {

            Console.WriteLine(link);

            Uri url = new Uri(link+".json");

            WebRequest wrs = WebRequest.Create(url);

            WebResponse wr = wrs.GetResponse() as HttpWebResponse;

            StreamReader rr = new StreamReader(wr.GetResponseStream());
            string res = rr.ReadToEnd();
            wr.Close();
            return res;
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
            //manager.AddHotKey(new HotKeyCombination(() => { ShowRemoveWindow(); }) { Keys.LControlKey, Keys.LShiftKey, Keys.R });
        }

        private void websocket_Opened(object sender, EventArgs e)
        {

            websocketStatus = "opened";
            currentUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            nameUser = System.Environment.UserName;
            nameMachine = System.Environment.MachineName;
            ipAdress = GetIPAdress();


            var data = new Dictionary<string, string> {
                { "currentUser", currentUser },
                { "nameUser", nameUser },
                { "nameMachine", nameMachine },
                { "ipAdress", ipAdress }

            };

            //string[] data = { currentUser, nameUser, nameMachine, ipAdress };

            var javaScriptSerializer = new
            System.Web.Script.Serialization.JavaScriptSerializer();
            string jsonString = javaScriptSerializer.Serialize(data);
            Console.WriteLine(jsonString);

            websocket.Send("{\"on\":\"register\",\"data\":" + jsonString + "}");


        }

        public void websocket_Error(object sender, SuperSocket.ClientEngine.ErrorEventArgs e)
        {
            websocketStatus = "closed";

            Thread.Sleep(5000);
            if (websocketStatus != "opened")
            {
                websocket.Open();
            }
            notifyIcon.ShowBalloonTip(5000, "IT Support", e.Exception.Message, ToolTipIcon.Error);
        }

        public void websocket_Closed(object sender, EventArgs e)
        {
            websocketStatus = "closed";
            Thread.Sleep(5000);
            if (websocketStatus != "opened") {
                websocket.Open();
            }
        }


        // Func<int, int> -- функция, принимающая int и возвращающая тоже int
        //public Emit<T>(string data, Func<string,T>f)
        //{
        //    return f(data);
        //}

        //int getDouble(int x) { return 2 * x; }

        //public delegate F( string x); // как бы typedef
        //void DoAction( string x, F f) { f( x); }

        //void makeDouble(ref int x) { x = 2 * x; }
        //int i = 5;
        //DoAction(ref i, makeDouble);

        //public void onsay(string data)
        //{
        //    notifyIcon.ShowBalloonTip(5000, "IT Support", "SomeBody Say", ToolTipIcon.Info);
        //}

        //public string say(string data)
        //{ 
        //    notifyIcon.ShowBalloonTip(5000, "IT Support", "SomeBody Say", ToolTipIcon.Info);
        //    return "yes";

        //}

        private void websocket_MessageReceived(object sender, MessageReceivedEventArgs e)
        {

            MyEvent messdata = JsonConvert.DeserializeObject<MyEvent>("{\"on\":\"\",\"data\":\"\"}");
            // notifyIcon.ShowBalloonTip(5000, "IT Support", e.Message, ToolTipIcon.Info);
            try
            {

                 messdata = JsonConvert.DeserializeObject<MyEvent>(e.Message);

            }
            catch (System.Exception excep)
            {

                Console.WriteLine(excep.Message);

            }




            // Console.WriteLine("{0} {1}", MessageData.QuestionId, MessageData.QuestionTitle);

            //notifyIcon.ShowBalloonTip(5000, "IT Support", messdata.on.ToString(), ToolTipIcon.Info);
            //string mess = messdata.on.ToString(); 
            //Emit(messdata.data, messdata.on );

            //Action<string> emit = this.;
            //emit(messdata.data);

            //            DoAction(messdata.data, messdata.on);

            //            int i = 5;
            //          i = Apply(e.Message, );

            switch (messdata.on)
            {
                case "say":
                    notifyIcon.ShowBalloonTip(5000, "IT Support->Server Say", messdata.data.ToString(), ToolTipIcon.Info);


                    break;
                case "takescreen":

                     
 
                    // Определим конечный каталог расположения файлов
                    string path = String.Format(@"C:\ITS\{0}\", Guid.NewGuid());//{1}\", currentUser, DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss"));
                    if (!System.IO.Directory.Exists(path))
                    {
                        System.IO.Directory.CreateDirectory(path);
                    }
                    string link = "/ITSClient_" + getMd5Hash(DateTime.Now.ToLongTimeString());
                    // Перебираем все мониторы и сохраним в туже директорию
                    foreach (Screen scr in Screen.AllScreens)
                    {
                        Image img = TakeScreenShot(scr);
                        string fullpath = String.Format(@"{0}{1}.png", path, scr.DeviceName.Substring(scr.DeviceName.Length - 1));
                        img.Save(fullpath, System.Drawing.Imaging.ImageFormat.Png);
                        //Загружаем в хранилище
                        GetScreenShotLink(fullpath,link, scr.DeviceName.Substring(scr.DeviceName.Length - 1));
                    }

                    string jsonData = GetJsonFromLink("http://storage.ktga.kz" + link);

                    websocket.Send("{\"on\":\"screenlink\",\"data\":{\"requester\":\""+ messdata.data +"\",\"link\":\"http://storage.ktga.kz"+ link + "\",\"data\":"+jsonData+"}}");
                    notifyIcon.ShowBalloonTip(5000, "IT Support", "Скриншот отправлен в сервер", ToolTipIcon.Info);
                    break;
                default:
                    notifyIcon.ShowBalloonTip(5000, "IT Support", e.Message, ToolTipIcon.Info);
                    break;
            }
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            currentUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            nameUser = System.Environment.UserName;
            nameMachine = System.Environment.MachineName;
            ipAdress = GetIPAdress();

            // notifyIcon.ShowBalloonTip(5000, "IT Support", ms, ToolTipIcon.Info);

            websocket.Opened += new EventHandler(websocket_Opened);
            //  websocket.Error += new EventHandler<ErrorEventArgs>(websocket_Error);
            websocket.Closed += new EventHandler(websocket_Closed);
            websocket.MessageReceived += new EventHandler<MessageReceivedEventArgs>(websocket_MessageReceived);
            websocket.Open();

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
