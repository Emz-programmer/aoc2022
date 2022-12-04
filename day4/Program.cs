using System;

namespace day4
{
    class Program
    {
        static void Main()
        {
            string[] input = File.ReadAllText("../../../input.txt").Split("\r\n");

            int containedPairs = 0, overlappingPairs = 0;
            
            for (int i=0;i<input.Length;i++)
            {                
                int [] pair1 = Array.ConvertAll(input[i].Split(',')[0].Split('-'), int.Parse);
                int [] pair2 = Array.ConvertAll(input[i].Split(',')[1].Split('-'), int.Parse);

                containedPairs += Convert.ToInt32((pair1[0] <= pair2[0] && pair1[1] >= pair2[1]) | (pair2[0] <= pair1[0] && pair2[1] >= pair1[1]));
                overlappingPairs += Convert.ToInt32(pair1[0] <= pair2[1] && pair2[0] <= pair1[1]);
            }

            Console.WriteLine("Contained Pairs: "+containedPairs);
            Console.WriteLine("Overlapping Pairs: "+overlappingPairs);
        }
    }
}