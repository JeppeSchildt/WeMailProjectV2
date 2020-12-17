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
            MessageBox.Show(dbdir);
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

        static string DecryptPass(string value)     // Decrypt function
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
            
            string InfoList = dbdir+@"\UserName.txt"; // \WeMailProjectV2\Email
            MessageBox.Show("info: \n" + InfoList);
            if (!File.Exists(InfoList)) {
                using (StreamWriter sw = File.CreateText(InfoList)) {
                    sw.Flush();
                    sw.Close();
                }
            }


            using (var sr = new StreamReader(InfoList))  // read the directry of the userid and password
            {
                string Decrypt = DecryptPass(Password.Password);
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
                MessageBox.Show("Wrong user name or password, create a new user name");
            }
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
}
