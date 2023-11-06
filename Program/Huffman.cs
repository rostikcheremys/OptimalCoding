using System;
using System.Linq;
using System.Collections.Generic;

namespace Program
{
    public class Huffman
    {
        private double Probability { get; set; }
        private Huffman Left { get; set; }
        private Huffman Right { get; set; }

        public void TaskOfFirst(Dictionary<string, string> dict, List<double> list)
        {
            var nodes = list.Select(n => new Huffman { Probability = n }).ToList();

            while (nodes.Count > 1)
            {
                nodes.Sort((x, y) => x.Probability.CompareTo(y.Probability));

                var newNode = new Huffman
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

            var probabilities = dict.Select(kv => new { Name = kv.Key, Probability = list[int.Parse(kv.Key.Substring(1)) - 1], Code = kv.Value }).ToList();
            probabilities.Sort((x, y) => x.Name.CompareTo(y.Name));

            Console.WriteLine("\nКодування методикою Хаффмена:");
            Console.WriteLine("Symbol\tProbability\tCode");

            foreach (var symbol in probabilities)
            {
                Console.WriteLine($"{symbol.Name}\t{symbol.Probability}\t\t{symbol.Code}");
            }

            void GetCodes(Huffman node, string code)
            {
                if (node.Left == null && node.Right == null) dict["x" + index++] = code;
                else
                {
                    if (node.Left != null) GetCodes(node.Left, code + "0");
                    if (node.Right != null) GetCodes(node.Right, code + "1");
                }
            }
        }
    }
}