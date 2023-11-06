﻿using System;
using System.Linq;
using System.Collections.Generic;

namespace Program
{
    public class ShannonFano
    {
        private string Name { get; set; }
        private double Probability { get; set; }
        private string Code { get; set; }
        
        public void TaskOfSecond(Dictionary<string, string> dict, List<double> list)
        {
            var probabilities = new List<ShannonFano>();
            int index = 1;
            
            foreach (double prob in list)
            {
                probabilities.Add(new ShannonFano { Name = "x" + index++, Probability = prob });
            }

            probabilities.Sort((x, y) => y.Probability.CompareTo(x.Probability));

            Divide(probabilities, 0, probabilities.Count - 1);
            
            Console.WriteLine("\nКодування методикою Шеннона-Фано:");
            
            Console.WriteLine("Symbol\tProbability\tCode");
            
            foreach (var symbol in probabilities.OrderBy(e => e.Name))
            {
                Console.WriteLine($"{symbol.Name}\t{symbol.Probability}\t\t{symbol.Code}");
            }
        }
        static void Divide(List<ShannonFano> probabilities, int start, int end)
        {
            if (start >= end) return;

            double totalProbability = 0;
            
            for (int i = start; i <= end; i++)
            {
                totalProbability += probabilities[i].Probability;
            }

            double probability = 0;
            int index = -1;
            double minDifference = double.MaxValue;

            for (int i = start; i <= end; i++)
            {
                probability += probabilities[i].Probability;
                
                double difference = Math.Abs(probability - totalProbability / 2);
                
                if (difference < minDifference)
                {
                    index = i;
                    minDifference = difference;
                }
            }

            for (int i = start; i <= index; i++)
            {
                probabilities[i].Code += "0";
            }

            for (int i = index + 1; i <= end; i++)
            {
                probabilities[i].Code += "1";
            }
            
            Divide(probabilities, start, index);
            Divide(probabilities, index + 1, end);
        }
    }
}