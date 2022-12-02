using System;

namespace day2
{
    class Program
    {
        static void Main()
        {
            string path = "../../../input.txt";
            string[] input = File.ReadAllText(path).Split("\r\n");
            int score = 0;
            int newScore = 0;

            foreach(string match in input)
            {
                score += rockPaperScissors(match);
                newScore += rockPaperScissorsNewRules(match);
            }

            Console.WriteLine("Using (X,Y,Z == R,P,S) rules: "+score);
            Console.WriteLine("Using (X,Y,Z == L,W,D) rules: "+newScore);
        }

        static int rockPaperScissors(string match)
        {
           string [] playerMove = match.Split(" ");

           switch (playerMove[0])
            {
                case "A":
                    switch (playerMove[1])
                    {
                        case "Y": return 6+2;
                        case "Z": return 0+3;
                        case "X": return 3+1;
                        default: break;
                    } break;
                case "B":
                    switch (playerMove[1])
                    {
                        case "Z": return 6+3;
                        case "X": return 0+1;
                        case "Y": return 3+2;
                        default: break;
                    }
                    break;
                case "C":
                    switch (playerMove[1])
                    {
                        case "X": return 6+1;
                        case "Y": return 0+2;
                        case "Z": return 3+3;
                        default: break;
                    }
                    break;

                    default: break;
            }

            return 0;
        }

        static int rockPaperScissorsNewRules(string match)
        { 
            string[] gameLogic = match.Split(" ");

            switch (gameLogic[0])
            {
                case "A":
                    switch (gameLogic[1])
                    {
                        case "X": return 0+3;
                        case "Y": return 3+1;
                        case "Z": return 6+2;
                        default: break;
                    }
                    break;
                case "B":
                    switch (gameLogic[1])
                    {
                        case "X": return 0+1;
                        case "Y": return 3+2;
                        case "Z": return 6+3;
                        default: break;
                    }
                    break;
                case "C":
                    switch (gameLogic[1])
                    {
                        case "X": return 0+2;
                        case "Y": return 3+3;
                        case "Z": return 6+1;
                        default: break;
                    }
                    break;

                default: break;
            }

            return 0;
        }
    }
}