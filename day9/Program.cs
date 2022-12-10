using System;
namespace day9
{
    class Program
    {
        static void Main()
        {
            string path = "../../../input.txt";
            string[] input = File.ReadAllText(path).Split("\r\n");
            int tailX = 25;
            int tailY = 25;
            int count = 0;
            int headX = 25;
            int headY = 25;

            char[,] grid = new char[50, 50];
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    grid[i, j] = '.';
                }
            }

            grid[tailX, tailY] = '#';

            foreach (string line in input)
            {
                string[] instructions = line.Split(" ");
                int steps = Convert.ToInt32(instructions[1]);

                for (int i = 0; i < steps; i++)
                {
                    int lastHeadX = headX;
                    int lastHeadY = headY;

                    switch (instructions[0])
                    {
                        case "U": headY++; break;
                        case "D": headY--; break;
                        case "R": headX++; break;
                        case "L": headX--; break;
                        default: break;
                    }

                    if (Math.Abs(tailX - headX) > 1 | Math.Abs(tailY - headY) > 1)
                    {
                        tailX = lastHeadX;
                        tailY = lastHeadY;
                    }
                    grid[tailY, tailX] = '#';
                }
            }


            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] == '#') count++;
                    grid[i, j] = '.';
                }
            }

            Console.WriteLine(count);

            headX = 25;
            headY = 25;
            tailX = 25;
            tailY = 25;

            int[] tailsX = new int[11];
            int[] tailsY = new int[11];




            for (int i = 0; i < 11; i++)
            {
                tailsX[i] = 25;
                tailsY[i] = 25;
            }



            foreach (string line in input)
            {
                string[] instructions = line.Split(" ");
                int steps = Convert.ToInt32(instructions[1]);
                Console.WriteLine(line);
                for (int i = 0; i < steps; i++)
                {
                    //prev head
                    tailsX[0] = headX;
                    tailsY[0] = headY;


                    switch (instructions[0])
                    {
                        case "U": headY++; break;
                        case "D": headY--; break;
                        case "R": headX++; break;
                        case "L": headX--; break;
                        default: break;
                    }

                    //Current Head
                    tailsX[10] = headX;
                    tailsY[10] = headY;
                    // Compute tails
                    for (int j = 1; j < 10; j++)
                    {
                        //Console.WriteLine(Math.Abs(tailsX[j] - tailsX[10])+","+ Math.Abs(tailsY[j] - tailsY[10]));

                        if (Math.Abs(tailsX[j] - tailsX[10]) > 1)
                        {

                            //Compare with current head
                            //if tail=head > 1: tail = lastHead, currentHead=Tail
                            int tempX = tailsX[j];
                            //int tempY = tailsY[j];
                            //set tail to previous head
                            tailsX[j] = tailsX[0];
                            //tailsY[j] = tailsY[0];
                            //update new head                            

                            /*

                            if (j < 9 && Math.Abs(tailsX[j] - tailsX[j + 1]) > 1 | Math.Abs(tailsY[j] - tailsY[j + 1]) > 1)
                            {
                                //Console.WriteLine(tailsX[j] - tailsX[j - 1]);
                                if (tailsX[j] - tailsX[j + 1] + 1 == tailsX[j] - tailsX[j - 1])
                                {
                                    tailsX[j]++;
                                    Console.WriteLine("Wiggle right ");

                                }
                                else if (Math.Abs(tailsX[j] - tailsX[j + 1] - 1) == 1 && Math.Abs(tailsX[10] - tailsX[j - 1] - 1) == 1)
                                { 
                                    tailsX[j]--;
                                    Console.WriteLine("Wiggle left ");
                                }

                                else if (tailsY[j] - tailsY[j + 1] + 1 == tailsY[j] - tailsY[j - 1])
                                {
                                    tailsY[j]++;
                                    Console.WriteLine("Wiggle up ");

                                }
                                else if (tailsY[j] - tailsY[j + 1] - 1 == tailsY[j] - tailsY[j - 1])
                                {
                                    tailsY[j]--;
                                    Console.WriteLine("Wiggle down " + j);
                                }
                                //can wiggle left or right


                            }*/

                            //update current "head"
                            tailsX[10] = tailsX[j];
                            //tailsY[10] = tailsY[j];

                            tailsX[0] = tempX;
                            //tailsY[0] = tempY;

                            //Console.WriteLine(Math.Abs(tailsX[0] - tailsX[j + 1]) + "," + Math.Abs(tailsY[0] - tailsY[j + 1]));

                            //Console.WriteLine("Moving: " + j);

                            //grid[tailsY[j], tailsX[j]] = j.ToString()[0];
                        }
                        if (Math.Abs(tailsY[j] - tailsY[10]) > 1)
                        {
                            //int tempX = tailsX[j];
                            int tempY = tailsY[j];
                            //set tail to previous head
                            tailsY[j] = tailsY[0];
                            //update current "head"
                            //tailsX[10] = tailsX[j];
                            tailsY[10] = tailsY[j];

                            //tailsX[0] = tempX;
                            tailsY[0] = tempY;
                        }
                    }

                    Console.WriteLine("-----");

                    grid[tailsY[9], tailsX[9]] = '#';
                }





            }

            count = 0;

            grid[25, 25] = 'S';

            for (int k = grid.GetLength(0)-1; k >= 0; k--)
            {
                Console.WriteLine();
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[k, j] == '#') count++;
                    Console.Write(grid[k, j]);

                }
            }

            Console.WriteLine("\n"+count);
        }
    }
}