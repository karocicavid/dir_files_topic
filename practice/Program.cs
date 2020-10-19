using System;
using System.IO;

namespace ConsoleApp
{   class Index
    {
        Client[] data;
        public Index()
        {
            data = new Client[100];
        }
        public Client this [int index]
            {
            set { data[index] = value; }
            get { return data[index]; }
            }
    }
    class Client
    {
        public string id { get; set; }
        public string passport { get; set; }
        public string payment { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                Index index = new Index();
                string path = @"D:\test_client";
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                if (!dirInfo.Exists)
                {
                    dirInfo.Create();
                }
                string filepath = @"D:\test_client\test_client.txt";
                FileInfo File = new FileInfo(filepath);
                if (!File.Exists)
                {
                    File.Create();
                }
                using (StreamWriter wr = new StreamWriter(filepath, true, System.Text.Encoding.Default))//writing in file
                {
                    wr.WriteLine("001; AZE12345678;22\n" +
                        "  002; AZE87652134;50\n" +
                        "  003; AZE87652134;12");
                }

                using (StreamReader sr = new StreamReader(filepath))//reading what we write
                {
                    for (int i = 1; i <= 3; i++)
                    {
                        string str = sr.ReadLine();
                        string[] line = str.Split(';');
                        string id = line[0];
                        string passport = line[1];
                        string payment = line[2];
                        index[i] = new Client { id = id, passport = passport, payment = payment };

                    }
                }
                int total = 0;
                for (int i = 1; i <= 3; i++)
                {
                    int payment = Convert.ToInt32(index[i].payment);//стринг конвертируем в инт чтобы сложить
                    total += payment;
                }
                Console.WriteLine(total);
                string total_string = Convert.ToString(total);// инт конвертируем в стринг
                string newfilepath = @"D:\test_client\new_test_client.txt";

                using (StreamWriter wr = new StreamWriter(newfilepath, false, System.Text.Encoding.Default))
                {
                    wr.WriteLine($"We noticed total sum of payments - {total}");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Files are created,let's check !");
                    Console.ResetColor();
                }
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("При втором запуске производится выдача результатов! ");
                Console.ResetColor();
            }
            Console.Read();
        }
    }
}
