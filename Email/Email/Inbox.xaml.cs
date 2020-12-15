using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;


namespace Email
{
    public partial class Inbox : Window
    {
        public Inbox()
        {
            InitializeComponent();
        }

        public static List<string> FindSent()               // function to fund the mails under sent folder and store in the list
        {
            string path = "S:/Email/Email/123/sent";
            string[] extensions = { ".txt" };

            var files = Directory.EnumerateFiles(path, "*.*", SearchOption.AllDirectories)
                .Where(s => extensions.Any(ext => ext == System.IO.Path.GetExtension(s)));

            List<string> textfile = new List<string>();

            textfile = files.ToList();     // textfile er en list med string
            return textfile;
        }

        private void Text()       // function to print the list of mails in sent folder
        {
            var numberOfLabels = 3;
            List<string> FillIn = new List<string>();
            FillIn = FindSent();      // FillIn er også en list med string

            List<string> FileName = new List<string>();

            FileName = FillIn;

            for (int i = 1; i <= numberOfLabels; i++) {
                FileName[i-1] = System.IO.Path.GetFileNameWithoutExtension(FillIn[i-1]);
                var labelName = string.Format("Label{0}", i);
                var label = (Label)this.FindName(labelName);
                label.Content = FileName[i-1];
            }
        }

        private void SentFolder_Click(object sender, RoutedEventArgs e)
        {
            Text();

            TXT.hey();
            TXT.read();
        }
    }
}
