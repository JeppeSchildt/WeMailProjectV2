using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.IO;


namespace Email
{   
    public partial class Create : Window
    {
        public Create()
        {
            InitializeComponent();
            Tlf.MaxLength = 8;
        }
        public int GetIntAValue()
        {
            Random rnd = new Random();
            return rnd.Next(1000, 9999);
        }
        public void CAPTCHA()
        {
            int code = GetIntAValue();
            Label1.Content = code;
        }

        /*static string Encrypt(string value)                       // An other encrypt but can't decrypt
        {
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider()) 
            {
                UTF8Encoding utf8 = new UTF8Encoding();
                byte[] data = md5.ComputeHash(utf8.GetBytes(value));
                return Convert.ToBase64String(data);
            }
        }*/

        public string EncryptPass(string value)         // Encrypt function
        {
            byte[] data = UTF8Encoding.UTF8.GetBytes(CPassword.Text);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider()) {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(value));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 }) {
                    ICryptoTransform transform = tripDes.CreateEncryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    Console.WriteLine(Convert.ToBase64String(results, 0, results.Length));
                    return Convert.ToBase64String(results, 0, results.Length);
                }
            }
        }

        private bool TlfValid()
        {
            Regex validator = new Regex("[0-9]{8}");

            if (validator.Match(Tlf.Text).Success)
                return true;

            else
                return false;
        }

        public void Done_Click(object sender, RoutedEventArgs e)
        {

            int code = GetIntAValue();

            if (CUserName.Text == "" || CPassword.Text == "" || Tlf.Text == "")
            {
                MessageBox.Show("miss information!");

            }

            else if (Code.Text != "" + Label1.Content)
            {
                MessageBox.Show("Wrong CAPTCHA");
            }
           
            else if (TlfValid() == false)
            {
                MessageBox.Show("Wrong Phone Number");
            }

            else
            {
                Done.IsEnabled = false;
                Done.Content = "finish";
                StreamWriter sw = new StreamWriter("S:/Email/Email/UserName.txt", true);

                string dir = @"S:/Email/Email/" + CUserName.Text;
                // If directory does not exist, create it
                if (!Directory.Exists(dir)) {
                    Directory.CreateDirectory(dir);
                    
                    string inboxPath = dir + "/inbox";
                    string sentPath = dir + "/sent";
                    string draftsPath = dir + "/drafts";
                    
                    Directory.CreateDirectory(inboxPath);
                    Directory.CreateDirectory(sentPath);
                    Directory.CreateDirectory(draftsPath);
                }

                string CrypPassword = EncryptPass(CPassword.Text);
                sw.WriteLine(CUserName.Text + "," + CrypPassword+ "," + Tlf.Text);
                sw.Flush();
                sw.Close();
                this.Close();

                /* You could loop all of your window, and then check if it is hidden,
                 and then you could show it again, refer to below code snippet:    */
                foreach (Window window in App.Current.Windows)
                {
                    if (!window.IsActive)
                    {
                        window.Show();
                    }
                }
            }         
        }

        private void CUserName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (CUserName.Text == "User Name")
            {
                CUserName.Text = "";
            }
        }

        private void CPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            if (CPassword.Text != null)
            {
                CPassword.Text = null;
            }
        }

        private void Tlf_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Tlf.Text != null)
            {
                Tlf.Text = null;
            }
        }

        private void Text_changed(object sender, TextChangedEventArgs e)

        {


            if (CUserName != null && CPassword != null && Tlf != null)
            {
                if (CUserName.Text != "" && CPassword.Text != "" && Tlf.Text != "" && CUserName.Text != "User Name" && CPassword.Text != "Password" && Tlf.Text != "Tlf")
                    CAPTCHA();
            }
        }  
    }
}