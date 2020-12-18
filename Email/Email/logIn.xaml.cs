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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Security.Cryptography;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Net.Mail;
using System.Xml.Serialization;

namespace CLIENT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LogIn : Window 
    {
        public static string dbdir;
        public LogIn()
        {
            InitializeComponent();
            string currentdir = Environment.CurrentDirectory; //Gets location of exe file
            //MessageBox.Show(currentdir);
            try
            {
                while (!(currentdir.EndsWith(@"\WeMailProjectV2\Email")))
                {
                    currentdir = currentdir.Substring(0, currentdir.LastIndexOf(@"\"));
                   // MessageBox.Show(currentdir);
                }
            }
            catch
            {
                MessageBox.Show("Error setting the Working Directory! \n" + @"Make sure your WeMailV2\Email\"+"\nDirectory is working and named as such! ");
            }
            // TESTING FUNCTION:
            //MessageBox.Show("Error setting the Working Directory! \n" + "Make sure your: \n" + @"WeMailV2\Email\ Directory" + "\n is working and named as such! ");

            dbdir = currentdir;
            //MessageBox.Show("LOCATION: \n" +dbdir);
            //TESTING FUNCTION:
            MessageBox.Show("Database directory :" + dbdir);
        }

        public static string userID;
        public void UserID_GotFocus(object sender, RoutedEventArgs e)
        {
            if (UserID.Text == "User ID")
                UserID.Text = "";
        }

        public void Password_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Password.Password == "Password")
                Password.Password = "";
        }

        static string EncryptPass(string value)     // Decrypt function
        {
            byte[] data = UTF8Encoding.UTF8.GetBytes(value);
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
        private void logIn_Click(object sender, RoutedEventArgs e)
        {
            
            char DL = '';
            string UserName = UserID.Text;
            string PassW = EncryptPass(Password.Password);

            /////////////////////
            /// SERIALISATION ///
            /////////////////////
            Console.WriteLine("Beginning Serialisation...\n");
            const int PORT_NO = 5000;
            const int PORT_N1 = 5001;
            const string LOCALHOST = "127.0.0.1";
            TcpClient tcpclient = new TcpClient(LOCALHOST, PORT_NO);
            NetworkStream nwStream = tcpclient.GetStream();
            LoginAttempt loginattempt = new LoginAttempt(UserName, PassW);
            XmlSerializer xmlSerializer = new XmlSerializer(loginattempt.GetType());
            StringWriter stringified = new StringWriter();
            xmlSerializer.Serialize(stringified, loginattempt);
            string res = LogIn.userID + DL + "LOGIN" + DL + stringified.ToString();
            byte[] bytesToSend = ASCIIEncoding.UTF8.GetBytes(res);
            Console.WriteLine("Sending : " + res);
            nwStream.Write(bytesToSend, 0, bytesToSend.Length);

            //---read back the text---
            byte[] bytesToRead = new byte[tcpclient.ReceiveBufferSize];
            int bytesRead = nwStream.Read(bytesToRead, 0, tcpclient.ReceiveBufferSize);
            //string returnsignal = Encoding.UTF8.GetString(bytesToRead, 0, bytesRead);

            //gets return signal but needs return class aswell.. check serialization
            Console.WriteLine("Yall?\n"+Encoding.ASCII.GetString(bytesToRead, 0, bytesRead)); /* returnsignal*//*); //Recieves true if success, false if error. Should be changed to a exception if error later. 
            */
            tcpclient.Close();
            IPAddress localaddress = IPAddress.Parse(LOCALHOST);
            TcpListener STClistener = new TcpListener(localaddress, PORT_N1);
            Console.WriteLine("Listening to server ay:");
            STClistener.Start();
            TcpClient STCclient = STClistener.AcceptTcpClient();
            NetworkStream nwStreamFromServer = STCclient.GetStream();
            byte[] serverbuffer = new byte[STCclient.ReceiveBufferSize];
            //read
            int bytesfromserver = nwStreamFromServer.Read(serverbuffer, 0, STCclient.ReceiveBufferSize);
            //convert into string
            string datafromserver = Encoding.ASCII.GetString(serverbuffer, 0, bytesfromserver);
            Console.WriteLine("Info from server: "+datafromserver);
            if (datafromserver.Equals("success"))
            {
                Inbox inbox = new Inbox();            // show the next window
                inbox.Show();
                this.Hide();
            }
            STCclient.Close();
            STClistener.Stop();

            ////////////////////////////
            /// END-OF-SERIALISATION ///
            /////////////////////////// 


            /*
            
            string InfoList = dbdir+@"\UserName.txt"; // \WeMailProjectV2\Email
            //MessageBox.Show("info: \n" + InfoList);
            if (!File.Exists(InfoList)) { //If UserName.txt does not exist, make it. 
                using (StreamWriter sw = File.CreateText(InfoList)) {
                    sw.Flush();
                    sw.Close();
                }
            }


            using (var sr = new StreamReader(InfoList))  //Open UserName.txt
            {
                string Decrypt = DecryptPass(Password.Password); //Encrypts the password to check against the one in storage
                while (!sr.EndOfStream) {
                    var line = sr.ReadLine();
                    string[] words = line.Split(',');
                    if (String.IsNullOrEmpty(line)) continue;
                    if (words[0].IndexOf(UserID.Text, StringComparison.CurrentCultureIgnoreCase) >= 0 && words[1].IndexOf(Decrypt, StringComparison.CurrentCultureIgnoreCase) >= 0)
                    // if string  is found, return +1   if not return -1, if empty return 0
                    {
                        userID = UserID.Text; 
                        Inbox inbox = new Inbox();            // show the next window
                        inbox.Show();
                        this.Hide();
                        return;
                    }
                }
                //IF SUCCESS DO:
                //userID = UserID.Text; //not sure if still uses
                /*
                Inbox inbox = new Inbox();            // show the next window
                inbox.Show();
                this.Hide();
                return; */ /*
                //IF FAIL DO: 
                //MessageBox.Show("Wrong user name or password, create a new user name"); 
            } */
        }

        public void Forgot_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Contact the admin by call: +4552223396");    //  can be changed by insert the number and find the code automatecal
        }
        public void Create_Click(object sender, RoutedEventArgs e)
        {
            
            Create create = new Create();               // got to the crate site
            create.Show();
            this.Hide();
        }
    }
    public class LoginAttempt 
    {
        public string UserName, Password;
        public LoginAttempt() {}
        public LoginAttempt(string UN, string Pass)
        {
            UserName = UN;
            Password = Pass;
        }
    }
}
