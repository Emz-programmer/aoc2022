using System;
using System.Text.RegularExpressions;
using System.Numerics;


namespace day11
{
    class Program
    {
        static void Main()
        {
            string path = "../../../input.txt";
            string []input = File.ReadAllText(path).Split("\r\n\r\n");
            
            Monkey [] monkeys = new Monkey[input.Length];
            int index = 0; 

            foreach(string monkey in input)
            {
                string[] monkeyData = monkey.Split("\r\n");

                string startingItems = monkeyData[1].Split(": ")[1];
                string operation = monkeyData[2].Split(": ")[1];
                ulong test = ulong.Parse(monkeyData[3].Split(" ").Last());
                int trueCase = int.Parse(monkeyData[4].Split(" ").Last());
                int falseCase = int.Parse(monkeyData[5].Split(" ").Last());

                monkeys[index] = new Monkey(startingItems,operation,test,trueCase,falseCase);

                index++;
            }


            //Console.WriteLine(Karasuba.fastMultiply(45,20));

            //20 rounds

            for(int i=0; i < 10000; i++)
            {
                if(i%100 == 0)Console.WriteLine("Crunching round: "+i);
                foreach (Monkey monkey in monkeys)
                {
                    while (monkey.HasItems())
                    {
                        BigInteger item = monkey.Operation(monkey.Pop());
                        int targetMonkey = monkey.Test();                        
                        monkeys[targetMonkey].Push(item);
                    }                    
                }
            }
            List<BigInteger> touched = new List<BigInteger>();
            index = 0;
            foreach(Monkey monkey in monkeys)
            {
                touched.Add((ulong)monkey.touched);
                //Console.WriteLine(monkey.touched);
                Console.Write("\nMonkey "+index+": ");
                index++;
                while (monkey.HasItems())
                {
                   Console.Write(monkey.Pop()+", ");
                }
                
                
            }

            touched.Sort();
            touched.Reverse();
            Console.WriteLine("\n-----\n"+touched[0]*touched[1]);
        }
    }

    class Monkey
    {
        string operation;
        public ulong testDivisor;
        BigInteger operationResult = 0;
        Stack<BigInteger> items = new Stack<BigInteger>();
        int trueCase;
        int falseCase;
        public int touched = 0;


        public Monkey(string startingItems, string operation, ulong testDivisor, int trueCase, int falseCase)
        {
            this.operation = operation;
            this.testDivisor = testDivisor;
            this.trueCase = trueCase;
            this.falseCase = falseCase;

            ulong[] tempItems = Array.ConvertAll(startingItems.Split(", "), ulong.Parse);
            
            foreach(ulong item in tempItems.Reverse())
            {
                items.Push(item);
                //touched++;
            }
        }

        public BigInteger Operation(BigInteger old)
        {            
            string []equation = operation.Split("= ");
            equation = equation[1].Split(' ');
            BigInteger[] num = new BigInteger[2];
            int index = 0;
            foreach(string item in equation)
            {
                if (item.Equals("old")) num[index++] = old;
                else if (Regex.IsMatch(item, @"\d+")) num[index++] = Convert.ToUInt64(item);             
                                
            }

            switch (equation[1])
            {
                //case "+": operationResult= num[0]+num[1]; break;
                //case "*": operationResult= num[0]*num[1]; break;
                case "+": operationResult = BigInteger.Add(num[0], num[1]); break;
                case "*":
                    if (num[0] != num[1]) operationResult = BigInteger.Multiply(num[0], num[1]);
                    else operationResult = BigInteger.Pow(num[0], 2) ; 
                    break;
                default: break;
            }
            operationResult %= 9699690;


            return operationResult;
        }

        public int Test()
        {
            //operationResult %= 9699690;
            //Console.WriteLine(operationResult%3 + "," + operationResult% 9699690 %3);
            if (BigInteger.ModPow(operationResult, 9699690, testDivisor) == 0) return trueCase;
            return falseCase;            
        }

        public BigInteger Pop()
        {
            touched++;
            return items.Pop();
        }

        public void Push(BigInteger item)
        {
            items.Push(item% 9699690);
            //touched++;
        }

        public bool HasItems()
        {
            return items.Count > 0;
        }
    }

}