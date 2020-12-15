using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Email
{
    public class TXT
    {
        // public string emailType, senderAddress, receiverAddress, timeStamp, subjectMatter, newContentText, oldContentText, emailFlag; //emailFlag can be read, unread, important


        public static string emailType = "1",
        senderAddress = "2f",
            receiverAddress = "3",
            timeStamp = "4",
            subjectMatter = "BIL",
            newContentText = "5",
            oldContentText = "6",
            emailFlag = "7";

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
            StreamWriter sw = new StreamWriter("S:/Email/Email/TranslateToTXT/" + subjectMatter + ".txt", true);
            sw.WriteLine(emailType + "," + senderAddress + "," + receiverAddress + "," +timeStamp + "," + newContentText + ","
                + oldContentText + "," + emailFlag );
            sw.Flush();
            sw.Close();
        }

        public static void read()
        {
            using (var sr = new StreamReader("S:/Email/Email/TranslateToTXT/" + subjectMatter+ ".txt"))  // read the directry of the userid and password
           {


                while (!sr.EndOfStream) {
                    var line = sr.ReadLine();
                    string[] words = line.Split(',');
                    if (String.IsNullOrEmpty(line)) continue;
                  

                    Console.WriteLine(words[1]);

                    Console.WriteLine(words[0]);
                }
            }
        }
    }









}

