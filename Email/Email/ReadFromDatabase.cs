using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CLIENT
{
    class Read
    {
 
    public static string emailType,
           senderAddress ,
          receiverAddress ,
          timeStamp ,
          newContentText ,
          oldContentText ,
          emailFlag ;

        public static void read()
        {

            
            using (var sr = new StreamReader(LogIn.dbdir +"/ Users/" + LogIn.userID + "/sent/" + Write.subjectMatter + ".txt"))  // read the directry of the userid and password
            {

                while (!sr.EndOfStream) {
                    var line = sr.ReadLine();
                    string[] words = line.Split(',');
                    if (String.IsNullOrEmpty(line)) continue;

                    emailType = words[0];
                    senderAddress = words[1];
                    receiverAddress = words[2];
                    timeStamp = words[3];
                    newContentText = words[4];
                    oldContentText = words[5];
                    emailFlag = words[6];
                }
            }

        }
        public static (string, string, string, string, string, string, string) you2()
        {
            read();
            return (emailType, senderAddress,
          receiverAddress,
          timeStamp,
          newContentText,
          oldContentText,
          emailFlag);


        }

    }



}
