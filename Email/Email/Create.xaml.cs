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
using System.Net.Sockets;
using System.Net.Mail;
using System.Xml.Serialization;

namespace CLIENT
{   public partial class Create : Window
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
                MessageBox.Show("You're missing information!");

            }
            else if (Code.Text != "" + Label1.Content)
            {
                MessageBox.Show("Wrong CAPTCHA");
            }
       
            else if (!(TlfValid())) //IF TLF returns false
            {
                MessageBox.Show("Wrong Phone Number");
            }
            else //If all information is okay
            {
                char DL = '';
                string UserName = CUserName.Text;
                string CrypPassword = EncryptPass(CPassword.Text);
                string TelephoneNr = Tlf.Text;
                /////////////////////
                /// SERIALISATION ///
                /////////////////////
                Console.WriteLine("Beginning Serialisation...\n");
                const int PORT_NO = 5000;
                const string LOCALHOST = "127.0.0.1";
                TcpClient tcpclient = new TcpClient(LOCALHOST, PORT_NO);
                NetworkStream nwStream = tcpclient.GetStream();
                UserAccount UserAcc = new UserAccount(UserName,CrypPassword,TelephoneNr);       
                XmlSerializer xmlSerializer = new XmlSerializer(UserAcc.GetType());
                StringWriter stringified = new StringWriter();
                xmlSerializer.Serialize(stringified, UserAcc);
                string res = LogIn.userID + DL + "CREATEUSER" + DL + stringified.ToString();
                byte[] bytesToSend = ASCIIEncoding.UTF8.GetBytes(res);
                Console.WriteLine("Sending : " + res);
                nwStream.Write(bytesToSend, 0, bytesToSend.Length);


                //---read back the text---
                byte[] bytesToRead = new byte[tcpclient.ReceiveBufferSize];
                int bytesRead = nwStream.Read(bytesToRead, 0, tcpclient.ReceiveBufferSize);
                //string returnsignal = Encoding.UTF8.GetString(bytesToRead, 0, bytesRead);

                //gets return signal but needs return class aswell.. check serialization
                MessageBox.Show(Encoding.ASCII.GetString(bytesToRead, 0, bytesRead) /* returnsignal*/); //Recieves true if success, false if error. Should be changed to a exception if error later. 
                tcpclient.Close();

                ////////////////////////////
                /// END-OF-SERIALISATION ///
                ///////////////////////////

                Done.IsEnabled = false;
                Done.Content = "finish";

                this.Close(); //NEEDS TO ONLY CLOSE WHEN RECIEVING SUCCESS SIGNAL
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
    public class UserAccount
    {
        public string UserName, PassWord, PhoneNumber;
        public UserAccount() { }
        public UserAccount(string UN, string Pass, string TLF)
        {
            UserName = UN;
            PassWord = Pass;
            PhoneNumber = TLF;

        }
    }

}



