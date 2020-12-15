using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net.Mail;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using System.IO;
namespace PInvokeTest
{
<<<<<<< Updated upstream
=======
    

>>>>>>> Stashed changes
    public partial class WEMAIL : Form
    {
        public WEMAIL()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void sendButton_Click(object sender, EventArgs e)
        {  
                string to = reciptxtbox.Text; //Takes user input for recipient
                string from = "Schildt0606@gmail.com"; //Current Sender. Needs changing to be personal to each account

                MailAddress Sender = new MailAddress(from); //Its possible to do MailAdress(from,"displayname"). Could be usefull ?
                MailAddress Recipient = new MailAddress(to);
                MailMessage message = new MailMessage(Sender, Recipient);

                string subject = subjecttxtbox.Text;
                string text = @messagetxtbox.Text;
                string domain = to.Substring(to.LastIndexOf('@') + 1); //Takes everything to the right of @

                message.Subject = subject;
                message.Body = text;
            if (domain.Equals("wemail.com", StringComparison.OrdinalIgnoreCase))
                {
                    SendMail.Wemailtransfer(Sender,Recipient,message);
                }
                else
                {
                    //SendMail.Regular(Sender,Recipient,message);
                }
        }
    }
    public class Email
<<<<<<< Updated upstream
=======
    {
        public string emailType, senderAddress, receiverAddress, timeStamp, subjectMatter, contentText, emailFlag;

        public Email() { }

        public Email(s inputEmailType, s inputSenderAddress, s inputReceiverAddress, s inputTimeStamp, s inputSubjectMatter, s inputContentText, s inputFlag)
        {
            emailType = inputEmailType;
            senderAddress = inputSenderAddress;
            receiverAddress = inputReceiverAddress;
            timeStamp = inputTimeStamp;
            subjectMatter = inputSubjectMatter;
            contentText = inputContentText;
            emailFlag = inputFlag;
        }
    }
    public class SendMail
>>>>>>> Stashed changes
    {
        public string emailType, senderAddress, receiverAddress, timeStamp, subjectMatter, contentText;

        public Email() { } 

        public Email(string inputEmailType, string inputSenderAddress, string inputReceiverAddress, string inputTimeStamp, string inputSubjectMatter, string inputContentText)
        {
            emailType = inputEmailType;
            senderAddress = inputSenderAddress;
            receiverAddress = inputReceiverAddress;
            timeStamp = inputTimeStamp;
            subjectMatter = inputSubjectMatter;
            contentText = inputContentText;
        }
    }
    public class SendMail
    {
        public static void Wemailtransfer(MailAddress Sender,MailAddress Recipient, MailMessage message)
        {
            char DL = '';
            try
            {
                //MessageBox.Show("You're sending to a Wemail account", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                const int PORT_NO = 5000;
                const string LOCALHOST = "127.0.0.1";
                TcpClient tcpclient = new TcpClient(LOCALHOST, PORT_NO);
                NetworkStream nwStream = tcpclient.GetStream();
                //sndr, timestamp, subject, txt
                //var TTS = Sender.Address + DL + DateTime.Today.ToShortDateString() + DL + message.Subject + DL + message.Body + DL;//Improvement is to use stringbuilder append() as it is faster
                Email test = new Email("SENT", Sender.Address, Recipient.Address, DateTime.Today.ToShortDateString(), message.Subject, message.Body);
                XmlSerializer xmlSerializer = new XmlSerializer(test.GetType());
                StringWriter stringified = new StringWriter();
                xmlSerializer.Serialize(stringified, test);
                string res = "SENT" + stringified.ToString();
                byte[] bytesToSend = ASCIIEncoding.UTF8.GetBytes(res);
                Console.WriteLine("Sending : " + message.Body);
                nwStream.Write(bytesToSend, 0, bytesToSend.Length);
                    
                //---read back the text---
                byte[] bytesToRead = new byte[tcpclient.ReceiveBufferSize];
                int bytesRead = nwStream.Read(bytesToRead, 0, tcpclient.ReceiveBufferSize);
                Console.WriteLine("Received : " + Encoding.UTF8.GetString(bytesToRead, 0, bytesRead));
                Console.ReadLine();
                tcpclient.Close();
            }
            catch (Exception ex)
            {
                ExceptionHandler.SendMailException(ex);
            }
        }
    }
    public class ExceptionHandler
    {
        public static void SendMailException(Exception ex)
        {
            MessageBoxButtons buttons = MessageBoxButtons.RetryCancel;
            MessageBox.Show(ex.ToString(), "Error sending mail", buttons, MessageBoxIcon.Warning);
        }
    }
}
