using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Zavd3
{
    public partial class MainWindow : Window
    {
        static CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
        CancellationToken token = cancelTokenSource.Token;
        public int sentences_count = 0;
        public int characters_count;
        public int interrogative_sentences_count = 0;
        public int exclamation_points_count = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            Task task1 = new Task(
                () => Number_of_sentences()
            );
            Task task2 = task1.ContinueWith(Number_of_characters);
            Task task3 = task2.ContinueWith(Number_of_words);
            Task task4 = task3.ContinueWith(Number_of_interrogative_sentences);
            Task task5 = task4.ContinueWith(Number_of_exclamation_points);
            Task task6 = task5.ContinueWith(Runn);
            task1.Start();
        }

        public void Number_of_sentences()
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {

                for (int i = 0; i < Textbox.Text.Length; i++)
                {
                    if (Textbox.Text[i] == '.' || Textbox.Text[i] == '!' || Textbox.Text[i] == '?')
                    {
                        sentences_count++;
                    }
                }

            }));
        }

        public void Number_of_characters(Task t)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                string temp_string = Textbox.Text;
                temp_string = temp_string.Replace(" ", "");
                characters_count = temp_string.Length;

            }));
        }

        public void Number_of_words(Task t)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {

                int count_words = 0;
                char[] wordsSplit = new char[] { ' ', ',', '!', '?', '.' };
                string[] words = Textbox.Text.Split(wordsSplit, StringSplitOptions.RemoveEmptyEntries);
                count_words = words.Count();
            }));
        }

        public void Number_of_interrogative_sentences(Task t)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                for (int i = 0; i < Textbox.Text.Length; i++)
                {
                    if (Textbox.Text[i] == '?')
                    {
                        interrogative_sentences_count++;
                    }
                }

            }));
        }

        public void Number_of_exclamation_points(Task t)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                for (int i = 0; i < Textbox.Text.Length; i++)
                {
                    if (Textbox.Text[i] == '!')
                    {
                        exclamation_points_count++;
                    }
                }

            }));
        }

        public void Runn(Task t)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {

                if (radiobt1.IsChecked == true)
                {
                    List<string> arr = new List<string>();
                    if (CheckBox1.IsChecked == true)
                        arr.Add($"Sentences count: {sentences_count}");
                    if (CheckBox2.IsChecked == true)
                        arr.Add($"Characters count: {characters_count}");
                    if (CheckBox3.IsChecked == true)
                        arr.Add($"Interrogative sentences count: {interrogative_sentences_count}");
                    if (CheckBox4.IsChecked == true)
                        arr.Add($"Exclamation points count: {exclamation_points_count}");
                    list_box.ItemsSource = arr;
                }
                else if (radiobt2.IsChecked == true)
                {
                    string str = null;
                    using (StreamWriter sw = new StreamWriter("zavd1.txt", false, System.Text.Encoding.Default))
                    {
                        if (CheckBox1.IsChecked == true)
                            str += $"Sentences count: {sentences_count}\n";
                        if (CheckBox2.IsChecked == true)
                            str += $"Count characters count: {characters_count}\n";
                        if (CheckBox3.IsChecked == true)
                            str += $"Count interrogative sentences count: {interrogative_sentences_count}\n";
                        if (CheckBox4.IsChecked == true)
                            str += $"Exclamation points count: {exclamation_points_count}\n";
                        sw.Write(str);
                    }
                }
            }));
        }

        private void pause_Click(object sender, RoutedEventArgs e)
        {
            cancelTokenSource.Cancel();
        }
    }
}
