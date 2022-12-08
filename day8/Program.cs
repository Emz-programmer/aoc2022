using System;

namespace day8
{
    class Program
    {
        static void Main()
        {
            string path = "../../../input.txt";
            string[] input = File.ReadAllText(path).Split("\r\n");
            int visibleTrees = (input.Length + input[0].Length - 2) * 2;
            int bestScenicScore = 0;
            int[,] forest = new int[input.Length, input[0].Length];

            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    forest[i, j] = Convert.ToInt32(input[i][j]);
                }
            }

            for (int i = 1; i < input.Length - 1; i++)
            {
                for (int j = 1; j < input[i].Length - 1; j++)
                {
                    int scenicScore = getScenicScore(i, j, forest);
                    if (isVisible(i, j, forest))
                    {
                        visibleTrees++;
                        if (scenicScore > bestScenicScore) bestScenicScore = scenicScore;
                    }
                }
            }

            Console.WriteLine("Visible trees: " + visibleTrees);
            Console.WriteLine("Scenic score:  " + bestScenicScore);
        }

        static bool isVisible(int x, int y, int[,] forest)
        {
            int currentTree = forest[x, y];
            List<int>[] directions = getDirections(x, y, forest);

            foreach (List<int> direction in directions)
            {
                if (direction.Max() < currentTree) return true;
            }

            return false;
        }

        static int getScenicScore(int x, int y, int[,] forest)
        {
            int[] scenicScores = { 0, 0, 0, 0 };
            int currentTree = forest[x, y];
            List<int>[] directions = getDirections(x, y, forest);

            for (int i = 0; i < scenicScores.Length; i++)
            {
                for (int j = 0; j < directions[i].Count; j++)
                {
                    scenicScores[i]++;
                    if (directions[i][j] >= currentTree) break;
                }
            }

            return scenicScores.Aggregate((val1, val2) => val1 * val2);
        }

        static List<int>[] getDirections(int x, int y, int[,] forest)
        {
            List<int>[] directions = new List<int>[4];

            for (int i = 0; i < directions.Length; i++)
            {
                directions[i] = new List<int>();
            }

            for (int i = 0; i < y; i++) directions[0].Add(forest[x, i]); // Left
            for (int i = 0; i < x; i++) directions[1].Add(forest[i, y]); // Up
            for (int i = y + 1; i < forest.GetLength(1); i++) directions[2].Add(forest[x, i]); // Right
            for (int i = x + 1; i < forest.GetLength(0); i++) directions[3].Add(forest[i, y]); // Down

            directions[0].Reverse();
            directions[1].Reverse();
            return directions;
        }
    }
}