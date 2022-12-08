using System;

namespace day8
{
    class Program
    {
        static void Main()
        {
            string path = "../../../input.txt";
            string[] input = File.ReadAllText(path).Split("\r\n");



            int[,] forest = new int[input.Length, input[0].Length];

            for (int i = 0; i < input.Length; i++)
            {
                char[] line = input[i].ToCharArray();
                //Console.WriteLine();

                for (int j = 0; j < line.Length; j++)
                {
                    forest[i, j] = Convert.ToInt32(line[j]);
                    //Console.Write(line[j]);
                }
            }

            int visibleTrees = (input.Length + input[0].Length - 2) * 2;

            int scenicScore = 0;

            //tree logic

            for (int i = 1; i < input.Length - 1; i++)
            {
                for (int j = 1; j < input[i].Length - 1; j++)
                {
                    //Console.WriteLine(i + "," + j);
                    if (isVisible(i, j, forest))
                    {
                        visibleTrees++;
                        if(getScenicScore(i,j,forest)>scenicScore) scenicScore = getScenicScore(i,j,forest);
                        //Console.WriteLine(i + "," + j);
                    }

                }

            }

            Console.WriteLine(visibleTrees);
            Console.WriteLine(scenicScore);
        }

        static bool isVisible(int x, int y, int[,] forest)
        {
            int currentTree = forest[x, y];

            List<int> left = new List<int>();
            List<int> right = new List<int>();
            List<int> up = new List<int>();
            List<int> down = new List<int>();

            
            for(int i = 0; i < y; i++) left.Add(forest[x, i]);        

            for(int i=y+1; i<forest.GetLength(1);i++) right.Add(forest[x, i]);

            for (int i = 0; i < x; i++) up.Add(forest[i, y]);

            for (int i = x + 1; i < forest.GetLength(0); i++) down.Add(forest[i, y]);

            if (left.Max() < currentTree) return true;
            if (right.Max() < currentTree) return true;
            if (up.Max() < currentTree) return true;
            if (down.Max() < currentTree) return true;

            return false;
        }

        static int getScenicScore(int x, int y, int [,] forest)
        {
            //Amount of trees that can be seen from x,y

            int currentTree = forest[x, y];
            

            int leftScore = 0;
            int rightScore = 0;
            int upScore = 0;
            int downScore = 0;

            List<int> left = new List<int>();
            List<int> right = new List<int>();
            List<int> up = new List<int>();
            List<int> down = new List<int>();

            for (int i = 0; i < y; i++) left.Add(forest[x, i]);
            for (int i = y + 1; i < forest.GetLength(1); i++) right.Add(forest[x, i]);
            for (int i = 0; i < x; i++) up.Add(forest[i, y]);
            for (int i = x + 1; i < forest.GetLength(0); i++) down.Add(forest[i, y]);
            //left
            for(int i = left.Count - 1; i >= 0; i--)
            {
                if (left[i] < currentTree) leftScore++;
                else
                {
                    leftScore++;
                    break;
                }
            }
            for(int i=0; i< right.Count; i++)
            {
                if (right[i] < currentTree) rightScore++;
                else
                {
                    rightScore++;
                    break;
                }
            }
            for(int i=up.Count - 1;i >= 0; i--)
            {
                if (up[i] < currentTree) upScore++;
                else
                {
                    upScore++;
                    break;
                }
            }
            for(int i=0; i < down.Count; i++)
            {
                if (down[i] < currentTree) downScore++;
                else
                {
                    downScore++;
                    break;
                }
            }



            return leftScore*rightScore*upScore*downScore;
        }

        

    }
}