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
public class LoginAttempt
{
    public string UserName, Password;
    public LoginAttempt() { } //Needs empty constructor to deserialize into 
    public LoginAttempt(string UN, string Pass)
    {
        UserName = UN;
        Password = Pass;
    }
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
        const int PORT_N1 = 5001;
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
                TcpListener CTSlistener = new TcpListener(localAdd, PORT_NO);
                Console.WriteLine("Listening...");
                CTSlistener.Start();
                //---incoming client connected---
                TcpClient client = CTSlistener.AcceptTcpClient();
             
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
                Console.WriteLine("sending return");
                byte[] teasting = Encoding.ASCII.GetBytes("Are you receiving this message?");
                nwStream.Write(buffer, 0, teasting.Length);
                
                /*
                Console.WriteLine("\n Sender: " + receivedEmail.senderAddress);
                Console.WriteLine("\n Time: " + receivedEmail.timeStamp);
                Console.WriteLine("\n Subject: " + receivedEmail.subjectMatter);
                Console.WriteLine("\n Content: " + receivedEmail.contentText);
                */
                //requestHandler(USER, REQ, dataReceived);  

                //---request handling---
                switch (REQ) {
                    case "CREATEUSER": //DONE?
                        { 
                        //user deserialization
                        Console.WriteLine("REQ: CREATE USER BIG NICE");
                        UserAccount useraccount = new UserAccount();
                        useraccount = deserializer(useraccount, dataReceived);
                        Console.WriteLine("\n Account:: " + useraccount.UserName);
                        Console.WriteLine("\n Pass:: " + useraccount.PassWord);
                        Console.WriteLine("\n PhoneNumber:: " + useraccount.PhoneNumber);
                        Console.WriteLine("DATABASE DIRECTORY: " + dbdir);
                        StreamWriter sw = new StreamWriter(dbdir + @"\UserName.txt", true);
                        string dir = dbdir + @"\Users\" + useraccount.UserName;
                        if (!(Directory.Exists(dir)))
                        {
                            Directory.CreateDirectory(dir);

                            string inboxPath = dir + "/inbox";
                            string sentPath = dir + "/sent";
                            string draftsPath = dir + "/drafts";

                            Directory.CreateDirectory(inboxPath);
                            Directory.CreateDirectory(sentPath);
                            Directory.CreateDirectory(draftsPath);
                        }
                        sw.WriteLine(useraccount.UserName + "," + useraccount.PassWord + "," + useraccount.PhoneNumber);
                        sw.Flush();
                        sw.Close();
                        break;
                }
                    case "LOGIN":
                        {




                            LoginAttempt Attempt = new LoginAttempt();
                            Attempt = deserializer(Attempt,dataReceived);
                            Console.WriteLine("\n Accountname Attempt: " + Attempt.UserName);
                            Console.WriteLine("\n Password attempt: " + Attempt.Password);
                              
                            string InfoList = dbdir + @"\UserName.txt";
                            if (!File.Exists(InfoList)){ //If UserName.txt does not exist, make it. 
                                using (StreamWriter sw = File.CreateText(InfoList))
                                {
                                    sw.Flush();
                                    sw.Close();
                                }
                            }
                            using (var sr = new StreamReader(InfoList))  //Open UserName.txt
                            {
                                string Decrypt = Attempt.Password; //Encrypts the password to check against the one in storage
                                while (!sr.EndOfStream)
                                {
                                    var line = sr.ReadLine();
                                    string[] words = line.Split(',');
                                    if (String.IsNullOrEmpty(line)) continue;
                                    if (words[0].IndexOf(Attempt.UserName, StringComparison.CurrentCultureIgnoreCase) >= 0 && words[1].IndexOf(Decrypt, StringComparison.CurrentCultureIgnoreCase) >= 0)
                                    // if string  is found, return +1   if not return -1, if empty return 0
                                    {
                                        Console.WriteLine("Successfull login!");
                                        /*
                                        userID = UserID.Text;
                                        Inbox inbox = new Inbox();            // show the next window
                                        inbox.Show();
                                        this.Hide();
                                        */
                                        //return;
                                        //////////////////
                                        ///TCP - START/// 
                                        ////////////////
                                        ///
                                        Console.Write("SENDING TO CLIENT USING TCP");
                                        string texttosend = "success"; //Needs to be the serialized return class
                                        TcpClient STCclient = new TcpClient(SERVER_IP, PORT_N1);
                                        NetworkStream nwStreamSTC = STCclient.GetStream();
                                        byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(texttosend);
                                        //SEND
                                        nwStreamSTC.Write(bytesToSend, 0, bytesToSend.Length);
                                        STCclient.Close();
                                        ////////////////
                                        ///TCP - END /// 
                                        ///////////////




                                    }
                                    else { Console.WriteLine("Error logging in!"); }
                                }
                                
                                //MessageBox.Show("Wrong user name or password, create a new user name");
                            }
                            break;
                        }
                    case "MARK":
                        {
                            
                            //email deserialization
                            break;
                        }
                    case "SEND": //DONE?
                        { //email deserialization - note sure if it sends lmao
                        Email newEmail = new Email();
                        newEmail = deserializer(newEmail, dataReceived);    
                        string domain = newEmail.receiverAddress.Substring(newEmail.receiverAddress.LastIndexOf('@') + 1); //Domain of reciever
                        Console.WriteLine(domain);                        // Den her virke kun for wemail, men mail kan sendes
                        if (domain.Equals("wemail.com", StringComparison.OrdinalIgnoreCase))
                        {
                            //receivedEmail.Send();
                               
                            //.Equals("wemail.com", StringComparison.OrdinalIgnoreCase)) {
                            var reciver = newEmail.receiverAddress;
                            String reciverID = reciver.Substring(0, reciver.IndexOf("@"));
                            

                           //   var receiverPath = Path.Combine(dbdir+@"\Users\", reciverID);
                          //  if (Directory.Exists(reciverID)) {

                                Write.Files2(newEmail);
                              //  Write.read(newEmail);
                         //   }

                          //  else {

                                Write.Files(newEmail);
                                Write.read(newEmail);
                          //  }
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
                        }
                    case "FORWARD":
                        { //email deserialization
                            break;
                        }
                    case "REPLY":
                        { //email deserialization
                            break;
                        }
                    case "UPDATEINBOX":
                        { 
                            //inbox deserialization
                        break;
                        }
                    case "DELETE":
                        { //email deserialization
                        break;
                        }
                    case "DRAFT": 
                        { //email deserialization
                            break;
                        }
                    default:
                        break;
                }
                //---call requestHandler---
                //requestHandler(USER, REQ, dataReceived);
            
                //---something important---
                //Writing back to client

                //buffer = new byte[client.ReceiveBufferSize];
                /*byte[] ReturnBytesToRead = new byte[client.ReceiveBufferSize]; 
                Encoding.ASCII.GetBytes("Are you receiving this message?");
                nwStream.Write(buffer, 0, testing.Length); 
                */
                /*
                string plswork = "HIMYM";
                Console.WriteLine("sending back:" + plswork);
                nwStream.Write(buffer, 0, bytesRead); */
                //IDEA IS:
                // tcp client + listener for client -> server
                // seperate tcp client + listener for server->client
                client.Close();
                CTSlistener.Stop(); 

           }
        }
    }
}