﻿using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public List<Resume> summaries = new List<Resume>();
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
            Parallel.Invoke(() => FileAnalysis());
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            list_box.Items.Clear();
            if (analisfolder.Text != "")
            {
                Parallel.Invoke(() => Ot4et());
            }
        }

        public void Find_most_experienced_specialist()
        {
            if (analisfolder.Text != "")
            {
                var find_most_experienced_specialist = summaries.AsParallel().AsOrdered().OrderByDescending(n => n.Count_of_work_year).First();
                list_box.Items.Add("The most experienced specialist: \n" + find_most_experienced_specialist.ToString());
            }
        }

        public void Find_less_experienced_specialist()
        {
            if (analisfolder.Text != "")
            {
                var find_less_experienced_specialist = summaries.AsParallel().AsOrdered().OrderBy(n => n.Count_of_work_year).First();
                list_box.Items.Add("The less experienced specialist: \n" + find_less_experienced_specialist.ToString());
            }
        }

        public void Find_person_with_most_big_sellers_needs()
        {
            if (analisfolder.Text != "")
            {
                var find_person_with_most_big_sellers_needs = summaries.AsParallel().AsOrdered().OrderByDescending(n => n.Salary_needs).First();
                list_box.Items.Add("Specialist with the most big seller needs: \n" + find_person_with_most_big_sellers_needs.ToString());
            }
        }

        public void Find_person_with_most_small_sellers_needs()
        {
            if (analisfolder.Text != "")
            {
                var find_person_with_most_small_sellers_needs = summaries.AsParallel().AsOrdered().OrderBy(n => n.Salary_needs).First();
                list_box.Items.Add("Specialist with the most small seller needs: \n" + find_person_with_most_small_sellers_needs.ToString());
            }
        }

        public void s()
        {
            if (analisfolder.Text != "")
            {
                if (Country_text_box.Text != "")
                {
                    var Specialists_from_the_same_country = summaries.AsParallel().AsOrdered().Where(n => n.Country == Country_text_box.Text);
                    list_box.Items.Add("Specialist which are from " + Country_text_box.Text + ": \n" + Specialists_from_the_same_country.ToString());
                }
            }
        }

        public void Ot4et()
        {
            if (analisfolder.Text != "")
            {
                var Find_most_experienced_specialist = summaries.AsParallel().AsOrdered().OrderByDescending(n => n.Count_of_work_year).First();
                list_box.Items.Add("The most experienced specialist: \n" + Find_most_experienced_specialist.ToString());

                var Find_less_experienced_specialist = summaries.AsParallel().AsOrdered().OrderBy(n => n.Count_of_work_year).First();
                list_box.Items.Add("The less experienced specialist: \n" + Find_less_experienced_specialist.ToString());

                var Find_person_with_most_big_sellers_needs = summaries.AsParallel().AsOrdered().OrderByDescending(n => n.Salary_needs).First();
                list_box.Items.Add("Specialist with the most big seller needs: \n" + Find_person_with_most_big_sellers_needs.ToString());

                var Find_person_with_most_small_sellers_needs = summaries.AsParallel().AsOrdered().OrderBy(n => n.Salary_needs).First();
                list_box.Items.Add("Specialist with the most small seller needs: \n" + Find_person_with_most_small_sellers_needs.ToString());

                if (Country_text_box.Text != "")
                {
                    var Specialists_from_the_same_country = summaries.AsParallel().AsOrdered().Where(n => n.Country == Country_text_box.Text);
                    list_box.Items.Add("Specialist which are from " + Country_text_box.Text + ": \n" + Find_person_with_most_big_sellers_needs.ToString());
                }
            }
        }

        public void FileAnalysis()
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                string[] fileArray = Directory.GetFiles(analisfolder.Text, "*.txt", SearchOption.AllDirectories);
                string lineStr;
                for (int j = 0; j < fileArray.Length; j++)
                {
                    try
                    {
                        using (FileStream fs = new FileStream(fileArray[j], FileMode.Open, FileAccess.Read))
                        {
                            StreamReader streamReader = new StreamReader(fs);
                            char[] wordsSplit = new char[] { ' ', ',', '!', '?', '.' };
                            while (!streamReader.EndOfStream)
                            {
                                lineStr = streamReader.ReadLine();
                                string[] words = lineStr.Split(wordsSplit, StringSplitOptions.RemoveEmptyEntries);
                                Resume summary = new Resume { Name = words[0], Surname = words[1], Country = words[2], Count_of_work_year = int.Parse(words[3]), Salary_needs = int.Parse(words[4]) };
                                summaries.Add(summary);
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }));
        }

        private void Resume_with_the_biggest_experience_click(object sender, RoutedEventArgs e)
        {
            list_box.Items.Clear();
            Parallel.Invoke(() => Find_most_experienced_specialist());
        }

        private void Resume_with_the_lowest_experience_click(object sender, RoutedEventArgs e)
        {
            list_box.Items.Clear();
            Parallel.Invoke(() => Find_less_experienced_specialist());
        }

        private void Resume_with_the_biggest_seller_needs_click(object sender, RoutedEventArgs e)
        {
            list_box.Items.Clear();
            Parallel.Invoke(() => Find_person_with_most_big_sellers_needs());
        }

        private void Resume_with_the_lowest_seller_needs_click(object sender, RoutedEventArgs e)
        {
            list_box.Items.Clear();
            Parallel.Invoke(() => Find_person_with_most_small_sellers_needs());
        }

        private void Show_resume_with_the_same_country(object sender, RoutedEventArgs e)
        {
            list_box.Items.Clear();
            Parallel.Invoke(() => s());
        }

        private void Show_all_resumes_click(object sender, RoutedEventArgs e)
        {
            list_box.Items.Clear();
            Parallel.Invoke(() =>
            {
                foreach (var item in summaries)
                {
                    list_box.Items.Add(item);
                }
            });
        }
    }

    public class Resume
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Country { get; set; }

        public int Count_of_work_year { get; set; }

        public int Salary_needs { get; set; }

        public string Get_full_name()
        {
            return Name + " " + Surname;
        }

        public override string ToString()
        {
            return "Name: " + Name + " Surname: " + Surname + " Country: " + Country + " Experience of specialist: " + Count_of_work_year.ToString() + " Salary needs: " + Salary_needs.ToString() + "\n";
        }
    }
}
