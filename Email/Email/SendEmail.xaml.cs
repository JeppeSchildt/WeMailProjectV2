﻿//Client

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
using System.Net;
using System.Net.Sockets;
using System.Net.Mail;
using System.Xml.Serialization;
using System.IO;
using s = System.String;

namespace CLIENT
{
    /// <summary>
    /// Interaction logic for SendeMail.xaml
    /// </summary>
    public partial class SendEmail : Window
    {
        public SendEmail()
        {
            InitializeComponent();
        }
        public void Recip_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Emails.Text == "RecipientEmails")
                Emails.Text = "";
        }

        public void Subject_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Subject.Text == "Subject")
                Subject.Text = "";
        }
        public void Message_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Message.Text == "Write here")
                Message.Text = "";
        }

        public void Send_Click(object sender, RoutedEventArgs e) //USER CLICKS SEND BUTTON
        {

            string to = Emails.Text; //Takes user input for recipient
            string from = LogIn.userID + "@wemail.com"; //Sender address specific to user

            MailAddress Sender = new MailAddress(from); //Its possible to do MailAdress(from,"displayname"). Could be usefull ?
            MailAddress Recipient = new MailAddress(to);
            MailMessage message = new MailMessage(Sender, Recipient);

            string subject = Subject.Text;
            string text = @Message.Text;
            message.Subject = subject;
            message.Body = text;        
            bool suc = SendMail.MailCall(Sender, Recipient, message); //serializes information and ships to client
            if (suc == true)
            {
                MessageBox.Show("Email has been sent!!");
                Inbox inbox = new Inbox();            // show the next window
                inbox.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Unknown Error :)))))");
            }

        }
    }

    public class Email
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
    {
        public static bool MailCall(MailAddress Sender, MailAddress Recipient, MailMessage message)
        {
            char DL = ''; //Possibly in need of changing, is not supported in TXT files. 
            try {
                const int PORT_NO = 5000;
                const string LOCALHOST = "127.0.0.1";
                TcpClient tcpclient = new TcpClient(LOCALHOST, PORT_NO);
                NetworkStream nwStream = tcpclient.GetStream();
                Email test = new Email("NON", Sender.Address, Recipient.Address, DateTime.Today.ToShortDateString(), message.Subject, message.Body, "NON");
                XmlSerializer xmlSerializer = new XmlSerializer(test.GetType());
                StringWriter stringified = new StringWriter();
                xmlSerializer.Serialize(stringified, test);
                string res = "ALEX" + DL + "SEND" + DL + stringified.ToString();
                byte[] bytesToSend = ASCIIEncoding.UTF8.GetBytes(res);

                Console.WriteLine("Sending : " + message.Body);
                nwStream.Write(bytesToSend, 0, bytesToSend.Length);
                //---read back the text---
                byte[] bytesToRead = new byte[tcpclient.ReceiveBufferSize];
                int bytesRead = nwStream.Read(bytesToRead, 0, tcpclient.ReceiveBufferSize);
                string returnsignal = Encoding.UTF8.GetString(bytesToRead, 0, bytesRead);
                Console.WriteLine("Succes : " + returnsignal); //Recieves true if success, false if error. Should be changed to a exception if error later. 
                tcpclient.Close();
                return bool.Parse(returnsignal);
            }
            catch (Exception ex) {
                ExceptionHandler.SendMailException(ex);
                return false;
            }
        }
    }
    public class ExceptionHandler
    {
        public static void SendMailException(Exception ex)
        {
            //  MessageBoxButtons buttons = MessageBoxButtons.RetryCancel;
            MessageBox.Show(ex.ToString(), "Error sending mail");//, buttons, MessageBoxIcon.Warning);
        }
    }
}


