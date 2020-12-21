using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
namespace Server
{

    public class Write
    {
        public static string dbdir = Loca();

        public static int counter = 1;
        public static int counter2 = 1;

        public static string Loca()
        {
            
            string currentdir = Environment.CurrentDirectory; //Gets location of exe file
            Console.WriteLine("Curr dir is:" + currentdir);
            try {
                while (!(currentdir.EndsWith(@"\WeMailProjectV2\Email"))) {
                    currentdir = currentdir.Substring(0, currentdir.LastIndexOf(@"\"));
                    Console.WriteLine("curr dir is:" + currentdir);
                }
            }
            catch {
                Console.WriteLine(currentdir);
            }
            return dbdir = currentdir;
        }

        public static void Files(Email inputEmail)
        {
            Loca();
            String senderEmail = inputEmail.senderAddress;
            String userID = senderEmail.Substring(0, senderEmail.IndexOf("@"));    // userID before @
            Console.WriteLine("dbdir is:"+dbdir);
            string dir = dbdir + "/Users/" + userID + "/sent/" + inputEmail.subjectMatter + ".txt";

            if (!(File.Exists(dir))) {   // if ! so only "if" works
               

                StreamWriter sw = new StreamWriter(dir);

                sw.WriteLine(inputEmail.emailType + "," + inputEmail.senderAddress + "," + inputEmail.receiverAddress + "," + inputEmail.timeStamp + "," +
                    inputEmail.contentText + "," + inputEmail.emailFlag);
                sw.Flush();
                sw.Close();
            }
           else {          // if no "!" then else "works"

                counter++;
                StreamWriter SW = new StreamWriter(dbdir + "/Users/" + userID + "/sent/" + inputEmail.subjectMatter + counter + ".txt");

                SW.WriteLine(inputEmail.emailType + "," + inputEmail.senderAddress + "," + inputEmail.receiverAddress + "," + inputEmail.timeStamp + "," +
                      inputEmail.contentText + "," + inputEmail.emailFlag);
                SW.Flush();
                SW.Close();
            }
        }

        public static void draft(Email inputEmail)
        {
            Loca();
            String senderEmail = inputEmail.senderAddress;
            String userID = senderEmail.Substring(0, senderEmail.IndexOf("@"));    // userID before @
            Console.WriteLine("dbdir is:" + dbdir);

            StreamWriter sw = new StreamWriter(dbdir + "/Users/" + userID + "/drafts/" + inputEmail.subjectMatter + ".txt", true);
            sw.WriteLine(inputEmail.emailType + "," + inputEmail.senderAddress + "," + inputEmail.receiverAddress + "," + inputEmail.timeStamp + "," +
                  inputEmail.contentText + "," + inputEmail.emailFlag);
            sw.Flush();
            sw.Close();
        }

        public static void Files2(Email inputEmail)
        {
            Loca();
            var reciver = inputEmail.receiverAddress;
            String reciverID = reciver.Substring(0, reciver.IndexOf("@"));

            string dir = dbdir + "/Users/" + reciverID + "/inbox/" + inputEmail.subjectMatter + ".txt";

            if (!(File.Exists(dir))) {   // if ! so only "if" works

                StreamWriter sw = new StreamWriter(dir);
                sw.WriteLine(inputEmail.emailType + "," + inputEmail.senderAddress + "," + inputEmail.receiverAddress + "," + inputEmail.timeStamp + "," +
                      inputEmail.contentText + "," + inputEmail.emailFlag);
                sw.Flush();
                sw.Close();
            }
            else {

                counter2++;
                StreamWriter SW = new StreamWriter(dbdir + "/Users/" + reciverID + "/inbox/" + inputEmail.subjectMatter + counter2 +".txt");

                SW.WriteLine(inputEmail.emailType + "," + inputEmail.senderAddress + "," + inputEmail.receiverAddress + "," + inputEmail.timeStamp + "," +
                      inputEmail.contentText + "," + inputEmail.emailFlag);
                SW.Flush();
                SW.Close();
            }
        }

        public static void read(Email ReadFile)
        {
            String senderEmail = ReadFile.senderAddress;
            String userID = senderEmail.Substring(0, senderEmail.IndexOf("@"));
           
            using (var sr = new StreamReader(dbdir + "/Users/" + userID + "/sent/" + ReadFile.subjectMatter + ".txt"))  // read the directry of the userid and password
            {
                while (!sr.EndOfStream) {
                    var line = sr.ReadLine();
                    string[] words = line.Split(',');
                    if (String.IsNullOrEmpty(line)) continue;
                    Console.WriteLine(words[0]);

                }

            }
        }
        public static UserAccount edit(UserAccount UA,Email ReadFile, int index, string newcontent) //Takes Email and updates a part of it
        {
            Loca();
            String userID = ReadFile.senderAddress.Substring(0, ReadFile.senderAddress.IndexOf("@"));    // userID before @
            Console.WriteLine("dbdir is:" + dbdir);
            StreamWriter sw = new StreamWriter(dbdir + "/Users/" + userID + "/sent/" + ReadFile.subjectMatter + ".txt", false); //true appends, false overwrites
            sw.WriteLine(ReadFile.emailType+","+ReadFile.senderAddress+","+ReadFile.receiverAddress+","+ReadFile.timeStamp+","+ReadFile.contentText+","+@newcontent);
            sw.Flush();
            sw.Close();
            Console.WriteLine("\nEmail was updated locally.. Trying to update user list");
            UA = UpdateList(UA,(Folder)Enum.Parse(typeof(Folder),ReadFile.emailType)); //Changes from string to enum Folder
            return UA; //Need a way to update this cunt
        }

        public static UserAccount UpdateList(UserAccount UA,Folder folder) //Updates list of emails connected to account
        {
            string foldertype = folder.ToString();
            Console.WriteLine("\n\n Updating Folder"+foldertype+" for:"+UA.UserName+"\n\n");
            string path = dbdir + @"\Users\" + UA.UserName + @"\"+foldertype; //Path to the folders
            string[] extension = { ".txt" };
            var files= Directory.EnumerateFiles(path, "*.*", SearchOption.AllDirectories)
                .Where(s => extension.Any(ext => ext == System.IO.Path.GetExtension(s)));
            List<string> textfile = new List<string>(); //List of the subject matters 
            /*
            for (int i = 0; i < textfile.Count; i++)
            {
                Console.WriteLine("\n"+textfile[i]);
            } */
            string[] testing = Directory.GetFiles(path);
            foreach (string filename in testing)
                Console.WriteLine("Processed file '{0}'.",filename);

            textfile = files.ToList();     // textfile er en list med string
            List<Email> updatedList = new List<Email>();
            //updatedList = readIntoEmailClass(UA,foldertype,textfile);
            List<string> tt = new List<string>();
            tt = testing.ToList();
            Console.WriteLine("\n                COUNT"+tt.Count()+"\n\n");
            updatedList = readIntoEmailClass(UA, foldertype, tt);

            foreach (string filename in tt)
                Console.WriteLine("Processed file v2 '{0}'.", filename);

            Console.WriteLine("\n\n AMOUNT IN FOLDER:" + updatedList.Count() + "\n\n");
            for (int i = 0; i < updatedList.Count; i++)
            {
                Console.WriteLine("\n\n StringList:"+i.ToString()+" "+textfile[i]);
            }
            switch(foldertype)
            {
                case ("inbox"):
                    Console.WriteLine("Case: Inbox");
                    UA.inboxFolder = updatedList;
                    break;
                case ("drafts"):
                    Console.WriteLine("Case: Drafts");
                    UA.draftFolder = updatedList;
                    break;
                case ("sent"):
                    Console.WriteLine("\n\nCase: sent\n\n");
                    UA.sentFolder = updatedList;
                    break;
            }
            return UA;
        }

        public static List<Email> readIntoEmailClass(UserAccount UA, string foldertype, List<string> texts) //Takes list of email names and creates+returns instance list from it.
        {
            //Console.Write("We in writeTo.cs:readIntoEmailClass");
            Console.Write("DBDIR: " + dbdir);
            List<Email> listOfEmails= new List<Email>();
            
            Console.WriteLine("ReadIntoClass:     Amount: "+texts.Count+"\n Foldertype::"+foldertype);
         for (int i = 0; i<texts.Count();i++) {
                Email email = new Email();
                using (var stringread = new StreamReader(texts[i]))
                {
                    Console.WriteLine("OL"+texts[i]);
                    while (!stringread.EndOfStream)
                    {
                        var line = stringread.ReadLine();
                        string[] words = line.Split(',');
                        if (String.IsNullOrEmpty(line)) continue;
                        email.emailType= words[0];
                        email.senderAddress = words[1];
                        email.receiverAddress = words[2];
                        email.timeStamp = words[3];
                        email.contentText = words[4];
                        email.emailFlag = words[5];
                        email.subjectMatter = texts[i].Substring(texts[i].LastIndexOf(@"\")+1).Substring(0, texts[i].Substring(texts[i].LastIndexOf(@"\")).LastIndexOf(@".")-1); 
                        Console.WriteLine("YAX: "+email.subjectMatter);
                    }
                }
                //Console.WriteLine("EmailSubject for :" + i + @"->" + email.subjectMatter);
                listOfEmails.Add(email); //Cannot access using indices, as initial capacity is 0. This makes it grow dynamically. 
            }
            return listOfEmails;
        }    
    }
}

