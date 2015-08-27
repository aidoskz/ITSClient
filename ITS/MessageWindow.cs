using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
 

 

namespace ITSClient
{
    public partial class MessageWindow : Form
    {
        public string message;
        

        public MessageWindow()
        {
            InitializeComponent();
            
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
