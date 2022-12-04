using System;

namespace day4
{
    class Program
    {
        static void Main()
        {
            string path = "../../../input.txt";
            string[] input = File.ReadAllText(path).Split("\r\n");

            int containedPairs = 0;
            int overlappingPairs = 0;

            foreach(string pair in input)
            {
                string pair1 = pair.Split(',')[0];
                string pair2 = pair.Split(',')[1];

                containedPairs += checkPairs(pair1, pair2, false);
                overlappingPairs += checkPairs(pair1, pair2, true);
            }

            Console.WriteLine(containedPairs);  
            Console.WriteLine(overlappingPairs);    
        }

        static int checkPairs(string pair1, string pair2, bool overlap)
        {
            int pair1Start = Int32.Parse(pair1.Split('-')[0]);
            int pair1End = Int32.Parse(pair1.Split('-')[1]);
            int pair2Start = Int32.Parse(pair2.Split('-')[0]);
            int pair2End = Int32.Parse(pair2.Split('-')[1]);

            if (pair1Start <= pair2Start && pair1End >= pair2End) return 1;
            if (pair2Start <= pair1Start && pair2End >= pair1End) return 1;
            if (overlap && pair1Start >= pair2Start && pair1Start <= pair2End) return 1;
            if (overlap && pair2Start >= pair1Start && pair2Start <= pair1End) return 1;

            return 0;
        }
    }    
}