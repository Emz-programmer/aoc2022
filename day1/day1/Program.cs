using System;

namespace day1
{
    class Program
    {
        static void Main()
        {
            string path = "../../../input.txt";
            string[] input = File.ReadAllText(path).Split("\r\n\r\n");
            int[] elfCalories = new int[input.Length];
            
            for (int i = 0; i < input.Length; i++)
            {
                int[] tempCalories = Array.ConvertAll(input[i].Split("\r\n"), int.Parse);
                elfCalories[i] = tempCalories.Sum();
            }

            Array.Sort(elfCalories);
            Array.Reverse(elfCalories);

            Console.WriteLine("Biggest Value: " + elfCalories[0]);
            Console.WriteLine("Sum of top 3: " + elfCalories.Take(3).Sum());
        }
    }
}