using System;

namespace day10
{
    class Program
    {
        static void Main()
        {
            string path = "../../../input.txt";
            string[] input = File.ReadAllText(path).Split("\r\n");
            int x = 1;
            int cycle = 1;
            char[] screen = new char[240];            
            bool latch = true;
            List<int> signals = new List<int>();

            for(int i=0;i<input.Length; i++)
            {
                string line = input[i];               
                signals.Add(cycle*x);
                
                if ((cycle%40) >=x && (cycle%40) <=x+2) screen[cycle-1] = '#';
                else screen[cycle-1]=' ';

                if(!line.Equals("noop"))
                {                 
                    if (latch)
                    {
                        i--;                        
                        latch = false;
                    }
                    else
                    {
                        latch = true;
                        x += Convert.ToInt32(line.Split(' ')[1]);                        
                    }
                }
                cycle++;                
            }
            int sum = 0;
            for(int i=19;i<220; i += 40)
            {
                sum += signals[i];               
            }
            Console.WriteLine("Sum: "+sum);

            for(int i=0;i< screen.Length;i++)
            {
                if (i % 40 == 0) Console.WriteLine();
                Console.Write(screen[i]);
            }
        }
    }
}