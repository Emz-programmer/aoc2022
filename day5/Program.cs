using System;
using System.Text.RegularExpressions;

namespace day5
{
    class Program
    {
        static void Main()
        {
            string path = "../../../input.txt";
            string[] input = File.ReadAllText(path).Split("\r\n\r\n");
            string[] shipYardInput = input[0].Split("\r\n");
            string [] steps = input[1].Split("\r\n");
            
            Stack<char> [] shipYard1 = parseInput(shipYardInput);
            Stack<char> [] shipYard2 = parseInput(shipYardInput);            

            foreach(string step in steps)                 
            {
                var matches = Regex.Matches(step,@"\d+");
                int move = Convert.ToInt32 (matches[0].ToString());
                int from = Convert.ToInt32 (matches[1].ToString())-1;
                int dest = Convert.ToInt32 (matches[2].ToString())-1;
                Stack <char> part2 = new Stack<char>();

                for(int i=0; i< move; i++)
                {                    
                    shipYard1[dest].Push(shipYard1[from].Pop());
                    part2.Push(shipYard2[from].Pop());
                }
                while (part2.Count > 0)
                {
                    shipYard2[dest].Push(part2.Pop());
                }
            }
            
            Console.WriteLine("CrateMover 9000: "+ getFirstCrates(shipYard1));
            Console.WriteLine("CrateMover 9001: "+ getFirstCrates(shipYard2));
        }

        static Stack<char>[] parseInput(string [] inputShipYard)
        {
            Stack<char>[] shipYard = new Stack<char>[(int)Math.Ceiling(inputShipYard[0].Length /4.0)];            
            int index = 0;

            for (int i = 1; i < inputShipYard[0].Length; i += 4)
            {
                Stack<char> crates = new Stack<char>();                

                for (int j = 0; j < inputShipYard.Length - 1; j++)
                {
                    crates.Push(inputShipYard[j].ElementAt(i));                    
                }
                Stack<char> reverse = new Stack<char>();                
                while (crates.Count > 0)
                {
                    if (crates.Peek() != ' ') reverse.Push(crates.Pop());
                    else crates.Pop();                    
                }
                shipYard[index] = reverse;                
                index++;
            }
            return shipYard;
        }

        static string getFirstCrates(Stack<char>[] shipYard)
        {
            string firstCrates = "";
            foreach(Stack<char> crate in shipYard) firstCrates += crate.Pop();
            return firstCrates;
        }
    }
}