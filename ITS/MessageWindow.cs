using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Security.Cryptography;

namespace ITSClient
{
    public partial class MessageWindow : Form
    {
        public string message;

        public MessageWindow()
        {
            InitializeComponent();
            string ms = GetScreenShotLink();
            this.textMessage.Text = ms;
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

        public string GetScreenShotLink()
        {
            FileStream fs = new FileStream("d:\\ra.png", FileMode.Open, FileAccess.Read);
            byte[] data = new byte[fs.Length];
            fs.Read(data, 0, data.Length);
            fs.Close();

            // Generate HASH of links
            string link = "/ITSClient_" + getMd5Hash(DateTime.Now.ToLongTimeString());


            // Generate post objects
            Dictionary<string, object> postParameters = new Dictionary<string, object>();
            postParameters.Add("filename",  "ra.png");
            postParameters.Add("fileformat", "png");
            postParameters.Add("file[0]", new FormUpload.FileParameter(data, "ra.png", "image/png"));

            //host
            string host = "http://storage.ktga.kz";

            // Create request and receive response
            string postURL = host+"/upload.php";
            string userAgent = "ITSClient";
            string referer = host+link;
            HttpWebResponse webResponse = FormUpload.MultipartFormDataPost(postURL, userAgent, postParameters,referer);

            // Process response
            StreamReader responseReader = new StreamReader(webResponse.GetResponseStream());
            string fullResponse = responseReader.ReadToEnd();
            webResponse.Close();
            
                WebRequest wrs = WebRequest.Create(referer+".json");

            WebResponse wr = wrs.GetResponse() as HttpWebResponse;

                StreamReader rr = new StreamReader(wr.GetResponseStream());
                string res = rr.ReadToEnd();
                wr.Close();

            return res;

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            message = textMessage.Text;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void MessageWindow_Load(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(300);
            
            this.WindowState = FormWindowState.Normal;
            this.Focus();
            this.Activate();
        }
    }
}
