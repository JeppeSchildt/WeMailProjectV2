using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CLIENT
{
    public class Write 
    {
        // public string emailType, senderAddress, receiverAddress, timeStamp, subjectMatter, newContentText, oldContentText, emailFlag; //emailFlag can be read, unread, important

        public static string emailType = "emailType",
        senderAddress = "senderAddress",
            receiverAddress = " receiverAddress",
            timeStamp = " timeStamp",
            subjectMatter = "BIL",
            newContentText = "newContentText",
            oldContentText = " oldContentText",
            emailFlag = "emailFlag";

        public static void you(string inputEmailType, string inputSenderAddress, string inputReceiverAddress,
           string inputTimeStamp, string inputSubjectMatter, string inputNewContentText, string inputOldContentText, string inputFlag)
        {
            emailType = inputEmailType;
            senderAddress = inputSenderAddress;
            receiverAddress = inputReceiverAddress;
            timeStamp = inputTimeStamp;
            subjectMatter = inputSubjectMatter;
            newContentText = inputNewContentText;
            oldContentText = inputOldContentText;
            emailFlag = inputFlag;
            Files();
        }

        public static void hey()
        {
            you(emailType, senderAddress, receiverAddress, timeStamp, subjectMatter, newContentText, oldContentText, emailFlag);
        }       
      
        public static void Files ()
        {
            StreamWriter sw = new StreamWriter(LogIn.dbdir +"/ Users/" + LogIn.userID + "/sent/" + subjectMatter + ".txt", true);
            sw.WriteLine(emailType + "," + senderAddress + "," + receiverAddress + "," +timeStamp + "," + newContentText + ","
                + oldContentText + "," + emailFlag );
            sw.Flush();
            sw.Close(); 
        }
        

        public static void read()
        {
            using (var sr = new StreamReader(LogIn.dbdir +"/Users/" + LogIn.userID +"/sent/" + subjectMatter+ ".txt"))  // read the directry of the userid and password
            {
                while (!sr.EndOfStream) {
                    var line = sr.ReadLine();
                    string[] words = line.Split(',');
                    if (String.IsNullOrEmpty(line)) continue;
                    Console.WriteLine(words[5]);
                }
            }

           
        }
    }
}

