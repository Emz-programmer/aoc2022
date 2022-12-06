using System;

namespace day6
{
    class Program
    {
        static void Main()
        {
            string path = "../../../input.txt";
            string input = File.ReadAllText(path);

            Console.WriteLine("Start of package: " + getMarker(input, 4));
            Console.WriteLine("Start of message: " + getMarker(input, 14));
        }

        static int getMarker(string datastream, int bufferSize)
        {
            for (int i = 0; i < datastream.Length - bufferSize - 1; i++)
            {
                string buffer = datastream.Substring(i, bufferSize);
                if (isAllUnique(buffer)) return i + bufferSize;
            }
            return 0;
        }

        static bool isAllUnique(string buffer)
        {
            for (int i = 0; i < buffer.Length; i++)
            {
                if (buffer.Substring(i + 1).Contains(buffer[i])) return false;
            }
            return true;
        }
    }
}