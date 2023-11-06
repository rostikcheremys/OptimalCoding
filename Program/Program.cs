using System;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;

namespace Program
{
    internal abstract class Program
    {
        static void Main()
        {
            Console.WriteLine("Введіть 1 для метода кодування методикою Хаффмена:");
            Console.WriteLine("Введіть 2 для метода кодування методикою Шеннона-Фано:");
            
            uint choice = Convert.ToUInt32(Console.ReadLine());
            
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Введіть через пробіл ймовірності виникнення символів:");
                    
                    List<double> listOfFirst = Console.ReadLine()!.Trim().Split().Select(s => double.Parse(s, CultureInfo.InvariantCulture)).ToList();
                    Dictionary<string, string> dictOfFirst = new Dictionary<string, string>();
                    
                    Huffman taskOfFirst = new Huffman();

                    taskOfFirst.TaskOfFirst(dictOfFirst, listOfFirst);
                    break;
                
                case 2:
                    Console.WriteLine("Введіть через пробіл ймовірності виникнення символів:");
                    
                    List<double> listOfSecond = Console.ReadLine()!.Trim().Split().Select(s => double.Parse(s, CultureInfo.InvariantCulture)).ToList();
                    Dictionary<string, string> dictOfSecond = new Dictionary<string, string>();

                    ShannonFano taskOfSecond = new ShannonFano();
            
                    taskOfSecond.TaskOfSecond(dictOfSecond, listOfSecond);
                    break;
            }
        }
    }
}