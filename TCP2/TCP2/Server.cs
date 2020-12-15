using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Xml.Serialization;
using System.IO;

public class user
{
    public string username, password;
    public List<emailFolder> userEmailFolders;
}

public class emailFolder
{
    public string folderType;
    public List<Email> emailList;

    public emailFolder receiveFolder()
    {
        bool success;
        emailFolder emailFolderVar = new emailFolder();
        return emailFolderVar; 
        //return success == true; 
    }
}

public class Email
{
    public string emailType, senderAddress, receiverAddress, timeStamp, subjectMatter, newContentText, oldContentText, emailFlag; //emailFlag can be read, unread, important

    public Email() { }

    public Email(string inputEmailType, string inputSenderAddress, string inputReceiverAddress, string inputTimeStamp, string inputSubjectMatter, string inputNewContentText, string inputOldContentText, string inputFlag)
    {
        emailType = inputEmailType;
        senderAddress = inputSenderAddress;
        receiverAddress = inputReceiverAddress;
        timeStamp = inputTimeStamp;
        subjectMatter = inputSubjectMatter;
        newContentText = inputNewContentText;
        oldContentText = inputOldContentText;
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
        Console.WriteLine("\n Content: " + this.newContentText);

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
}

namespace Server
{
    /*
    public void requestHandler(string USER, string FUNC, string data)
    {
        switch (FUNC)
        {
            case "CREATEUSER": //user deserialization
                break;
            case "LOGIN": //user deserialization
                break;
            case "MARK": //email deserialization
                break;
            case "SEND": //email deserialization
                Email newEmail = new Email();
                newEmail = deserializer(newEmail, data);
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
    }
    */
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

                //---convert the data received into a string---
                string dataReceived = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                //---parse string to read USER, REQ and data into separate strings---
                string USER = dataReceived.Substring(0, dataReceived.IndexOf(''));
                dataReceived = dataReceived.Substring(dataReceived.IndexOf('') + 1);
                string REQ = dataReceived.Substring(0, dataReceived.IndexOf(''));
                dataReceived = dataReceived.Substring(dataReceived.IndexOf('') + 1);

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

