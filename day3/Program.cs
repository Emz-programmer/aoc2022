using System;

namespace day3
{
    class Program
    {
        static void Main()
        {
            string path = "../../../input.txt";
            string[] input = File.ReadAllText(path).Split("\r\n");

            string[,] compartment = new string[input.Length,2];

            for (int i = 0;i < input.Length;i++)
            {
                string bag = input[i];
                compartment [i,0] = bag.Substring(0, bag.Length / 2);
                compartment [i,1] = bag.Substring(bag.Length / 2);
            }

            int priorityScore = 0;

            for (int i = 0;i < input.Length; i++)
            {
                priorityScore += getPriority(compartment[i,0], compartment[i,1]);
            }

            Console.WriteLine("Bag Priority Score: "+priorityScore);

            int groupScore = 0;
            var elfCarriers = new Stack<string>(input);

            while(elfCarriers.Count > 0)
            {
                string elf1 = elfCarriers.Pop();
                string elf2 = elfCarriers.Pop();
                string elf3 = elfCarriers.Pop();

                groupScore += getPriority(elf1, elf2, elf3);
            }

            Console.WriteLine("Elf Group Score: "+groupScore);
        }

        static int getPriority(string compartment1, string compartment2)
        {
            char[] priority = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            for(int i = 0; i < priority.Length; i++)
            {
                if (compartment1.Contains(priority[i]) && compartment2.Contains(priority[i])) return i + 1;
            }
            return 0;
        }

        static int getPriority(string elf1, string elf2, string elf3)
        {
            char[] priority = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            for (int i = 0; i < priority.Length; i++)
            {
                if (elf1.Contains(priority[i]) && elf2.Contains(priority[i]) && elf3.Contains(priority[i])) return i + 1;
            }
            return 0;
        }
    }
}