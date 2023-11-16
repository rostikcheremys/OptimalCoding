using System;
using System.Linq;
using System.Collections.Generic;

namespace Program
{
    public class ShannonFano
    {
        private string Name { get; set; }
        private double Probability { get; set; }
        private string Code { get; set; }
        
        public void TaskOfSecond(List<double> list)
        {
            // Створюємо список
            var probabilities = new List<ShannonFano>();
            int index = 1;
            
            // Створюємо об'єкти ShannonFano з ймовірностями зі вхідного списку
            foreach (double prob in list)
            {
                probabilities.Add(new ShannonFano { Name = "x" + index++, Probability = prob });
            }

            // Відсортовуємо ймовірності в порядку незростання
            probabilities.Sort((x, y) => x.Probability.CompareTo(y.Probability));

            // Розділюємо ймовірності за методикою Шеннона-Фано
            Divide(probabilities, 0, probabilities.Count - 1);
            
            Console.WriteLine("\nКодування методикою Шеннона-Фано:");
            
            Console.WriteLine("Символ\tЙмовірність\tКод");
            
            // Виводимо символи, ймовірності та коди Шеннона-Фано
            foreach (var symbol in probabilities.OrderBy(e => e.Name))
            {
                Console.WriteLine($"{symbol.Name}\t{symbol.Probability}\t\t{symbol.Code}");
            }
        }

        // Рекурсивна функція для розділення ймовірностей Шеннона-Фано
        static void Divide(List<ShannonFano> probabilities, int start, int end)
        {
            if (start >= end) return;

            double totalProbability = 0;
            
            // Знайдемо загальну ймовірність для вказаного діапазону
            for (int i = start; i <= end; i++)
            {
                totalProbability += probabilities[i].Probability;
            }

            int index = -1;
            
            double probability = 0;
            double minDifference = double.MaxValue;

            // Знайдемо індекс, який розділить ймовірності до половини загальної ймовірності
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

            // Додаємо '0' до коду Шеннона-Фано для першої частини ймовірностей та '1' для другої частини
            for (int i = start; i <= index; i++)
            {
                probabilities[i].Code += "0";
            }

            for (int i = index + 1; i <= end; i++)
            {
                probabilities[i].Code += "1";
            }
            
            // Рекурсивно розділюємо обидві частини
            Divide(probabilities, start, index);
            Divide(probabilities, index + 1, end);
        }
    }
}
