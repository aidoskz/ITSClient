namespace ITSClient
{
    partial class MainWindow
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.NameUser = new System.Windows.Forms.Label();
            this.IPAdress = new System.Windows.Forms.Label();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.StripShowHideProgram = new System.Windows.Forms.ToolStripMenuItem();
            this.StripSendToSupport = new System.Windows.Forms.ToolStripMenuItem();
            this.labelNameUser = new System.Windows.Forms.Label();
            this.LabelIPAdress = new System.Windows.Forms.Label();
            this.labelNameMachine = new System.Windows.Forms.Label();
            this.TextNameMachine = new System.Windows.Forms.Label();
            this.buttonSendToSupport = new System.Windows.Forms.Button();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // NameUser
            // 
            this.NameUser.AutoSize = true;
            this.NameUser.Location = new System.Drawing.Point(124, 31);
            this.NameUser.Name = "NameUser";
            this.NameUser.Size = new System.Drawing.Size(57, 13);
            this.NameUser.TabIndex = 0;
            this.NameUser.Text = "NameUser";
            // 
            // IPAdress
            // 
            this.IPAdress.AutoSize = true;
            this.IPAdress.Location = new System.Drawing.Point(124, 53);
            this.IPAdress.Name = "IPAdress";
            this.IPAdress.Size = new System.Drawing.Size(49, 13);
            this.IPAdress.TabIndex = 2;
            this.IPAdress.Text = "IPAdress";
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipText = "ИТ Поддержка";
            this.notifyIcon.BalloonTipTitle = "IT Support";
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Visible = true;
            this.notifyIcon.DoubleClick += new System.EventHandler(this.notifyIcon_DoubleClick);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StripShowHideProgram,
            this.StripSendToSupport});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(385, 48);
            // 
            // StripShowHideProgram
            // 
            this.StripShowHideProgram.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.StripShowHideProgram.Name = "StripShowHideProgram";
            this.StripShowHideProgram.ShortcutKeyDisplayString = "";
            this.StripShowHideProgram.Size = new System.Drawing.Size(384, 22);
            this.StripShowHideProgram.Text = "IT Support";
            this.StripShowHideProgram.Click += new System.EventHandler(this.StripShowHideProgram_Click);
            // 
            // StripSendToSupport
            // 
            this.StripSendToSupport.Name = "StripSendToSupport";
            this.StripSendToSupport.ShortcutKeyDisplayString = "Ctrl + F5";
            this.StripSendToSupport.Size = new System.Drawing.Size(384, 22);
            this.StripSendToSupport.Text = "Отправить обращение в службу техподдержки";
            this.StripSendToSupport.Click += new System.EventHandler(this.StripSendToSupport_Click);
            // 
            // labelNameUser
            // 
            this.labelNameUser.AutoSize = true;
            this.labelNameUser.Location = new System.Drawing.Point(12, 31);
            this.labelNameUser.Name = "labelNameUser";
            this.labelNameUser.Size = new System.Drawing.Size(106, 13);
            this.labelNameUser.TabIndex = 3;
            this.labelNameUser.Text = "Имя пользователя:";
            // 
            // LabelIPAdress
            // 
            this.LabelIPAdress.AutoSize = true;
            this.LabelIPAdress.Location = new System.Drawing.Point(12, 53);
            this.LabelIPAdress.Name = "LabelIPAdress";
            this.LabelIPAdress.Size = new System.Drawing.Size(53, 13);
            this.LabelIPAdress.TabIndex = 4;
            this.LabelIPAdress.Text = "IP адрес:";
            // 
            // labelNameMachine
            // 
            this.labelNameMachine.AutoSize = true;
            this.labelNameMachine.Location = new System.Drawing.Point(12, 9);
            this.labelNameMachine.Name = "labelNameMachine";
            this.labelNameMachine.Size = new System.Drawing.Size(98, 13);
            this.labelNameMachine.TabIndex = 5;
            this.labelNameMachine.Text = "Имя компьютера:";
            // 
            // TextNameMachine
            // 
            this.TextNameMachine.AutoSize = true;
            this.TextNameMachine.Location = new System.Drawing.Point(124, 9);
            this.TextNameMachine.Name = "TextNameMachine";
            this.TextNameMachine.Size = new System.Drawing.Size(76, 13);
            this.TextNameMachine.TabIndex = 6;
            this.TextNameMachine.Text = "NameMachine";
            // 
            // buttonSendToSupport
            // 
            this.buttonSendToSupport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSendToSupport.Location = new System.Drawing.Point(12, 69);
            this.buttonSendToSupport.Name = "buttonSendToSupport";
            this.buttonSendToSupport.Size = new System.Drawing.Size(434, 93);
            this.buttonSendToSupport.TabIndex = 1;
            this.buttonSendToSupport.Text = "Отправить обращение в службу технической поддержки";
            this.buttonSendToSupport.UseVisualStyleBackColor = true;
            this.buttonSendToSupport.Click += new System.EventHandler(this.buttonSendToSupport_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 171);
            this.Controls.Add(this.TextNameMachine);
            this.Controls.Add(this.labelNameMachine);
            this.Controls.Add(this.LabelIPAdress);
            this.Controls.Add(this.labelNameUser);
            this.Controls.Add(this.IPAdress);
            this.Controls.Add(this.buttonSendToSupport);
            this.Controls.Add(this.NameUser);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainWindow";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IT Support";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label NameUser;
        private System.Windows.Forms.Label IPAdress;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Label labelNameUser;
        private System.Windows.Forms.Label LabelIPAdress;
        private System.Windows.Forms.Label labelNameMachine;
        private System.Windows.Forms.Label TextNameMachine;
        private System.Windows.Forms.Button buttonSendToSupport;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem StripShowHideProgram;
        private System.Windows.Forms.ToolStripMenuItem StripSendToSupport;
    }
}

