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
            Console.WriteLine("Введiть 1 для метода кодування методикою Хаффмена:");
            Console.WriteLine("Введiть 2 для метода кодування методикою Шеннона-Фано:");
            
            uint choice = Convert.ToUInt32(Console.ReadLine());
            
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Введiть через пробiл ймовiрностi виникнення символiв:");
                    
                    List<double> listOfFirst = Console.ReadLine()!.Trim().Split().Select(s => double.Parse(s, CultureInfo.InvariantCulture)).ToList();
                    Dictionary<string, string> dict = new Dictionary<string, string>();
                    
                    Huffman taskOfFirst = new Huffman();

                    taskOfFirst.TaskOfFirst(dict, listOfFirst);
                    break;
                
                case 2:
                    Console.WriteLine("Введiть через пробiл ймовiрностi виникнення символiв:");
                    
                    List<double> listOfSecond = Console.ReadLine()!.Trim().Split().Select(s => double.Parse(s, CultureInfo.InvariantCulture)).ToList();

                    ShannonFano taskOfSecond = new ShannonFano();
            
                    taskOfSecond.TaskOfSecond(listOfSecond);
                    break;
            }
        }
    }
}