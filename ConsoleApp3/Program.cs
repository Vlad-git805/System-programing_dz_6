using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            string str;
            List<int> list = new List<int>();
            using (FileStream fs = new FileStream("numbers.txt", FileMode.Open, FileAccess.Read))
            {

                StreamReader streamReader = new StreamReader(fs);
                while (!streamReader.EndOfStream)
                {
                    str = streamReader.ReadLine();
                    list.Add(int.Parse(str));
                }

                streamReader.Close();
            }

            List<int> index_list = new List<int>();
            for (int i = 0; i < list.Count; i++)
            {
                index_list.Add(i);
            }

            var plnq = index_list.AsParallel()
                                    .AsOrdered()
                                    .Select(n => maximum_length_of_the_sequence_numbers(n, list));


            Console.WriteLine(plnq.Max());
        }

        static int maximum_length_of_the_sequence_numbers(int x, List<int> list)
        {
            int count = 0;
            for (int i = x; i < list.Count; i++)
            {
                if(list[i] < 0)
                {
                    return 0;
                }
                else if (i < list.Count - 1)
                {
                    if (list[i + 1]>=0)
                    {
                        count++;
                        if (i + 1 == list.Count - 1)
                        {
                            count++;
                            return count;
                        }
                    }
                    if (list[i + 1] <0)
                    {
                        count++;
                        return count;
                    }
                }
            }
            return ++count;
        }
    }
}
