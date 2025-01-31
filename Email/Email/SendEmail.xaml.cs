﻿using System;
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
        private readonly Inbox _inbox;
        UserAccount CurrUser = new UserAccount();
        public SendEmail(Inbox inbox)
        {
            InitializeComponent();
            _inbox = inbox;
            CurrUser=_inbox.CurrUser;
            
        }
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
            //MessageBox.Show("yallah:"+CurrUser.UserName);
            string to = Emails.Text; //Takes user input for recipient
            string from = CurrUser.UserName+"@wemail.com"; //Current Sender. Needs changing to be personal to each account
            MailAddress Sender = new MailAddress(from); //Its possible to do MailAdress(from,"displayname"). Could be usefull ?
            MailAddress Recipient = new MailAddress(to);
            MailMessage message = new MailMessage(Sender, Recipient);

            string subject = Subject.Text;
            string text = @Message.Text;
            string domain = to.Substring(to.LastIndexOf('@') + 1); //Takes everything to the right of @

            message.Subject = subject;
            message.Body = text;
            if ( SendMail.Wemailtransfer(Sender, Recipient, message) == true) 
            {
                Inbox inbox = new Inbox();            // show the next window
                inbox.Show();
                this.Close();
            }
           
        }

        public void draft_Click(object sender, RoutedEventArgs e) //USER CLICKS SEND BUTTON
        {
         
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
        public static string server = "mail.smtp2go.com"; //Current SMTP server. Coupled to IP   
        /*
        public static void Regular(MailAddress Sender, MailAddress Recipient, MailMessage message)
        {
            SmtpClient client = new SmtpClient(server);
            client.Send(message);
           // MessageBox.Show("Email Sent!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        } */

        public static bool Wemailtransfer(MailAddress Sender, MailAddress Recipient, MailMessage message)
        {
            char DL = ''; //Possibly in need of changing, is not supported in TXT files. 
            try {
                const int PORT_NO = 5000;
                const string LOCALHOST = "127.0.0.1";
                TcpClient tcpclient = new TcpClient(LOCALHOST, PORT_NO);
                NetworkStream nwStream = tcpclient.GetStream();
                string flag = "Unread";
                Email test = new Email("NON", Sender.Address, Recipient.Address, DateTime.Today.ToShortDateString(), message.Subject, message.Body, flag);
                XmlSerializer xmlSerializer = new XmlSerializer(test.GetType());
                StringWriter stringified = new StringWriter();
                xmlSerializer.Serialize(stringified, test);
                string res = LogIn.userID + DL + "SEND" + DL + stringified.ToString();
                byte[] bytesToSend = ASCIIEncoding.UTF8.GetBytes(res);


                Console.WriteLine("Sending : " + message.Body);
                nwStream.Write(bytesToSend, 0, bytesToSend.Length);


                //---read back the text---
                byte[] bytesToRead = new byte[tcpclient.ReceiveBufferSize];
                int bytesRead = nwStream.Read(bytesToRead, 0, tcpclient.ReceiveBufferSize);
                //string returnsignal = Encoding.UTF8.GetString(bytesToRead, 0, bytesRead);

                //gets return signal but needs return class aswell.. check serialization


                ////////////////////////////
                /// STC - SERIALISATION ///
                //////////////////////////
                const int PORT_N1 = 5001;
                IPAddress localaddress = IPAddress.Parse(LOCALHOST);
                TcpListener STClistener = new TcpListener(localaddress, PORT_N1);

                Console.WriteLine("Listening to server on sendemail.Xaml.Cs:");
                STClistener.Start();
                TcpClient STCclient = STClistener.AcceptTcpClient();
                NetworkStream nwStreamFromServer = STCclient.GetStream();
                byte[] serverbuffer = new byte[STCclient.ReceiveBufferSize];
                //read
                int bytesfromserver = nwStreamFromServer.Read(serverbuffer, 0, STCclient.ReceiveBufferSize);
                //convert into string
                string datafromserver = Encoding.ASCII.GetString(serverbuffer, 0, bytesfromserver);
                Console.WriteLine("Info from server: " + datafromserver);
                ReturnClass RT = new ReturnClass();
                XmlSerializer xmls = new XmlSerializer(RT.GetType());
                StringReader returnclasstostring = new StringReader(datafromserver);
                RT = (ReturnClass)xmls.Deserialize(returnclasstostring);
                if(RT.success)
                {
                    MessageBox.Show("Email has been sent!!");

                    STCclient.Close();
                    STClistener.Stop();
                    tcpclient.Close();
                    return true;
                }
                else
                {
                  //  MessageBox.Show("no receipient found");
                      MessageBox.Show("ERROR IN SENDING: "+ RT.exceptionstring);
                    STCclient.Close();
                    STClistener.Stop();
                    tcpclient.Close();
                    return false;
                }
            }
            catch (Exception ex) {
                ExceptionHandler.SendMailException(ex);
                //Should check if TCP shit is open n close if they are
                return false;
            }
        }
        public static bool Forward(MailAddress Sender, MailAddress Recipient, MailMessage message,string newrecipient) //Could change to recieve email class instead
        {
            char DL = '';
            const int PORT_NO = 5000;
            const string LOCALHOST = "127.0.0.1";
            TcpClient tcpclient = new TcpClient(LOCALHOST, PORT_NO);
            NetworkStream nwStream = tcpclient.GetStream();
            string flag = "FWD";
            Email test = new Email("NON", Sender.Address, Recipient.Address, DateTime.Today.ToShortDateString(), message.Subject, message.Body, flag);
            XmlSerializer xmlSerializer = new XmlSerializer(test.GetType());
            StringWriter stringified = new StringWriter();
            xmlSerializer.Serialize(stringified, test);
            string res = LogIn.userID + DL + "FWD" + DL + stringified.ToString()+DL+newrecipient;
            byte[] bytesToSend = ASCIIEncoding.UTF8.GetBytes(res);
            Console.WriteLine("Sending : " + message.Body);
            nwStream.Write(bytesToSend, 0, bytesToSend.Length);
            return true;
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


