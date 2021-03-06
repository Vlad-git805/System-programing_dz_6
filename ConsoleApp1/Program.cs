using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
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

            var without_povtoru = list.AsParallel()
                                    .AsOrdered()
                                    .Where(n => Is_Without_povtors(n, list) == false)
                                    .Select(n => n);


            foreach (var item in without_povtoru)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine();
            Console.WriteLine(without_povtoru.Count());
        }

        static bool Is_Without_povtors(int x , List<int> list)
        {
            bool is_povtor = false;
            int count = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if(x == list[i])
                {
                    count++;
                }
                if(count >= 2)
                {
                    is_povtor = true;
                    break;
                }

            }
            return is_povtor;
        }
    }
}
