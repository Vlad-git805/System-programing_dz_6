using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Zavd4
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = @"C:\Users";
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                analisfolder.Text = dialog.FileName;
                DirectoryInfo d = new DirectoryInfo(dialog.FileName);
                FileInfo[] Files = d.GetFiles("*.txt", SearchOption.AllDirectories);
            }
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = @"C:\Users";
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                destfolder.Text = dialog.FileName;
                DirectoryInfo d = new DirectoryInfo(dialog.FileName);
                FileInfo[] Files = d.GetFiles("*.txt", SearchOption.AllDirectories);
            }
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            if (analisfolder.Text != "")
            {
                Task task1 = new Task(() => FileAnalysis());
                task1.Start();
            }


        }

        private void CopyFile(string path)
        {

            string copyFileName = $"{System.IO.Path.GetFileNameWithoutExtension(path)}{System.IO.Path.GetExtension(path)}";
            string destFile = System.IO.Path.Combine(destfolder.Text, copyFileName);

            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                using (FileStream sourceFs = File.OpenRead(path))
                {

                    FileStream destFs = new FileStream(destFile, FileMode.OpenOrCreate, FileAccess.Write);

                    destFs.SetLength(sourceFs.Length);

                    long sourceFileLength = sourceFs.Length;
                    byte[] buffer = new byte[4*16000];
                    long readBytes = 0;
                    long totalReadBytes = 0;

                    do
                    {
                        readBytes = sourceFs.Read(buffer, 0, buffer.Length);
                        destFs.Write(buffer, 0, (int)readBytes);
                        totalReadBytes += readBytes;

                    } while (readBytes > 0);

                    destFs.Close();
                }
            }));
        }

        public void FileAnalysis()
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                string[] fileArray = Directory.GetFiles(analisfolder.Text, "*.txt", SearchOption.AllDirectories);

                for (int j = 0; j < fileArray.Length; j++)
                {
                    Regex regex = new Regex(@".*[(]\d[)]");
                    MatchCollection matches = regex.Matches(fileArray[j]);
                    if (matches.Count > 0)
                        continue;

                    int count = 0;
                    string path1 = fileArray[j].Remove(fileArray[j].Length - 4, 4);

                    for (int i = 0; i < fileArray.Length; i++)
                        if (fileArray[i].Contains(path1))
                            count++;

                    if (count > 1)
                        CopyFile(fileArray[j]);
                }

            }));
        }
    }
}
