//SERVER / J+A 15/12 14.00

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Net.Mail;
using System.Xml.Serialization;
using System.IO;
using s = System.String;

public class User
{
    public string username, password;
    public List<EmailFolder> userEmailFolders;
}
public class EmailFolder
{
    public string folderType;
    public List<Email> emailList;

    public EmailFolder receiveFolder()
    {
        bool success;
        EmailFolder emailFolderVar = new EmailFolder();
        return emailFolderVar;
        //return success == true; 
    }
}
public class Email
{
    public string emailType, senderAddress, receiverAddress, timeStamp, subjectMatter, contentText,emailFlag;

    public Email() { }
    //remember s = string
    public Email(s inputEmailType, s inputSenderAddress, s inputReceiverAddress, s inputTimeStamp, s inputSubjectMatter, s inputContentText,s inputFlag)
    {
        emailType = inputEmailType;
        senderAddress = inputSenderAddress;
        receiverAddress = inputReceiverAddress;
        timeStamp = inputTimeStamp;
        subjectMatter = inputSubjectMatter;
        contentText = inputContentText;
        emailFlag = inputFlag;
    }

    public bool deleteEmail()
    {

        bool success = true;
        return success == true;
    }

    public bool sendEmail(string sender)
    {
        //Her skal den email vi ønsker at sende gemmes i serveren hos sender OG receiver
        bool success;
        if (sender == this.senderAddress)
            success = true;
        else
            success = false;

        Console.WriteLine("\n Sender: " + this.senderAddress);
        Console.WriteLine("\n Time: " + this.timeStamp);
        Console.WriteLine("\n Subject: " + this.subjectMatter);
        Console.WriteLine("\n Content: " + this.contentText);

        return success;
    }

    public bool forwardEmail()
    {
        bool success = true;
        return success == true;
    }

    public bool replyToEmail()
    {
        bool success = true;
        return success == true;
    }
    public bool flagEmail()
    {
        bool success = true;
        return success == true;
    }

    public void Send(MailMessage message)
    {
        try
        {
            string server = "mail.smtp2go.com";
            SmtpClient client = new SmtpClient(server);
            client.Send(message);
            Console.WriteLine("Success");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Big ass exception:" + ex.ToString());
            //Should return exception to client side, to exception handle using error prompts
            //ExceptionHandler.SendMailException(ex); 
        }
    }
    public void Send() //Sends mail using SMTP
    {
        MailAddress Recipient = new MailAddress(this.senderAddress);
        MailAddress Sender = new MailAddress(this.receiverAddress);
        MailMessage message = new MailMessage(Sender, Recipient);

        string subject = this.subjectMatter;
        string text = this.contentText;
        // string domain = to.Substring(to.LastIndexOf('@') + 1); //Takes everything to the right of @
        message.Subject = subject;
        message.Body = text;
        Send(message);
        //          #pragma warning disable CS0219 // The variable 'DL' is assigned but its value is never used
        //      char DL = '';
        //          #pragma warning restore CS0219 // The variable 'DL' is assigned but its value is never used
        /*
        //MessageBox.Show("You're sending to a Wemail account", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        const int PORT_NO = 5000;
        const string LOCALHOST = "127.0.0.1";
        TcpClient tcpclient = new TcpClient(LOCALHOST, PORT_NO);
        NetworkStream nwStream = tcpclient.GetStream();
        //sndr, timestamp, subject, txt
        var TTS = Sender.Address + DL + DateTime.Today.ToShortDateString() + DL + message.Subject + DL + message.Body + DL;//Improvement is to use stringbuilder append() as it is faster
        byte[] bytesToSend = ASCIIEncoding.UTF8.GetBytes(TTS);
        Console.WriteLine("Sending : " + message.Body);
        nwStream.Write(bytesToSend, 0, bytesToSend.Length);

        //---read back the text---
        byte[] bytesToRead = new byte[tcpclient.ReceiveBufferSize];
        int bytesRead = nwStream.Read(bytesToRead, 0, tcpclient.ReceiveBufferSize);
        Console.WriteLine("Received : " + Encoding.UTF8.GetString(bytesToRead, 0, bytesRead));
        Console.ReadLine();
        tcpclient.Close(); */

    }
    public void Forward(MailAddress Recipient)
    {
        MailAddress Sender = new MailAddress(this.receiverAddress);
        MailMessage newmessage = new MailMessage(Sender, Recipient);
        newmessage.Subject = "Fwd: " + this.subjectMatter;
        newmessage.Body = "Forwarded: " + this.contentText;
        Send(newmessage);
    }
    public void Reply(string text)
    {
        MailAddress Sender = new MailAddress(this.receiverAddress);
        MailAddress Recipient = new MailAddress(this.senderAddress);
        MailMessage newmessage = new MailMessage(Sender, Recipient);
        newmessage.Subject = "Re: " + this.subjectMatter;
        newmessage.Body = text + "------" + this.contentText;
    }
    public void Wemailtransfer()
    {
        MailAddress Recipient = new MailAddress(this.senderAddress); //Its possible to do MailAdress(from,"displayname"). Could be usefull ?
        MailAddress Sender = new MailAddress(this.receiverAddress);
        MailMessage message = new MailMessage(Sender, Recipient);

        string subject = this.subjectMatter;
        string text = this.contentText;
       // string domain = to.Substring(to.LastIndexOf('@') + 1); //Takes everything to the right of @

        message.Subject = subject;
        message.Body = text;

#pragma warning disable CS0219 // The variable 'DL' is assigned but its value is never used
        char DL = '';
#pragma warning restore CS0219 // The variable 'DL' is assigned but its value is never used
        try
        {
            string server = "mail.smtp2go.com";
            SmtpClient client = new SmtpClient(server);
            client.Send(message);

            /*
            //MessageBox.Show("You're sending to a Wemail account", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            const int PORT_NO = 5000;
            const string LOCALHOST = "127.0.0.1";
            TcpClient tcpclient = new TcpClient(LOCALHOST, PORT_NO);
            NetworkStream nwStream = tcpclient.GetStream();
            //sndr, timestamp, subject, txt
            var TTS = Sender.Address + DL + DateTime.Today.ToShortDateString() + DL + message.Subject + DL + message.Body + DL;//Improvement is to use stringbuilder append() as it is faster
            byte[] bytesToSend = ASCIIEncoding.UTF8.GetBytes(TTS);
            Console.WriteLine("Sending : " + message.Body);
            nwStream.Write(bytesToSend, 0, bytesToSend.Length);

            //---read back the text---
            byte[] bytesToRead = new byte[tcpclient.ReceiveBufferSize];
            int bytesRead = nwStream.Read(bytesToRead, 0, tcpclient.ReceiveBufferSize);
            Console.WriteLine("Received : " + Encoding.UTF8.GetString(bytesToRead, 0, bytesRead));
            Console.ReadLine();
            tcpclient.Close(); */
        }
        catch (Exception ex)
        {
            Console.WriteLine("Big ass exception:" + ex.ToString());
            //Should return exception to client side, to exception handle using error prompts
            //ExceptionHandler.SendMailException(ex); 
        }
    }
}

namespace Server
{
    class Program
    {

        const int PORT_NO = 5000;
        const string SERVER_IP = "127.0.0.1"; //Run over localhost
        /* Core driver of server */
        /* Consider applying the deserialization in here */

        public static T deserializer<T>(T desObj, string data)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(desObj.GetType());
            StringReader stringified = new StringReader(data);
            desObj = (T)xmlSerializer.Deserialize(stringified);
            return desObj;
        }

        public static string serializer<T>(T sObj)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(sObj.GetType());
            StringWriter stringified = new StringWriter();
            xmlSerializer.Serialize(stringified, sObj);
            string objSerialized = stringified.ToString();
            return objSerialized;
        }
        static void Main(string[] args)
        {
            while (true)
            {
                //---listen at the specified IP and port no.---
                IPAddress localAdd = IPAddress.Parse(SERVER_IP);
                TcpListener listener = new TcpListener(localAdd, PORT_NO);
                Console.WriteLine("Listening...");
                listener.Start();
                //---incoming client connected---
                TcpClient client = listener.AcceptTcpClient();

                //---get the incoming data through a network stream---
                NetworkStream nwStream = client.GetStream();
                byte[] buffer = new byte[client.ReceiveBufferSize];

                //---read incoming stream---
                int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize);
                string dataReceived = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                //---convert the data received into a string---
                string USER = dataReceived.Substring(0, dataReceived.IndexOf(''));
                dataReceived = dataReceived.Substring(dataReceived.IndexOf('') + 1);
                string REQ = dataReceived.Substring(0, dataReceived.IndexOf(''));
                dataReceived = dataReceived.Substring(dataReceived.IndexOf('') + 1);

                Email receivedEmail = new Email();
                XmlSerializer xmlSerializer = new XmlSerializer(receivedEmail.GetType());
                StringReader stringified = new StringReader(dataReceived);
                receivedEmail = (Email)xmlSerializer.Deserialize(stringified);


                //requestHandler(USER, REQ, dataReceived);

                //---request handling---
                switch (REQ)
                {
                    case "CREATEUSER": //user deserialization
                        break;
                    case "LOGIN": //user deserialization
                        break;
                    case "MARK": //email deserialization
                        break;
                    case "SEND": //email deserialization
                        Email newEmail = new Email();
                        newEmail = deserializer(newEmail, dataReceived);
                        newEmail.sendEmail(USER);
                        break;
                    case "FORWARD": //email deserialization
                        break;
                    case "REPLY": //email deserialization
                        break;
                    case "UPDATEINBOX": //inbox deserialization
                        break;
                    case "DELETE": //email deserialization
                        break;
                    case "DRAFT": //email deserialization
                        break;
                    default:
                        break;
                }
                //---call requestHandler---
                //requestHandler(USER, REQ, dataReceived);

                //---something important---
                nwStream.Write(buffer, 0, bytesRead);
                client.Close();
                listener.Stop();
            }
        }
    }
}   