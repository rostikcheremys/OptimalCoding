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

            CalculateCodes(nodes[0], "", dict, ref index);

            Console.WriteLine("\nКодування методикою Хаффмена:");
            Console.WriteLine("Symbol\tProbability\tCode");

            foreach (var kvp in dict)
            {
                Console.WriteLine($"x{kvp.Key}\t{list[int.Parse(kvp.Key) - 1]}\t\t{kvp.Value}");
            }
        }

        private void CalculateCodes(Huffman node, string code, Dictionary<string, string> dict, ref int index)
        {
            if (node.Left == null && node.Right == null) dict[index++.ToString()] = code;
            else
            {
                if (node.Left != null) CalculateCodes(node.Left, code + "0", dict, ref index);
                if (node.Right != null) CalculateCodes(node.Right, code + "1", dict, ref index);
            }
        }
    }
}