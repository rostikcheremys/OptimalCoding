using System;
using System.Linq;
using System.Collections.Generic;

namespace Program
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("Для кодування джерел введіть через пропуск ймовірності виникнення символів:");
            
            Dictionary<string, string> dict = new Dictionary<string, string>();
            List<double> list = Console.ReadLine()!.Trim().Split().Select(double.Parse).ToList();
           
            Task1(dict, list);
            Task2(dict, list);
        }
        
        static void Task1(Dictionary<string, string> dict, List<double> list)
        {
            var nodes = list.Select(n => new HuffmanСoding { Probability = n }).ToList();
            while (nodes.Count > 1)
            {
                nodes.Sort((x, y) => x.Probability.CompareTo(y.Probability));
                var newNode = new HuffmanСoding
                {
                    Probability = nodes[0].Probability + nodes[1].Probability,
                    Left = nodes[0],
                    Right = nodes[1]
                };
                nodes.RemoveRange(0, 2);
                nodes.Add(newNode);
            }
            int index = 1;

            GetCodes(nodes[0], "");

            var probabilities = dict.Select(kv => new ShannonFanoCoding { Name = kv.Key, Probability = list[int.Parse(kv.Key.Substring(1)) - 1], Code = kv.Value }).ToList();
            probabilities.Sort((x, y) => x.Name.CompareTo(y.Name));

            Console.WriteLine("Кодування методикою Хаффмена:");

            Console.WriteLine("Symbol\tProbability\tCode");
            foreach (var symbolProbability in probabilities)
            {
                Console.WriteLine($"{symbolProbability.Name}\t{symbolProbability.Probability}\t\t{symbolProbability.Code}");
            }

            Console.WriteLine("\n********************************************************************\n");

            void GetCodes(HuffmanСoding node, string code)
            {
                if (node.Left == null && node.Right == null) dict["x" + index++] = code;
                else
                {
                    if (node.Left != null) GetCodes(node.Left, code + "0");
                    if (node.Right != null) GetCodes(node.Right, code + "1");
                }
            }
        }

        static void Task2(Dictionary<string, string> dict, List<double> list)
        {
            var probabilities = new List<ShannonFanoCoding>();
            int index = 1;
            
            foreach (var prob in list)
            {
                probabilities.Add(new ShannonFanoCoding { Name = "x" + index++, Probability = prob });
            }

            probabilities.Sort((x, y) => y.Probability.CompareTo(x.Probability));

            Divide(probabilities, 0, probabilities.Count - 1);
            
            Console.WriteLine("Кодування методикою Шеннона-Фано:");
            
            Console.WriteLine("Symbol\tProbability\tCode");
            foreach (var symbolProbability in probabilities.OrderBy(e => e.Name))
            {
                Console.WriteLine($"{symbolProbability.Name}\t{symbolProbability.Probability}\t\t{symbolProbability.Code}");
            }
        }
        static void Divide(List<ShannonFanoCoding> probabilities, int start, int end)
        {
            if (start >= end) return;

            double totalProbability = 0;
            for (int i = start; i <= end; i++)
            {
                totalProbability += probabilities[i].Probability;
            }

            double cumulativeProbability = 0;
            int splitIndex = -1;
            double minDifference = double.MaxValue;

            for (int i = start; i <= end; i++)
            {
                cumulativeProbability += probabilities[i].Probability;
                double difference = Math.Abs(cumulativeProbability - totalProbability / 2);
                if (difference < minDifference)
                {
                    splitIndex = i;
                    minDifference = difference;
                }
            }

            for (int i = start; i <= splitIndex; i++)
            {
                probabilities[i].Code += "0";
            }

            for (int i = splitIndex + 1; i <= end; i++)
            {
                probabilities[i].Code += "1";
            }


            Divide(probabilities, start, splitIndex);
            Divide(probabilities, splitIndex + 1, end);
        }
    }
}