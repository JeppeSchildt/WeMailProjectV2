using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Net.Mail;
using System.Xml.Serialization;
using System.IO;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;

using s = System.String;

public enum Folder {
    inbox,
    sent,
    drafts
}
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
    //public List<EmailFolder> userEmailFolders;
    public List<Email> sentFolder,draftFolder,inboxFolder;
    public UserAccount() { }
    public UserAccount(string UN, string Pass, string TLF)
    {
        UserName = UN;
        PassWord = Pass;
        PhoneNumber = TLF;
    }
    public class ReturnClass
    {
        //Could do enum success/fail instead of bool. Not sure if needed though
        public Boolean success;
        // public Exception ex;
        public string exceptionstring; //Cannot XML serialize exception as it uses IDIRECTORY. DO ex.tostring instead.. 
        public ReturnClass() { }
        public ReturnClass(string excep, bool succ)
        {
            success = succ;
            exceptionstring = excep;
        }
    }
}

public class EmailFolder
{
    public string folderType;
    public List<Email> emailList;

    public EmailFolder receiveFolder()
    {
        EmailFolder emailFolderVar = new EmailFolder();
        return emailFolderVar;
        //return success == true; 
    }
}

public class ReturnClass
{
    //Could do enum success/fail instead of bool. Not sure if needed though
    public Boolean success;
    public UserAccount useracc = new UserAccount();
    // public Exception ex;
    public string exceptionstring; //Cannot XML serialize exception as it uses IDIRECTORY. DO ex.tostring instead.. 
    public ReturnClass() {}
    public ReturnClass(string excep, bool succ)
    {
        success = succ;
        exceptionstring = excep;
    }
    public ReturnClass(string excep, bool succ,UserAccount user) {
        success = succ;
        exceptionstring = excep;
        useracc = user;
    }
    public void Successfull()
    {
        this.success = true;
        this.exceptionstring = "";
    }
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
        //UserAccount.ReturnClass test = new UserAccount.ReturnClass();
       
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
        Console.WriteLine("Mail successfully forwarded to:"+Recipient.Address);
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
                Console.WriteLine("\nListening...");
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
                //start STC client //
                TcpClient STCclient = new TcpClient(SERVER_IP, PORT_N1);
                NetworkStream nwStreamSTC = STCclient.GetStream();
                UserAccount CurrentUser = new UserAccount();
                //---request handling---
                switch (REQ) {
                    case "CREATEUSER": //DONE?
                        {
                            try
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
                                if (!(Directory.Exists(dir))) {
                                    Directory.CreateDirectory(dir);
                                    string inboxPath = dir + "/inbox";
                                    string sentPath = dir + "/sent";
                                    string draftsPath = dir + "/drafts";
                                    Directory.CreateDirectory(inboxPath);
                                    Directory.CreateDirectory(sentPath);
                                    Directory.CreateDirectory(draftsPath);

                                    sw.WriteLine(useraccount.UserName + "," + useraccount.PassWord + "," + useraccount.PhoneNumber);
                                    sw.Flush();
                                    sw.Close();
                                    ReturnClass rtrn = new ReturnClass();
                                    rtrn.success = true;
                                    string returnclassstring = serializer(rtrn);
                                    byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(returnclassstring);
                                    //SEND
                                    nwStreamSTC.Write(bytesToSend, 0, bytesToSend.Length);
                                    //could close STC
                                    STCclient.Close(); //2012.1929
                                }
                                else 
                                {
                                    ReturnClass rtrn = new ReturnClass();
                                    rtrn.success = false;
                                    rtrn.exceptionstring = "Username is taken";

                                    string returnclassstring = serializer(rtrn);
                                    byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(returnclassstring);
                                    //SEND
                                    nwStreamSTC.Write(bytesToSend, 0, bytesToSend.Length);
                                }
                            }
                            catch (Exception ex)
                            {
                                ReturnClass rtrn = new ReturnClass(ex.ToString(), false);
                                string returnclassstring = serializer(rtrn);
                                byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(returnclassstring);
                                //SEND
                                nwStreamSTC.Write(bytesToSend, 0, bytesToSend.Length);
                                //could close STC
                            }
                        break;
                }
                    case "LOGIN": // DONE ? - NEEDS TO RETURN USERCLASS
                        {
                            Boolean found = false;
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
                                        CurrentUser.UserName = Attempt.UserName; //Needs to actually return user class...
                                        Console.WriteLine("Assigning password:" + words[1]);
                                        CurrentUser.PassWord = words[1];
                                        Console.WriteLine("Assigning phonenumber:" + words[2]);
                                        CurrentUser.PassWord = words[2];
                                        //Console.WriteLine("Updating sentFolder");
                                        CurrentUser = Write.UpdateList(CurrentUser,Folder.sent);
                                        //Console.WriteLine("Updating inboxFolder");
                                        CurrentUser=Write.UpdateList(CurrentUser, Folder.inbox);
                                        //Console.WriteLine("Updating draftFolder");
                                        CurrentUser=Write.UpdateList(CurrentUser,Folder.drafts);
                                        found = true;
                                    }
                                    else { //return class of faults
                                        Console.WriteLine("Error logging in!");
                                      }
                                }
                                if (found)
                                {
                                    //////////////////
                                    ///TCP - START/// 
                                    ////////////////
                                    ///
                                    Console.Write("SENDING TO CLIENT USING TCP");
                                    ReturnClass returntest = new ReturnClass();
                                    returntest.useracc = CurrentUser;
                                    returntest.success = true;
                                    returntest.exceptionstring = "";
                                    string returnclassstring = serializer(returntest);
                                    byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(returnclassstring);
                                    //SEND
                                    nwStreamSTC.Write(bytesToSend, 0, bytesToSend.Length);
                                    Console.WriteLine("CurrentUserNameIs:"+CurrentUser.UserName);
                                    STCclient.Close();
                                    ////////////////
                                    ///TCP - END /// 
                                    ///////////////
                                }
                                else
                                {
                                    Console.Write("SENDING ERROR TO CLIENT USING TCP : L323");
                                    UserAccount usrtest = new UserAccount();
                                    usrtest.UserName = "Doughnut";
                                    ReturnClass returntest = new ReturnClass("user not found",false,usrtest);
                                    Console.Write("KOLORA:" + returntest.success.ToString());
                                    /*
                                    returntest.useracc = usrtest;
                                    returntest.success = false;
                                    returntest.exceptionstring = "User is not found";
                                    */
                                    string returnclassstring = serializer(returntest);
                                    byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(returnclassstring);
                                    //SEND
                                    nwStreamSTC.Write(bytesToSend, 0, bytesToSend.Length);
                                    Console.WriteLine("Error message returned from server: ");
                                    STCclient.Close();
                                    /////////////////////////
                                }
                            }
                            break;
                        }
                    case "MARK": //UNREAD - READ - IMPORTANT :: Should be done, returning an updated useraccount
                        {

                            //recieves: User class - Email Class - Mark
                            char DL = '';
                            Console.WriteLine("Mark:"+dataReceived );
                            string Userclassstring = dataReceived.Substring(0,dataReceived.IndexOf(DL));
                            string tempdata = dataReceived.Substring(dataReceived.IndexOf(DL)+1); //From right after dl to end
                            string EmailClassString = tempdata.Substring(0, tempdata.IndexOf(DL));
                            string MarkType = tempdata.Substring(tempdata.IndexOf(DL)+1);
                            //Deserialize UserClass:
                            UserAccount UserA = new UserAccount();
                            UserA = deserializer(UserA, Userclassstring);
                            //Deserialize EmailClass
                            Email EmailToMark = new Email();
                            EmailToMark = deserializer(EmailToMark,EmailClassString);
                            string folder = EmailToMark.emailType;
                            Console.WriteLine("\nUserClass:" + Userclassstring);
                            Console.WriteLine("\nEmailClass:" + EmailClassString);
                            Console.WriteLine("\nMarkType:"+MarkType);
                            Console.WriteLine("Editing file..");

                            ReturnClass returnClass = new ReturnClass();
                            try
                            {
                                UserA=Write.edit(UserA,EmailToMark, 5, MarkType); //Updates Flag on the email.
                                Console.Write("User was updated?");
                                returnClass.success = true;
                            }
                            catch(Exception ex) {
                                returnClass.exceptionstring = ex.ToString();
                                returnClass.success = false;
                            }
                            returnClass.useracc = UserA;
                            string returnclassstring = serializer(returnClass);
                            byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(returnclassstring);
                            //SEND
                            nwStreamSTC.Write(bytesToSend, 0, bytesToSend.Length);
                            Console.WriteLine("Information was sent from Server regarding Mark");
                            STCclient.Close();
                            break;
                        }
                    case "SEND":  //DONE ? Needs to return a useraccount with updated sent folder
                        { 
                        Email newEmail = new Email();
                        newEmail = deserializer(newEmail, dataReceived);    
                        string domain = newEmail.receiverAddress.Substring(newEmail.receiverAddress.LastIndexOf('@') + 1); //Domain of reciever
                        Console.WriteLine(domain);
                        ReturnClass rtrn = new ReturnClass();
                        Console.WriteLine("\n\nIN SEND: USERNAME IS:"+CurrentUser.UserName+"\n\n");
                        Console.WriteLine("\n\nIN SEND: USERID IS:" + USER + "\n\n");
                        CurrentUser.UserName = USER; //Needs changing temp fix?
                            if (domain.Equals("wemail.com", StringComparison.OrdinalIgnoreCase))
                        {
                            try 
                                {  
                                    var reciver = newEmail.receiverAddress;
                                    String reciverID = reciver.Substring(0, reciver.IndexOf("@"));
                                    var receiverPath = Path.Combine(dbdir+@"\Users\", reciverID);
                                    if (Directory.Exists(receiverPath)) {
                                        Write.Files2(newEmail);
                                        Write.Files(newEmail);
                                        rtrn.success = true;
                                    }
                                    else {
                                        rtrn.success = false;
                                        rtrn.exceptionstring = "no receipient found";
                                    }
                                }
                          catch(Exception ex)
                                {
                                    rtrn.success = false;
                                    rtrn.exceptionstring = ex.ToString();
                                }
                        }
                        else
                        {
                            try
                            {
                                    newEmail.Send(); 
                                    Write.Files(newEmail);
                                    rtrn.success = true;
                            }
                                catch (Exception ex)
                            {
                                    rtrn.success = false;
                                    rtrn.exceptionstring = ex.ToString();
                            }
                        }
                            if (rtrn.success == true)
                            {
                                //Needs to update inboxes of user account. Draft is not nessecary as it doesnt use this function
                                rtrn.useracc=CurrentUser; //Was other way around b efore

                                CurrentUser = Write.UpdateList(CurrentUser,Folder.sent); //responds to drafts, inbox, sent. Pretty self explanatory
                                CurrentUser = Write.UpdateList(CurrentUser, Folder.inbox);
                            }
                            rtrn.useracc = CurrentUser;
                            string returnclassstring = serializer(rtrn);
                            byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(returnclassstring);
                            //SEND
                            nwStreamSTC.Write(bytesToSend, 0, bytesToSend.Length);
                            break;
                        }
                    case "FORWARD":
                        { //email deserialization
                          //should recieve: UserREQemailnew recipient
                          //WHEN FORWARDING - NEED TO SPECIFY ADDRESS OFC
                            Email emailtoforward = new Email();
                            dataReceived = dataReceived.Substring(0, dataReceived.IndexOf('')); //
                            string newrecipient = dataReceived.Substring(dataReceived.IndexOf('') + 1);
                            emailtoforward = deserializer(emailtoforward, dataReceived);
                            MailAddress rec = new MailAddress(newrecipient);
                            emailtoforward.Forward(rec);
                            //Should return userclass
                            ReturnClass RT = new ReturnClass();
                            RT.Successfull(); //Sets successflag to true, and exceptionstring to be empty
                            CurrentUser=Write.UpdateList(CurrentUser,Folder.sent); //Updates inboxes
                            CurrentUser=Write.UpdateList(CurrentUser, Folder.inbox); //Not sure if needed. Does it not update inbox when showing anyways?
                            RT.useracc = CurrentUser;
                            //Return that shit
                            string returnclassstring = serializer(RT);
                            byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(returnclassstring);
                            //SEND
                            nwStreamSTC.Write(bytesToSend, 0, bytesToSend.Length);
                            break;
                        }
                    case "REPLY":
                        { //email deserialization
                            Email emailtoforward = new Email();
                            dataReceived = dataReceived.Substring(0, dataReceived.IndexOf('')); //
                            string newtext = dataReceived.Substring(dataReceived.IndexOf('') + 1);
                            emailtoforward = deserializer(emailtoforward, dataReceived);
                            emailtoforward.Reply(newtext); //old email + the new text
                            //Should return userclass
                            ReturnClass RT = new ReturnClass();
                            RT.Successfull(); //Sets successflag to true, and exceptionstring to be empty
                            CurrentUser = Write.UpdateList(CurrentUser, Folder.sent); //Updates inboxes
                            CurrentUser = Write.UpdateList(CurrentUser, Folder.inbox); //Not sure if needed. Does it not update inbox when showing anyways?
                            RT.useracc = CurrentUser;
                            //Return that shit
                            string returnclassstring = serializer(RT);
                            byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(returnclassstring);
                            //SEND
                            nwStreamSTC.Write(bytesToSend, 0, bytesToSend.Length);
                            break;
                        }
                    case "UPDATEINBOX":
                        { //Should find folder to update in serialized message and only update one, however only a question of time complexity
                            //Currently recieves: UserID + REQ + UserAccount
                            Console.WriteLine("jepp + \n :" + dataReceived);
                            string curruser = dataReceived; //Takes current user
                            dataReceived = dataReceived.Substring(dataReceived.IndexOf('') + 1);
                            CurrentUser = deserializer(CurrentUser,curruser);
                            ReturnClass rtrn = new ReturnClass();
                            try
                            {
                                Console.WriteLine("Updating in box server side");
                                Write.UpdateList(CurrentUser, Folder.sent);
                                Write.UpdateList(CurrentUser, Folder.drafts);
                                CurrentUser = Write.UpdateList(CurrentUser, Folder.inbox);
                                rtrn.success = true;
                            }
                            catch (Exception ex)
                            {
                                rtrn.success = false;
                                rtrn.exceptionstring = ex.ToString();
                            }
                            rtrn.useracc = CurrentUser;
                            string returnclassstring = serializer(rtrn);
                            byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(returnclassstring);
                            //SEND
                            nwStreamSTC.Write(bytesToSend, 0, bytesToSend.Length);

                            STCclient.Close();
                            //inbox deserialization
                            break;
                        }
                    case "DELETE":
                        {
                            //user dl req dl useraccount dl foldertype dl filename
                            //Catch useraccount
                            string curruser = dataReceived.Substring(0,dataReceived.IndexOf(''));
                            CurrentUser = deserializer(CurrentUser, curruser);
                            dataReceived = dataReceived.Substring(dataReceived.IndexOf('')+1);
                            string foldertype = dataReceived.Substring(0,dataReceived.IndexOf(''));
                            dataReceived = dataReceived.Substring(dataReceived.IndexOf('') + 1);
                            string filename = dataReceived;
                            string path = dbdir+@"\Users\"+CurrentUser.UserName+@"\"+foldertype;
                            Console.WriteLine("Foldertype recieved: "+foldertype+" filename recieved: "+filename);
                            string[] testing = Directory.GetFiles(path);
                            foreach (string fileiterator in testing) //loop through the folder 
                            {
                                Console.WriteLine("Processed file '{0}'.", fileiterator);
                                string abb = fileiterator.Substring(fileiterator.LastIndexOf(@"\")+1);
                                Console.WriteLine("Abb: " + abb);
                                Console.WriteLine("filename: "+filename+@".txt");
                                if (abb.Equals(filename+@".txt"))
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write("\nFound file to delete \n");
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    File.Delete(fileiterator);
                                    Console.Write("\nFile Deleted");
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                             }
                            ReturnClass RT = new ReturnClass();   
                            RT.Successfull();
                            Write.UpdateList(CurrentUser, Folder.sent);
                            Write.UpdateList(CurrentUser, Folder.drafts);
                            CurrentUser = Write.UpdateList(CurrentUser, Folder.inbox);
                            RT.useracc = CurrentUser;
                            //send that shit back
                            string returnclassstring = serializer(RT);
                            byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(returnclassstring);
                            //SEND
                            nwStreamSTC.Write(bytesToSend, 0, bytesToSend.Length);
                            STCclient.Close();
                            break;
                        }
                    case "DRAFT": 
                        { //email deserialization
                            break;
                        }
                    default:
                        break;
                }
                STCclient.Close();
                client.Close();
                CTSlistener.Stop(); 
           }
        }

        private static void read()
        {
            throw new NotImplementedException();
        }
        public UserAccount UpdateAll(UserAccount UA) //Updates all 3 lists
        {
            UA = Write.UpdateList(UA, Folder.inbox); 
            UA =Write.UpdateList(UA,Folder.sent);
            UA = Write.UpdateList(UA, Folder.drafts);
            return UA;
        }
    }
}