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
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Net.Mail;
using System.Xml.Serialization;
namespace CLIENT
{
 //CAN ACCESS CurrUser using _login.CurrUser;   
    public partial class Inbox : Window
    {
        private readonly LogIn _login;
        
        public Inbox(LogIn logIn) //
        {
            InitializeComponent();
            _login = logIn;
        }
        public Inbox() //
        {
            InitializeComponent();
        }

        public static List<string> FindSent()               // function to fund the mails under sent folder and store in the list
        {
            string path = LogIn.dbdir + "/Users/" + LogIn.userID+"/sent";
            string[] extensions = { ".txt" };
            
            var files = Directory.EnumerateFiles(path, "*.*", SearchOption.AllDirectories)
                .Where(s => extensions.Any(ext => ext == System.IO.Path.GetExtension(s)));

            List<string> textfile = new List<string>();

            textfile = files.ToList();     // textfile er en list med string
           
            return textfile;
        }

        private void Text()       // function to print the list of mails in sent folder
        {
           // List<string> FillIn = new List<string>();
            var FillIn = FindSent();      // FillIn er også en list med string
           // var numberOfLabels = FillIn.Count;
            var numberOfLabels = 3;   // Only 3 labels now
          

          var FileName = FillIn;
            
            for (int i = 1; i <= numberOfLabels; i++) {
                FileName[i-1] = System.IO.Path.GetFileNameWithoutExtension(FillIn[i-1]);
                var labelName = string.Format("Label{0}", i);
                var label = (Label)this.FindName(labelName);
                label.Content = FileName[i-1];
            }
        }  

        private void SentFolder_Click(object sender, RoutedEventArgs e)
        {
            Text();

           // Write.hey();
           // Write.read();
           // Console.WriteLine(Read.you2());
            
        }

        private void NewEmail_Click(object sender, RoutedEventArgs e)
        {
            SendEmail sendEmail = new SendEmail();            // show the next window
            sendEmail.Show();
            this.Hide();
            return;

        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            var fil = FindSent();

            if (fil.Count != 0) 
            {
                File.Delete(fil[0]);
                MessageBox.Show("Sent Email has been delete");


            }
            else {
                MessageBox.Show("Folder is empty");

            }


        }
        private void Mark_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("not implemented yet you impatient bastard");
            //dummy function for mark
            //send over 5000
            const int PORT_NO = 5000;
            const int PORT_N1 = 5001;
            const string LOCALHOST = "127.0.0.1";
            Console.WriteLine("Beginning Serialisation at Inbox.xaml.xs..\n");
            TcpClient tcpclient = new TcpClient(LOCALHOST, PORT_NO); //SEND ON PORT 5k
            NetworkStream nwStream = tcpclient.GetStream();
            IPAddress localaddress = IPAddress.Parse(LOCALHOST);
            TcpListener STClistener = new TcpListener(localaddress, PORT_N1);
            XmlSerializer xmlSerializer = new XmlSerializer(_login.CurrUser.GetType());
            StringWriter stringified = new StringWriter();
            xmlSerializer.Serialize(stringified, _login.CurrUser); //Stringified is now the CurrentUser class
            char DL = '';
            Email EmailToMark = new Email("inbox","888@wemail.com","888@wemail.com",DateTime.Now.ToString(),"Kolo","ra","Unread");  
            XmlSerializer v2 = new XmlSerializer(EmailToMark.GetType());
            StringWriter EmailStringified = new StringWriter();
            v2.Serialize(EmailStringified, EmailToMark);
            string MarkType = "READ";
            string res = LogIn.userID + DL + "MARK" + DL + stringified.ToString()+DL+EmailStringified+DL+MarkType;
            byte[] bytesToSend = ASCIIEncoding.UTF8.GetBytes(res);
            Console.WriteLine("Sending ur mom : " + res);
            nwStream.Write(bytesToSend, 0, bytesToSend.Length);

            //Listen on 5001
            STClistener.Start();
            TcpClient STCclient = STClistener.AcceptTcpClient();
            NetworkStream nwStreamFromServer = STCclient.GetStream();
            byte[] serverbuffer = new byte[STCclient.ReceiveBufferSize];
            //read
            int bytesfromserver = nwStreamFromServer.Read(serverbuffer, 0, STCclient.ReceiveBufferSize);
            //convert into string
            string datafromserver = @Encoding.ASCII.GetString(serverbuffer, 0, bytesfromserver);

            //Need to deserialize
            ReturnClass RT = new ReturnClass();
            XmlSerializer xmls = new XmlSerializer(RT.GetType());
            StringReader returnclasstostring = new StringReader(datafromserver);
            RT = (ReturnClass)xmls.Deserialize(returnclasstostring);
            Console.WriteLine("\n UserAccountID: " + RT.useracc.UserName);
            Console.WriteLine("\n " + RT.success.ToString());
            _login.CurrUser = RT.useracc;
            Console.WriteLine("this do be good sign");



            
        

            Console.WriteLine("[inbox.xaml.cs] Info from server: " + datafromserver);


        }
    }
}
