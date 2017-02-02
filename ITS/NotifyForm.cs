using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ITSClient
{
    public partial class NotifyForm : Form
    {
        private static List<NotifyForm> openForms = new List<NotifyForm>(); 
        private static Dictionary<string, NotifyForm> openedForms = new Dictionary<string, NotifyForm>();

        private bool allowFocus = false;

        private FormAnimator animator;

        private IntPtr currentForegroundWindow;
 
        [DllImport("user32")]
        private static extern IntPtr GetForegroundWindow();
 
        [DllImport("user32")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        public NotifyForm( string iin, string fio, string adinfo)
        {
            InitializeComponent();

            this.avatar.Load("https://cloud.ktg.kz/index.php/s/ZQMd6l4fOC8b8Dj/download?path=%2F&files=" + iin + ".jpg&downloadStartSecret=njdvkkmuj7kes1a0l9hhbyb9");
            this.header.Text = fio; 
            this.info.Text = adinfo;
            this.animator = new FormAnimator(this,
                                             FormAnimator.AnimationMethod.Slide,
                                             FormAnimator.AnimationDirection.Up,
                                             500);
        }

        public new void Show()
        {
            
            // Determine the current foreground window so it can be reactivated each time this form tries to get the focus.
            this.currentForegroundWindow = GetForegroundWindow();

            // Display the form.
            base.Show();
        }

        private void NotifyForm_Load(object sender, EventArgs e)
        {
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width - 5,
                                     Screen.PrimaryScreen.WorkingArea.Height - this.Height - 5);
            // Move each open form upwards to make room for this one.
            foreach (KeyValuePair<string, NotifyForm> entry in NotifyForm.openedForms)
            {
                entry.Value.Top -= this.Height + 5;
            }

            if (!NotifyForm.openedForms.ContainsKey(this.header.Text))
            {
                NotifyForm.openedForms.Add(this.header.Text, this);
            }

 

            // Add this form from the open form list.
            NotifyForm.openForms.Add(this);
            this.timeToClose.Interval = 5000;
            this.timeToClose.Start();
        }
  

        private void timeToClose_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NotifyForm_Activated(object sender, EventArgs e)
        {
            if (!this.allowFocus)
            {
                // Activate the window that previously had the focus.
                SetForegroundWindow(this.currentForegroundWindow);
            }
        }

        private void NotifyForm_Shown(object sender, EventArgs e)
        {
            // Once the animation has completed the form can receive focus.
            this.allowFocus = true;



            // Close the form by sliding down.
            this.animator.Direction = FormAnimator.AnimationDirection.Down;
        }

        private void NotifyForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (KeyValuePair<string, NotifyForm> entry in NotifyForm.openedForms)
            {
                entry.Value.Top += this.Height + 5;

            }

            if (NotifyForm.openedForms.ContainsKey(this.header.Text))
            {
                NotifyForm.openedForms.Remove(this.header.Text);
            }
        }
    }
}
