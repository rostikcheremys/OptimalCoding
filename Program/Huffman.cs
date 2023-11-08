using System;
using System.Linq;
using System.Collections.Generic;

namespace Program
{
    public class Huffman
    {
        private double Probability { get; set; }
        private Huffman LeftSide { get; set; }
        private Huffman RightSide { get; set; }
        
        public void TaskOfFirst(Dictionary<string, string> dict, List<double> list)
        {
            // Створюємо вузли Хаффмена з ймовірностями зі вхідного списку
            var nodes = list.Select(n => new Huffman { Probability = n }).ToList();

            // Будуємо дерево Хаффмена
            while (nodes.Count > 1)
            {
                // Відсортовуємо ймовірності в порядку зростання
                nodes.Sort((x, y) => x.Probability.CompareTo(y.Probability));

                // Створюємо новий вузол, об'єднавши два вузли з найменшими ймовірностями
                var newNode = new Huffman
                {
                    Probability = nodes[0].Probability + nodes[1].Probability,
                    LeftSide = nodes[0],
                    RightSide = nodes[1]
                };

                // Видаляємо два вузли з найменшими ймовірностями та додаємо новий вузол
                nodes.RemoveRange(0, 2);
                nodes.Add(newNode);
            }

            int index = 1;

            // Рекурсивно призначаємо коди Хаффмена символам та зберігаємо їх у словнику
            CalculateCodes(nodes[0], "");

            // Створюємо список символів із їх ймовірностями та кодами Хаффмена
            var probabilities = dict.Select(kv => new { Name = kv.Key, Probability = list[int.Parse(kv.Key.Substring(1)) - 1], Code = kv.Value }).ToList();

            // Відсортуємо символи за їх назвами
            probabilities.Sort((x, y) => x.Name.CompareTo(y.Name));

            Console.WriteLine("\nКодування методикою Хаффмена:");
            Console.WriteLine("Символ\tЙмовірність\tКод");

            // Виводимо символи, ймовірності та коди Хаффмена
            foreach (var symbol in probabilities)
            {
                Console.WriteLine($"{symbol.Name}\t{symbol.Probability}\t\t{symbol.Code}");
            }

            // Рекурсивна функція для призначення кодів Хаффмена вузлам
            void CalculateCodes(Huffman node, string code)
            {
                // Якщо досягли останнього вузла, зберігаємо код Хаффмена у словнику за ключем
                if (node.LeftSide == null && node.RightSide == null) dict["x" + index++] = code;
                else
                {
                    // Пройдем лівого нащадка з доданим '0' та правого нащадка з доданим '1'
                    if (node.LeftSide != null) CalculateCodes(node.LeftSide, code + "0");
                    if (node.RightSide != null) CalculateCodes(node.RightSide, code + "1");
                }
            }
        }
    }
}
