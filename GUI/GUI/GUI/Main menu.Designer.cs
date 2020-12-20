
namespace GUI
{
    partial class Form1
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
            this.panelMenu = new System.Windows.Forms.Panel();
            this.logoutLabel = new System.Windows.Forms.Label();
            this.DraftsBtn = new FontAwesome.Sharp.IconButton();
            this.Logout = new FontAwesome.Sharp.IconPictureBox();
            this.OutboxBtn = new FontAwesome.Sharp.IconButton();
            this.InboxBtn = new FontAwesome.Sharp.IconButton();
            this.newEmailBtn = new FontAwesome.Sharp.IconButton();
            this.panelLogo = new System.Windows.Forms.Panel();
            this.btnHome = new System.Windows.Forms.PictureBox();
            this.panelTitleBar = new System.Windows.Forms.Panel();
            this.btnMinimizeApp = new FontAwesome.Sharp.IconButton();
            this.btnExitApp = new FontAwesome.Sharp.IconButton();
            this.lblTitleChildForm = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.iconCurrentChildForm = new FontAwesome.Sharp.IconPictureBox();
            this.panelShadow = new System.Windows.Forms.Panel();
            this.panelDesktop = new System.Windows.Forms.Panel();
            this.homeLogo = new System.Windows.Forms.PictureBox();
            this.panelMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Logout)).BeginInit();
            this.panelLogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnHome)).BeginInit();
            this.panelTitleBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconCurrentChildForm)).BeginInit();
            this.panelDesktop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.homeLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.panelMenu.Controls.Add(this.logoutLabel);
            this.panelMenu.Controls.Add(this.DraftsBtn);
            this.panelMenu.Controls.Add(this.Logout);
            this.panelMenu.Controls.Add(this.OutboxBtn);
            this.panelMenu.Controls.Add(this.InboxBtn);
            this.panelMenu.Controls.Add(this.newEmailBtn);
            this.panelMenu.Controls.Add(this.panelLogo);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(220, 514);
            this.panelMenu.TabIndex = 0;
            // 
            // logoutLabel
            // 
            this.logoutLabel.AutoSize = true;
            this.logoutLabel.Font = new System.Drawing.Font("MS Reference Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logoutLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(145)))), ((int)(((byte)(166)))));
            this.logoutLabel.Location = new System.Drawing.Point(52, 471);
            this.logoutLabel.Name = "logoutLabel";
            this.logoutLabel.Size = new System.Drawing.Size(65, 20);
            this.logoutLabel.TabIndex = 6;
            this.logoutLabel.Text = "Logout";
            this.logoutLabel.Click += new System.EventHandler(this.label2_Click);
            // 
            // DraftsBtn
            // 
            this.DraftsBtn.Dock = System.Windows.Forms.DockStyle.Top;
            this.DraftsBtn.FlatAppearance.BorderSize = 0;
            this.DraftsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DraftsBtn.Font = new System.Drawing.Font("MS Reference Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DraftsBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(145)))), ((int)(((byte)(166)))));
            this.DraftsBtn.IconChar = FontAwesome.Sharp.IconChar.FileAlt;
            this.DraftsBtn.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(145)))), ((int)(((byte)(166)))));
            this.DraftsBtn.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.DraftsBtn.IconSize = 32;
            this.DraftsBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.DraftsBtn.Location = new System.Drawing.Point(0, 320);
            this.DraftsBtn.Name = "DraftsBtn";
            this.DraftsBtn.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.DraftsBtn.Size = new System.Drawing.Size(220, 60);
            this.DraftsBtn.TabIndex = 4;
            this.DraftsBtn.Text = "Drafts";
            this.DraftsBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.DraftsBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.DraftsBtn.UseVisualStyleBackColor = true;
            this.DraftsBtn.Click += new System.EventHandler(this.DraftsBtn_Click);
            // 
            // Logout
            // 
            this.Logout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.Logout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(145)))), ((int)(((byte)(166)))));
            this.Logout.IconChar = FontAwesome.Sharp.IconChar.SignOutAlt;
            this.Logout.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(145)))), ((int)(((byte)(166)))));
            this.Logout.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.Logout.Location = new System.Drawing.Point(16, 465);
            this.Logout.Name = "Logout";
            this.Logout.Size = new System.Drawing.Size(32, 32);
            this.Logout.TabIndex = 5;
            this.Logout.TabStop = false;
            this.Logout.Click += new System.EventHandler(this.iconPictureBox1_Click);
            // 
            // OutboxBtn
            // 
            this.OutboxBtn.Dock = System.Windows.Forms.DockStyle.Top;
            this.OutboxBtn.FlatAppearance.BorderSize = 0;
            this.OutboxBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OutboxBtn.Font = new System.Drawing.Font("MS Reference Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OutboxBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(145)))), ((int)(((byte)(166)))));
            this.OutboxBtn.IconChar = FontAwesome.Sharp.IconChar.Share;
            this.OutboxBtn.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(145)))), ((int)(((byte)(166)))));
            this.OutboxBtn.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.OutboxBtn.IconSize = 32;
            this.OutboxBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.OutboxBtn.Location = new System.Drawing.Point(0, 260);
            this.OutboxBtn.Name = "OutboxBtn";
            this.OutboxBtn.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.OutboxBtn.Size = new System.Drawing.Size(220, 60);
            this.OutboxBtn.TabIndex = 3;
            this.OutboxBtn.Text = "Outbox";
            this.OutboxBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.OutboxBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.OutboxBtn.UseVisualStyleBackColor = true;
            this.OutboxBtn.Click += new System.EventHandler(this.OutboxBtn_Click);
            // 
            // InboxBtn
            // 
            this.InboxBtn.Dock = System.Windows.Forms.DockStyle.Top;
            this.InboxBtn.FlatAppearance.BorderSize = 0;
            this.InboxBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.InboxBtn.Font = new System.Drawing.Font("MS Reference Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InboxBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(145)))), ((int)(((byte)(166)))));
            this.InboxBtn.IconChar = FontAwesome.Sharp.IconChar.Inbox;
            this.InboxBtn.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(145)))), ((int)(((byte)(166)))));
            this.InboxBtn.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.InboxBtn.IconSize = 32;
            this.InboxBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.InboxBtn.Location = new System.Drawing.Point(0, 200);
            this.InboxBtn.Name = "InboxBtn";
            this.InboxBtn.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.InboxBtn.Size = new System.Drawing.Size(220, 60);
            this.InboxBtn.TabIndex = 2;
            this.InboxBtn.Text = "Inbox";
            this.InboxBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.InboxBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.InboxBtn.UseVisualStyleBackColor = true;
            this.InboxBtn.Click += new System.EventHandler(this.InboxBtn_Click);
            // 
            // newEmailBtn
            // 
            this.newEmailBtn.Dock = System.Windows.Forms.DockStyle.Top;
            this.newEmailBtn.FlatAppearance.BorderSize = 0;
            this.newEmailBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.newEmailBtn.Font = new System.Drawing.Font("MS Reference Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newEmailBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(145)))), ((int)(((byte)(166)))));
            this.newEmailBtn.IconChar = FontAwesome.Sharp.IconChar.Envelope;
            this.newEmailBtn.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(145)))), ((int)(((byte)(166)))));
            this.newEmailBtn.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.newEmailBtn.IconSize = 32;
            this.newEmailBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.newEmailBtn.Location = new System.Drawing.Point(0, 140);
            this.newEmailBtn.Name = "newEmailBtn";
            this.newEmailBtn.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.newEmailBtn.Size = new System.Drawing.Size(220, 60);
            this.newEmailBtn.TabIndex = 1;
            this.newEmailBtn.Text = "New email";
            this.newEmailBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.newEmailBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.newEmailBtn.UseVisualStyleBackColor = true;
            this.newEmailBtn.Click += new System.EventHandler(this.newEmailBtn_Click);
            // 
            // panelLogo
            // 
            this.panelLogo.Controls.Add(this.btnHome);
            this.panelLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLogo.Location = new System.Drawing.Point(0, 0);
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.panelLogo.Size = new System.Drawing.Size(220, 140);
            this.panelLogo.TabIndex = 0;
            // 
            // btnHome
            // 
            this.btnHome.Image = global::GUI.Properties.Resources.WeMailLogo1;
            this.btnHome.Location = new System.Drawing.Point(0, 0);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(220, 82);
            this.btnHome.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnHome.TabIndex = 0;
            this.btnHome.TabStop = false;
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(55)))), ((int)(((byte)(73)))));
            this.panelTitleBar.Controls.Add(this.btnMinimizeApp);
            this.panelTitleBar.Controls.Add(this.btnExitApp);
            this.panelTitleBar.Controls.Add(this.lblTitleChildForm);
            this.panelTitleBar.Controls.Add(this.label1);
            this.panelTitleBar.Controls.Add(this.iconCurrentChildForm);
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleBar.Location = new System.Drawing.Point(220, 0);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(888, 75);
            this.panelTitleBar.TabIndex = 1;
            this.panelTitleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTitleBar_MouseDown);
            // 
            // btnMinimizeApp
            // 
            this.btnMinimizeApp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinimizeApp.FlatAppearance.BorderSize = 0;
            this.btnMinimizeApp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimizeApp.IconChar = FontAwesome.Sharp.IconChar.WindowMinimize;
            this.btnMinimizeApp.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(145)))), ((int)(((byte)(166)))));
            this.btnMinimizeApp.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnMinimizeApp.IconSize = 32;
            this.btnMinimizeApp.Location = new System.Drawing.Point(816, 0);
            this.btnMinimizeApp.Name = "btnMinimizeApp";
            this.btnMinimizeApp.Size = new System.Drawing.Size(32, 32);
            this.btnMinimizeApp.TabIndex = 4;
            this.btnMinimizeApp.UseVisualStyleBackColor = true;
            this.btnMinimizeApp.Click += new System.EventHandler(this.btnMinimizeApp_Click);
            // 
            // btnExitApp
            // 
            this.btnExitApp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExitApp.FlatAppearance.BorderSize = 0;
            this.btnExitApp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExitApp.IconChar = FontAwesome.Sharp.IconChar.Times;
            this.btnExitApp.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(145)))), ((int)(((byte)(166)))));
            this.btnExitApp.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnExitApp.IconSize = 32;
            this.btnExitApp.Location = new System.Drawing.Point(854, 3);
            this.btnExitApp.Name = "btnExitApp";
            this.btnExitApp.Size = new System.Drawing.Size(32, 32);
            this.btnExitApp.TabIndex = 0;
            this.btnExitApp.UseVisualStyleBackColor = true;
            this.btnExitApp.Click += new System.EventHandler(this.btnExitApp_Click);
            // 
            // lblTitleChildForm
            // 
            this.lblTitleChildForm.AutoSize = true;
            this.lblTitleChildForm.Font = new System.Drawing.Font("MS Reference Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleChildForm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(145)))), ((int)(((byte)(166)))));
            this.lblTitleChildForm.Location = new System.Drawing.Point(56, 29);
            this.lblTitleChildForm.Name = "lblTitleChildForm";
            this.lblTitleChildForm.Size = new System.Drawing.Size(56, 20);
            this.lblTitleChildForm.TabIndex = 2;
            this.lblTitleChildForm.Text = "Home";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 0;
            // 
            // iconCurrentChildForm
            // 
            this.iconCurrentChildForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(55)))), ((int)(((byte)(73)))));
            this.iconCurrentChildForm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(145)))), ((int)(((byte)(166)))));
            this.iconCurrentChildForm.IconChar = FontAwesome.Sharp.IconChar.Home;
            this.iconCurrentChildForm.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(145)))), ((int)(((byte)(166)))));
            this.iconCurrentChildForm.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconCurrentChildForm.Location = new System.Drawing.Point(20, 23);
            this.iconCurrentChildForm.Name = "iconCurrentChildForm";
            this.iconCurrentChildForm.Size = new System.Drawing.Size(32, 32);
            this.iconCurrentChildForm.TabIndex = 0;
            this.iconCurrentChildForm.TabStop = false;
            // 
            // panelShadow
            // 
            this.panelShadow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.panelShadow.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelShadow.Location = new System.Drawing.Point(220, 75);
            this.panelShadow.Name = "panelShadow";
            this.panelShadow.Size = new System.Drawing.Size(888, 9);
            this.panelShadow.TabIndex = 2;
            // 
            // panelDesktop
            // 
            this.panelDesktop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.panelDesktop.Controls.Add(this.homeLogo);
            this.panelDesktop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDesktop.Location = new System.Drawing.Point(220, 84);
            this.panelDesktop.Name = "panelDesktop";
            this.panelDesktop.Size = new System.Drawing.Size(888, 430);
            this.panelDesktop.TabIndex = 3;
            // 
            // homeLogo
            // 
            this.homeLogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.homeLogo.Image = global::GUI.Properties.Resources.WeMailLogo3;
            this.homeLogo.Location = new System.Drawing.Point(0, 0);
            this.homeLogo.Name = "homeLogo";
            this.homeLogo.Size = new System.Drawing.Size(888, 430);
            this.homeLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.homeLogo.TabIndex = 0;
            this.homeLogo.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1108, 514);
            this.Controls.Add(this.panelDesktop);
            this.Controls.Add(this.panelShadow);
            this.Controls.Add(this.panelTitleBar);
            this.Controls.Add(this.panelMenu);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1148, 614);
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "WeMail";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panelMenu.ResumeLayout(false);
            this.panelMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Logout)).EndInit();
            this.panelLogo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnHome)).EndInit();
            this.panelTitleBar.ResumeLayout(false);
            this.panelTitleBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconCurrentChildForm)).EndInit();
            this.panelDesktop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.homeLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMenu;
        private FontAwesome.Sharp.IconButton newEmailBtn;
        private System.Windows.Forms.Panel panelLogo;
        private FontAwesome.Sharp.IconButton DraftsBtn;
        private FontAwesome.Sharp.IconButton OutboxBtn;
        private FontAwesome.Sharp.IconButton InboxBtn;
        private System.Windows.Forms.PictureBox btnHome;
        private System.Windows.Forms.Panel panelTitleBar;
        private FontAwesome.Sharp.IconPictureBox iconCurrentChildForm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTitleChildForm;
        private System.Windows.Forms.Panel panelShadow;
        private System.Windows.Forms.Panel panelDesktop;
        private FontAwesome.Sharp.IconButton btnMinimizeApp;
        private FontAwesome.Sharp.IconButton btnExitApp;
        private System.Windows.Forms.PictureBox homeLogo;
        private System.Windows.Forms.Label logoutLabel;
        private FontAwesome.Sharp.IconPictureBox Logout;
    }
}

