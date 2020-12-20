using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using FontAwesome.Sharp;
using GUI.Resources.Classes;
using GUI.Resources.Forms;

namespace GUI
{
    public partial class Form1 : Form
    {
        //Fields
        private IconButton currentBtn;
        private Panel leftBorderBtn;
        private Form currentChildForm;

        public loginForm previousLoginForm;
        public UserAccount currentUser; 

        //Constructor
        public Form1()
        {
            InitializeComponent();
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 60);
            panelMenu.Controls.Add(leftBorderBtn);
            //Form
            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }

        //Color struct
        private struct RGBColors
        {
            public static Color white = Color.FromArgb(0, 0, 0);
            public static Color jet = Color.FromArgb(51, 51, 51);
            public static Color eerie_Black = Color.FromArgb(32, 29, 29);
            public static Color charcoal = Color.FromArgb(29, 55, 73);
            public static Color blue_Munsell = Color.FromArgb(38, 145, 166);

        }

        //Methods
        private void ActivateButton(object senderBtn, Color color)
        {
            if (senderBtn != null)
            {
                DisableButton();

                //Button
                currentBtn = (IconButton)senderBtn;
                currentBtn.BackColor = RGBColors.jet;
                currentBtn.ForeColor = color;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = color;
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;

                //Left border button 
                leftBorderBtn.BackColor = color;
                leftBorderBtn.Location = new Point(0, currentBtn.Location.Y);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();

                //Icon current child form
                iconCurrentChildForm.IconChar = currentBtn.IconChar;
                iconCurrentChildForm.IconColor = color;
            }
        }

        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = RGBColors.eerie_Black;
                currentBtn.ForeColor = RGBColors.blue_Munsell;
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor = RGBColors.blue_Munsell;
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }

        private void OpenChildForm(Form childForm)
        {
            if (currentChildForm != null)
            {
                //Open only one form at a time
                currentChildForm.Close();
            }
            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelDesktop.Controls.Add(childForm);
            panelDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            lblTitleChildForm.Text = childForm.Text;
        }

        private void newEmailBtn_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.blue_Munsell);
            sendEmailForm sendForm = new sendEmailForm();
            sendForm.mainFormReference = this;
            OpenChildForm(sendForm); 

            //OpenChildForm(new sendEmailForm());
            lblTitleChildForm.Text = "New email";
        }

        private void InboxBtn_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.blue_Munsell);
            OpenChildForm(new FormInbox());
            lblTitleChildForm.Text = "Inbox";
        }

        private void OutboxBtn_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.blue_Munsell);
            OpenChildForm(new FormOutbox());
            lblTitleChildForm.Text = "Outbox";
        }

        private void DraftsBtn_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.blue_Munsell);
            OpenChildForm(new FormDrafts());
            lblTitleChildForm.Text = "Drafts";
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            currentChildForm.Close();
            Reset();
        }

        private void Reset()
        {
            DisableButton();
            leftBorderBtn.Visible = false;
            iconCurrentChildForm.IconChar = IconChar.Home;
            iconCurrentChildForm.IconColor = RGBColors.blue_Munsell;
            lblTitleChildForm.Text = "Home";
        }

        //Drag form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnExitApp_Click(object sender, EventArgs e)
        {
            Application.Exit(); 
        }

        private void btnMinimizeApp_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized; 
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
            previousLoginForm.Show();
        }

        private void iconPictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
            previousLoginForm.Show(); 
        }

        public T deserializer<T>(T desObj, string data)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(desObj.GetType());
            StringReader stringified = new StringReader(data);
            desObj = (T)xmlSerializer.Deserialize(stringified);
            return desObj;
        }
        public string serializer<T>(T sObj)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(sObj.GetType());
            StringWriter stringified = new StringWriter();
            xmlSerializer.Serialize(stringified, sObj);
            string objSerialized = stringified.ToString();
            return objSerialized;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
