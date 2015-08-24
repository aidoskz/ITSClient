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

        public string GetScreenShotLink()
        {
            FileStream fs = new FileStream("d:\\ra.png", FileMode.Open, FileAccess.Read);
            byte[] data = new byte[fs.Length];
            fs.Read(data, 0, data.Length);
            fs.Close();

            // Generate post objects
            Dictionary<string, object> postParameters = new Dictionary<string, object>();
            postParameters.Add("filename",  "ra.png");
            postParameters.Add("fileformat", "png");
            postParameters.Add("file[0]", new FormUpload.FileParameter(data, "ra.png", "image/png"));

            // Create request and receive response
            string postURL = "http://storage.ktga.kz/upload.php";
            string userAgent = "ITSClient";
            string referer = "http://storage.ktga.kz/test12";
            HttpWebResponse webResponse = FormUpload.MultipartFormDataPost(postURL, userAgent, postParameters,referer);

            // Process response
            StreamReader responseReader = new StreamReader(webResponse.GetResponseStream());
            string fullResponse = responseReader.ReadToEnd();
            webResponse.Close();
            
                WebRequest wrs = WebRequest.Create("http://storage.ktga.kz/test12.json");

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
