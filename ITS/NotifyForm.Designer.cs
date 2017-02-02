namespace ITSClient
{
    partial class NotifyForm
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
            this.avatar = new System.Windows.Forms.PictureBox();
            this.header = new System.Windows.Forms.Label();
            this.info = new System.Windows.Forms.Label();
            this.timeToClose = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.avatar)).BeginInit();
            this.SuspendLayout();
            // 
            // avatar
            // 
            this.avatar.ImageLocation = "https://cloud.ktg.kz/index.php/s/ZQMd6l4fOC8b8Dj/download?path=%2F&files=92071235" +
    "0990.jpg&downloadStartSecret=njdvkkmuj7kes1a0l9hhbyb9";
            this.avatar.Location = new System.Drawing.Point(0, 0);
            this.avatar.Name = "avatar";
            this.avatar.Size = new System.Drawing.Size(131, 161);
            this.avatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.avatar.TabIndex = 0;
            this.avatar.TabStop = false;
            // 
            // header
            // 
            this.header.AutoSize = true;
            this.header.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.header.Location = new System.Drawing.Point(136, 9);
            this.header.Name = "header";
            this.header.Size = new System.Drawing.Size(45, 22);
            this.header.TabIndex = 1;
            this.header.Text = "ФИО";
            // 
            // info
            // 
            this.info.AutoSize = true;
            this.info.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.info.Location = new System.Drawing.Point(137, 31);
            this.info.Name = "info";
            this.info.Size = new System.Drawing.Size(38, 16);
            this.info.TabIndex = 2;
            this.info.Text = "ИНФО";
            // 
            // timeToClose
            // 
            this.timeToClose.Tick += new System.EventHandler(this.timeToClose_Tick);
            // 
            // NotifyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 161);
            this.Controls.Add(this.info);
            this.Controls.Add(this.header);
            this.Controls.Add(this.avatar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "NotifyForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Notify";
            this.TopMost = true;
            this.Activated += new System.EventHandler(this.NotifyForm_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.NotifyForm_FormClosed);
            this.Load += new System.EventHandler(this.NotifyForm_Load);
            this.Shown += new System.EventHandler(this.NotifyForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.avatar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.PictureBox avatar;
        public System.Windows.Forms.Label header;
        public System.Windows.Forms.Label info;
        public System.Windows.Forms.Timer timeToClose;
 
    }
}