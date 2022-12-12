using System;

namespace day12
{
    class Program
    {
        static void Main()
        {
            string path = "../../../input.txt";
            string [] input = File.ReadAllText(path).Split("\r\n");

            Mountain myMountain = new Mountain(input);
            myMountain.Test();
            //Console.WriteLine("Best Path: "+myMountain.Navigate());

        }
    }

    class Mountain
    {
        int[][] map;

        public Mountain(string [] input)
        {            
            //map = input.Select(x => x.ToArray()).ToArray();
            map = new int[input.Length][];

            for (int i = 0; i < input.Length; i++)
            {
                int []line = new int[input[i].Length];
                for(int j = 0; j < input[i].Length; j++)
                {
                    line[j] = input[i][j]-'a';
                    if(input[i][j] == 'S')
                    {
                        line[j] = 0;
                    }
                    if (input[i][j] == 'E')
                    {
                        line[j] = 25;
                    }
                }
                map[i] = line;
            }


        }

        public void Test()
        {
            for(int i = 0;i < map.Length; i++)
            {
                Console.WriteLine();
                for(int j = 0;j < map[i].Length; j++)
                {
                    Console.Write(map[i][j]+"|");
                }
            }
            Console.WriteLine("\n------");

        }

        private int [] getStart()
        {
            for(int i = 0; i < map.Length; i++)
            {
                for(int j=0;j < map[i].Length; j++)
                {
                    if(map[i][j] == 'S') return new int [] { i,j};
                }
            }
            return null;
        }

        private int[] getEnd()
        {
            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                    if (map[i][j] == 'E') return new int[] { i, j };
                }
            }
            return null;
        }

        private bool isValidDirection(char direction, int x, int y)
        {
            int newX = x;
            int newY = y;
            switch (direction)
            {
                case 'L': newX--; break;
                case 'R': newX++; break;
                case 'U': newY--; break;
                case 'D': newY++; break;
                default: break;
            }


            if (newX < 0 | newY < 0 || newX > map[0].Length - 1 | newY > map.Length - 1)
            {
               /* Console.WriteLine(newX + "," + newY);
                Console.WriteLine(map[0].Length + "," + map.Length);
                Console.WriteLine("-");*/
                return false;
            }
            if (map[newY][newX] == 'E' && map[y][x] == 'z') return true;
            if (map[newY][newX] == 'a' && map[y][x] == 'S') return true;
            if(map[newY][newX] == 'S') return false;
            //Console.WriteLine(map[newY][newX]-map[y][x]);
            return map[newY][newX] - map[y][x]<=1 && map[newY][newX] - map[y][x] >= 0;
        }

        public int Navigate()
        {
            int bestPath = map.Length*map[0].Length;
            Console.WriteLine("max path: "+bestPath);
            int last = 5;
            int [] start = getStart();
            int [] end = getEnd();
            char[] directions = "LRUD".ToCharArray();
            Random rng = new Random();

            for (int i = 0;i < 10000; i++)
            {
                int x = start[1];
                int y = start[0];
                int currentPath = 0;

                while(x!=end[1] | y != end[0])
                {
                    int select = rng.Next(4);
                    bool isValid = isValidDirection(directions[select], x, y);
                    bool flag = false;
                    while (select == last | !isValid)
                    {
                        if (flag) 
                        {
                            Console.WriteLine($"{y},{x}: direction: {directions[last]}, {currentPath}");                            
                            currentPath = 99999;
                            break;
                        }
                        //Console.WriteLine(select);
                        select = 0;                        
                        isValid = isValidDirection(directions[select], x, y);
                        if (isValid) break;
                        select = 1;
                        isValid = isValidDirection(directions[select], x, y);
                        if (isValid) break;
                        select = 2;
                        isValid = isValidDirection(directions[select], x, y);
                        if (isValid) break;
                        select = 3;
                        isValid = isValidDirection(directions[select], x, y);
                        if (isValid) break;

                        flag = true;
                    }

                    
                    char direction = directions[select];

                    switch (direction)
                    {
                        case 'L': x--; last = 1; break;
                        case 'R': x++; last = 0; break;
                        case 'U': y--; last = 3; break;
                        case 'D': y++; last = 2; break;
                        default: break;
                    }
                    //Console.WriteLine("Moved: "+direction);
                    //Console.WriteLine(y + "," + x);
                    if (currentPath >= bestPath) break;
                    currentPath++;
                }
                Console.WriteLine("Finished: "+i+" Best Path: "+bestPath);
                
                if(currentPath < bestPath) bestPath = currentPath;
            }

            //Console.WriteLine(start[0]+","+start[1]);
            //Console.WriteLine(end[0] + "," + end[1]);
            return bestPath;
        }
    }
}