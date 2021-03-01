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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace zavd1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Start_button_Click(object sender, RoutedEventArgs e)
        {
            if (Text_box.Text != "")
            {
                int lenght = Text_box.Text.Length;
                Task[] tasks1 = new Task[5]
                {
                    new Task(() =>
                    {
                        int count = 0;
                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                        for (int i = 0; i < lenght; i++)
                        {
                            if(Text_box.Text[i] == '.' || Text_box.Text[i] == '?' || Text_box.Text[i] == '!')
                            {
                                count++;
                                count_sentences_lable.Content = count.ToString();
                            }
                        }
                        }));
                    }),
                    new Task(() =>
                    {
                        char[] vowels = new char[] { '!', '"', '#', '$', '%', '$', '\'', '(', ')', '*', '+', '`', '-', '_', '.', '/', ':', ';', '<', '=', '>', '?', '@', '[', '\\', ']', '^', '|', '}', '~', ','};
                        int count = 0;
                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                        for (int i = 0; i < lenght; i++)
                        {
                            for (int j = 0; j < vowels.Length; j++)
                            {
                                if (Text_box.Text[i] == vowels[j])
                                {
                                    count++;
                                    count_symbols_lable.Content = count.ToString();
                                }
                            }
                        }
                        }));
                    }),
                    new Task(() =>
                    {
                        char[] wordsSplit = new char[] { ' ', ',', '!', '?', '.' };
                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                        string[] words = Text_box.Text.Split(wordsSplit, StringSplitOptions.RemoveEmptyEntries);
                        count_words_lable.Content = words.Length.ToString();
                        }));
                    }),
                    new Task(() =>
                    {

                        int count = 0;
                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                        for (int i = 0; i < lenght; i++)
                        {
                            if(Text_box.Text[i] == '!')
                            {
                                count++;
                                count_vowel_lable.Content = count.ToString();
                            }
                        }
                        }));
                    }),
                    new Task(() =>
                    {
                        int count = 0;
                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                        for (int i = 0; i < lenght; i++)
                        {
                            if(Text_box.Text[i] == '?')
                            {
                                count++;
                                count_questions_lable.Content = count.ToString();
                            }
                        }
                        }));
                    })
                };
                foreach (var t in tasks1)
                    t.Start();

                Task.WaitAll(tasks1);
            }
        }
    }
}
