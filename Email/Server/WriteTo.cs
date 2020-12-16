using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Server
{

    public class Write
    {

        public static void Files(Email inputEmail)
        {
            String senderEmail = inputEmail.senderAddress;
            String userID = senderEmail.Substring(0, senderEmail.IndexOf("@"));    // userID before @

            StreamWriter sw = new StreamWriter("S:/Email/Email/Users/" + userID + "/sent/" + inputEmail.subjectMatter + ".txt", true);
            sw.WriteLine(inputEmail.emailType + "," + inputEmail.senderAddress + "," + inputEmail.receiverAddress + "," + inputEmail.timeStamp + "," +
                  inputEmail.contentText + "," + inputEmail.emailFlag);
            sw.Flush();
            sw.Close();
        }

        public static void read(Email ReadFile)
        {
            String senderEmail = ReadFile.senderAddress;
            String userID = senderEmail.Substring(0, senderEmail.IndexOf("@"));
           
            using (var sr = new StreamReader("S:/Email/Email/Users/" + userID + "/sent/" + ReadFile.subjectMatter + ".txt"))  // read the directry of the userid and password
            {
                while (!sr.EndOfStream) {
                    var line = sr.ReadLine();
                    string[] words = line.Split(',');
                    if (String.IsNullOrEmpty(line)) continue;
                    Console.WriteLine(words[0]);
                }

            }
            


        }
    }
}

