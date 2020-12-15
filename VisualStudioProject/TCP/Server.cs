using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Net.Mail;
using System.Xml.Serialization;
using System.IO;

public class Email
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
    } //MailAddress Sender, MailAddress Recipient, MailMessage message
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
        
        static void Main(string[] args)
        {
            
            int i = 0;
            while (i < 10)
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

                //---convert the data received into a string---
                string dataReceived = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                string FUNC = dataReceived.Substring(0, dataReceived.IndexOf(''));
                Console.WriteLine("test:"+FUNC);
                dataReceived = dataReceived.Substring(dataReceived.IndexOf('') + 1);
                Email receivedEmail = new Email();
                XmlSerializer xmlSerializer = new XmlSerializer(receivedEmail.GetType());
                StringReader stringified = new StringReader(dataReceived);
                receivedEmail = (Email)xmlSerializer.Deserialize(stringified); 
                
                string Sender = receivedEmail.senderAddress;
                string timeofday = receivedEmail.timeStamp;
                string subject = receivedEmail.subjectMatter;
                string msg = receivedEmail.contentText;
                
                //receivedEmail.Wemailtransfer();
                Console.WriteLine("Received : " + dataReceived);

                //---write back the text to the client---
                // Console.WriteLine("Sending back : " + dataReceived);
                Console.WriteLine("\nSender: "+ Sender);
                Console.WriteLine("\nTimestamp: " + timeofday);
                Console.WriteLine("\nSubject line: " + subject);
                Console.WriteLine("\nMessage: " + msg);
                Console.WriteLine("\nFUNC: " + FUNC); 


                nwStream.Write(buffer, 0, bytesRead);
                client.Close();
                listener.Stop();
                Console.ReadLine();
                i++;
            }
        }
    }
}   