using System;
using System.Text.RegularExpressions;

namespace day7
{
    class Program
    {
        static void Main()
        {
            string path = "../../../input.txt";
            string[] input = File.ReadAllText(path).Split("\r\n");
            Dictionary<string, int> fileSizes = getFileSizes(input);
            int sum = 0;
            int updateSize = 30000000;
            int dataToDelete = 70000000;
            int freeSpace = dataToDelete - fileSizes["/"];
            int target = updateSize - freeSpace;

            foreach (var directory in fileSizes.Keys)
            {
                if (fileSizes[directory] <= 100000) sum += fileSizes[directory];
                if (fileSizes[directory] > target && fileSizes[directory] < dataToDelete) dataToDelete = fileSizes[directory];
            }

            Console.WriteLine("Sum of small directories: " + sum);
            Console.WriteLine("Size of deleted file: " + dataToDelete);
        }

        static Dictionary<string, int> getFileSizes(string[] input)
        {
            Dictionary<string, int> fileSizes = new Dictionary<string, int>();
            string currentDirectory = "";

            foreach (string line in input)
            {
                string[] command = line.Split(' ');
                if (command[0].Equals("$") && command[1].Equals("cd"))
                {
                    switch (command[2])
                    {
                        case "..": currentDirectory = Regex.Replace(currentDirectory, @"[a-z]+/$", ""); break;
                        case "/": currentDirectory = "/"; break;
                        default: currentDirectory += command[2] + "/"; break;
                    }
                }
                if (Regex.IsMatch(command[0], @"^\d+$"))
                {
                    string parentDirectory = currentDirectory;
                    int fileSize = Convert.ToInt32(command[0]);

                    while (parentDirectory.Length > 0)
                    {
                        if (fileSizes.ContainsKey(parentDirectory)) fileSizes[parentDirectory] += fileSize;
                        else fileSizes.Add(parentDirectory, fileSize);
                        if (parentDirectory.Equals("/")) break;
                        parentDirectory = Regex.Replace(parentDirectory, @"[a-z]+/$", "");
                    }
                }
            }

            return fileSizes;
        }
    }
}
