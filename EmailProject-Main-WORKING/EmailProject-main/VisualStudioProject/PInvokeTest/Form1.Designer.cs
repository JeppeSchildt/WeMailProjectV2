namespace PInvokeTest
{
    partial class WEMAIL
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
            this.sendButton = new System.Windows.Forms.Button();
            this.reciptxtbox = new System.Windows.Forms.TextBox();
            this.subjecttxtbox = new System.Windows.Forms.TextBox();
            this.RecipientLBL = new System.Windows.Forms.Label();
            this.SubjectLBL = new System.Windows.Forms.Label();
            this.MsgLBL = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.messageQueue1 = new System.Messaging.MessageQueue();
            this.messagetxtbox = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // sendButton
            // 
            this.sendButton.Location = new System.Drawing.Point(327, 367);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(276, 127);
            this.sendButton.TabIndex = 1;
            this.sendButton.Text = "Send Mail";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // reciptxtbox
            // 
            this.reciptxtbox.Location = new System.Drawing.Point(327, 37);
            this.reciptxtbox.Name = "reciptxtbox";
            this.reciptxtbox.Size = new System.Drawing.Size(276, 20);
            this.reciptxtbox.TabIndex = 2;
            // 
            // subjecttxtbox
            // 
            this.subjecttxtbox.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.subjecttxtbox.Location = new System.Drawing.Point(327, 107);
            this.subjecttxtbox.Name = "subjecttxtbox";
            this.subjecttxtbox.Size = new System.Drawing.Size(276, 20);
            this.subjecttxtbox.TabIndex = 3;
            // 
            // RecipientLBL
            // 
            this.RecipientLBL.AutoSize = true;
            this.RecipientLBL.Font = new System.Drawing.Font("Verdana", 15F);
            this.RecipientLBL.Location = new System.Drawing.Point(412, 9);
            this.RecipientLBL.Name = "RecipientLBL";
            this.RecipientLBL.Size = new System.Drawing.Size(114, 25);
            this.RecipientLBL.TabIndex = 5;
            this.RecipientLBL.Text = "Recipient:";
            this.RecipientLBL.Click += new System.EventHandler(this.label4_Click);
            // 
            // SubjectLBL
            // 
            this.SubjectLBL.AutoSize = true;
            this.SubjectLBL.Font = new System.Drawing.Font("Verdana", 15F);
            this.SubjectLBL.Location = new System.Drawing.Point(412, 79);
            this.SubjectLBL.Name = "SubjectLBL";
            this.SubjectLBL.Size = new System.Drawing.Size(97, 25);
            this.SubjectLBL.TabIndex = 6;
            this.SubjectLBL.Text = "Subject:";
            // 
            // MsgLBL
            // 
            this.MsgLBL.AutoSize = true;
            this.MsgLBL.Font = new System.Drawing.Font("Verdana", 15F);
            this.MsgLBL.Location = new System.Drawing.Point(412, 148);
            this.MsgLBL.Name = "MsgLBL";
            this.MsgLBL.Size = new System.Drawing.Size(106, 25);
            this.MsgLBL.TabIndex = 7;
            this.MsgLBL.Text = "Message:";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // messageQueue1
            // 
            this.messageQueue1.MessageReadPropertyFilter.LookupId = true;
            this.messageQueue1.SynchronizingObject = this;
            // 
            // messagetxtbox
            // 
            this.messagetxtbox.Location = new System.Drawing.Point(327, 176);
            this.messagetxtbox.Name = "messagetxtbox";
            this.messagetxtbox.Size = new System.Drawing.Size(276, 136);
            this.messagetxtbox.TabIndex = 8;
            this.messagetxtbox.Text = "";
            // 
            // WEMAIL
            // 
            this.ClientSize = new System.Drawing.Size(950, 521);
            this.Controls.Add(this.messagetxtbox);
            this.Controls.Add(this.MsgLBL);
            this.Controls.Add(this.SubjectLBL);
            this.Controls.Add(this.RecipientLBL);
            this.Controls.Add(this.subjecttxtbox);
            this.Controls.Add(this.reciptxtbox);
            this.Controls.Add(this.sendButton);
            this.Name = "WEMAIL";
            this.Text = "*";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.TextBox reciptxtbox;
        private System.Windows.Forms.TextBox subjecttxtbox;
        private System.Windows.Forms.Label RecipientLBL;
        private System.Windows.Forms.Label SubjectLBL;
        private System.Windows.Forms.Label MsgLBL;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Messaging.MessageQueue messageQueue1;
        private System.Windows.Forms.RichTextBox messagetxtbox;
    }
}