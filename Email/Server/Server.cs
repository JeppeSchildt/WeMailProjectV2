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
public class EmailFolder
{
    public string folderType;
    public List<Email> emailList;

    public EmailFolder receiveFolder()
    {
        // bool success; //currently not used
        EmailFolder emailFolderVar = new EmailFolder();
        return emailFolderVar;
        //return success == true; 
    }
}
public class ReturnClass
{
    public ReturnClass() {}
    public ReturnClass(Exception ex, bool success) {}
}
public class Email
{
    public string emailType, senderAddress, receiverAddress, timeStamp, subjectMatter, contentText, emailFlag;
    
    public Email() { }
    //remember s = string
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

    public bool deleteEmail()
    {
        bool success = true;
        return success == true;
    }
    public void Send(MailMessage message)
    {
        try {
            string server = "mail.smtp2go.com";
            SmtpClient client = new SmtpClient(server);
            Console.WriteLine("Recipient yallah::"+message.To);
            client.Send(message);
            Console.WriteLine("Success");
        }
        catch (Exception ex) {
            Console.WriteLine("Big ass exception:" + ex.ToString());
            //Should return exception to client side, to exception handle using error prompts
            //ExceptionHandler.SendMailException(ex); 
        }
    }
    public void Send() //Sends mail using SMTP
    {
        MailAddress Recipient = new MailAddress(this.receiverAddress);
        MailAddress Sender = new MailAddress(this.senderAddress);
        MailMessage message = new MailMessage(Sender, Recipient);

        string subject = this.subjectMatter;
        string text = this.contentText;
        // string domain = to.Substring(to.LastIndexOf('@') + 1); //Takes everything to the right of @
        message.Subject = subject;
        message.Body = text;
        Send(message);
    }
    public void Forward(MailAddress Recipient) //Not sure about recipient vs sender
    {

        MailAddress Sender = new MailAddress(this.senderAddress);
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
}

namespace Server
{

    class Server
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
            string dbdir = Write.dbdir;
            while (true) {
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
                string USER = dataReceived.Substring(0, dataReceived.IndexOf('')); //USER REQUESTING
                dataReceived = dataReceived.Substring(dataReceived.IndexOf('') + 1);
                string REQ = dataReceived.Substring(0, dataReceived.IndexOf('')); //REQUEST 
                dataReceived = dataReceived.Substring(dataReceived.IndexOf('') + 1);

                //Email receivedEmail = new Email();
                UserAccount useraccount = new UserAccount();
                //XmlSerializer xmlSerializer = new XmlSerializer(receivedEmail.GetType());
                XmlSerializer xmlSerializer = new XmlSerializer(useraccount.GetType());

                StringReader stringified = new StringReader(dataReceived);
                //receivedEmail = (Email)xmlSerializer.Deserialize(stringified);
                useraccount = (UserAccount)xmlSerializer.Deserialize(stringified);
                Console.WriteLine("sending return");
                byte[] teasting = Encoding.ASCII.GetBytes("Are you receiving this message?");
                nwStream.Write(buffer, 0, teasting.Length);

                /*
                Console.WriteLine("\n Sender: " + receivedEmail.senderAddress);
                Console.WriteLine("\n Time: " + receivedEmail.timeStamp);
                Console.WriteLine("\n Subject: " + receivedEmail.subjectMatter);
                Console.WriteLine("\n Content: " + receivedEmail.contentText);
                */
                Console.WriteLine("\n Account:: " + useraccount.UserName);
                Console.WriteLine("\n Pass:: " + useraccount.PassWord);
                Console.WriteLine("\n PhoneNumber:: " + useraccount.PhoneNumber);
               // Console.WriteLine("\n Content: " + receivedEmail.contentText);
                //requestHandler(USER, REQ, dataReceived);

                //---request handling---
                switch (REQ) {
                    case "CREATEUSER": //user deserialization
                        Console.WriteLine("REQ: CREATE USER BIG NICE");



                        //Client needs to:
                        // Authenticate requirements regarding legality
                        // Encrypt Password
                        //
                        //Server needs to: 
                        // Do the folder shit
                        // Add user to username txt
                        break;
                    case "LOGIN": //user deserialization
                        break;
                    case "MARK": //email deserialization
                        break;
                    case "SEND": //email deserialization
                        Email newEmail = new Email();
                        newEmail = deserializer(newEmail, dataReceived);
                        
                        string domain = newEmail.receiverAddress.Substring(newEmail.receiverAddress.LastIndexOf('@') + 1); //Domain of reciever
                        Console.WriteLine(domain);                        // Den her virke kun for wemail, men mail kan sendes
                        if (domain.Equals("wemail.com", StringComparison.OrdinalIgnoreCase))
                        {
                            var reciver = newEmail.receiverAddress;
                            String reciverID = reciver.Substring(0, reciver.IndexOf("@"));

                            var receiverPath = Path.Combine(dbdir+@"\Users\", reciverID); 
                            if (Directory.Exists(reciverID))
                            {
                                Write.Files2(newEmail);
                                Write.read(newEmail);
                            }

                            Write.Files(newEmail);
                            Write.read(newEmail);
                        }
                        else
                        {
                            //receivedEmail.Send();
                            //store in senders sent
                            Write.Files(newEmail);
                            Write.read(newEmail);
                        }

                        //newEmail.sendEmail(USER);
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
                //Writing back to client
                byte[] testing = Encoding.ASCII.GetBytes("Are you receiving this message?");
                nwStream.Write(buffer, 0, testing.Length);
                client.Close();
                listener.Stop();
           }
        }
    }
}