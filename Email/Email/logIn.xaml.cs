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
        public UserAccount CurrUser = new UserAccount();
        const int PORT_NO = 5000;
        const int PORT_N1 = 5001;
        const string LOCALHOST = "127.0.0.1";
        static IPAddress localaddress = IPAddress.Parse(LOCALHOST);
        static TcpListener STClistener = new TcpListener(localaddress, PORT_N1);

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
           // MessageBox.Show("Database directory :" + dbdir);
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

      

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (UserID.Text != "" && Password.Password != "") {

                if (e.Key == Key.Enter) {
                    logIn_Click(sender, e);
                }
            }
        }
        private void logIn_Click(object sender, RoutedEventArgs args)
        {
            
            char DL = '';
            string UserName = UserID.Text;
            string PassW = EncryptPass(Password.Password);

            userID = UserName;

            /////////////////////
            /// SERIALISATION ///
            /////////////////////
            Console.WriteLine("Beginning Serialisation...\n");
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
            Console.WriteLine("Yall?\n"+Encoding.ASCII.GetString(bytesToRead, 0, bytesRead));
            tcpclient.Close();
            //
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
            /////////////////////////
            /// DE-SERIALISATION ///
            ///////////////////////
            ReturnClass RT = new ReturnClass();
            XmlSerializer xmls = new XmlSerializer(RT.GetType());
            StringReader returnclasstostring = new StringReader(datafromserver);
            RT = (ReturnClass)xmls.Deserialize(returnclasstostring);
            Console.WriteLine("\n UserAccountID: " + RT.useracc.UserName);
            Console.WriteLine("\n " + RT.success.ToString());

             ///////////////
            ///IMPORTANT///
           ///////////////

            CurrUser = RT.useracc; //IF SUCCESSFULL LOGIN - ADD CURR USER IS GIVEN
             
           ///////////////
          ///IMPORTANT///
         ///////////////
            if (RT.success == true)
            {
                Inbox inbox = new Inbox();            // show the next window
                inbox.Show();
                this.Hide();
            }
            else {
                MessageBox.Show("Error in login");
                Password.Password = "";
            }
            STCclient.Close();
            STClistener.Stop();

            ////////////////////////////
            /// END-OF-SERIALISATION ///
            /////////////////////////// 
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
    public class ReturnClass
    {
        //Could do enum success/fail instead of bool. Not sure if needed though
        public Boolean success;
        public UserAccount useracc = new UserAccount();
        // public Exception ex;
        public string exceptionstring; //Cannot XML serialize exception as it uses IDIRECTORY. DO ex.tostring instead.. 
        public ReturnClass() { }
        public ReturnClass(string excep, bool succ)
        {
            success = succ;
            exceptionstring = excep;
        }
        public ReturnClass(string excep, bool succ, UserAccount user)
        {
            success = succ;
            exceptionstring = excep;
            useracc = user;
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
