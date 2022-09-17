using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace WordFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Не указан путь к файлу! Введите директорию файла в свойствах проекта во вкладке 'Отладка'.");
                return;
            }
            string path = args[0];
           
            if (File.Exists(path)) path = args[0];
            else
            {
                Console.WriteLine("Файл не найден! Проверьте корректность директории и названия файла.");
                return;
            }
            
            string line;
            var dict = new Dictionary<string, int>();

            using (StreamReader sr = new StreamReader(path, Encoding.UTF8))
            {
                while ((line = sr.ReadLine()) != null)
                {

                    string[] str = line.Split(new char[] { ' ', ',', '.', '-', '!', '?', '"', '[', ']', ';', ':', '(', ')' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var value in str)
                    {
                        var lowerStr = value.ToLower();

                        if (dict.ContainsKey(lowerStr))
                            dict[lowerStr]++;
                        else
                            dict[lowerStr] = 1;
                    }
                }

                sr.Close();
            }

            var sortedDict = from entry in dict orderby entry.Value descending select entry;

            using (var result = new StreamWriter("result.txt"))
                foreach (var pair in sortedDict)
                {
                    result.WriteLine($"{pair.Key}\t{pair.Value}");
                }
            Console.WriteLine("Программа успешно завершена! Смотри файл result.txt в корне программы");
            Console.ReadLine();
        }
    }
}

