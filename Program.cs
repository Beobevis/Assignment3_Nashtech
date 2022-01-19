using System.Diagnostics;

namespace Assignment3
{
    static class Program
    {
        delegate bool CheckNumberIsPrime(int number);
        static async Task Main(string[] args)
        {
            int min = 0, max = 100;
            // Basic way to find Prime Numbers
            //Method 1:
            // for (int i = min; i <= max; i++)
            // {
            //     if (IsPrimeNumberMaster(i))
            //     {
            //         Console.Write(i + " ");
            //     }
            // }
            //Method 2:
            // var result = GetPrimeNumbers(min, max,1);
            // Console.WriteLine($"Total Number: {result.Count}");
            // Console.Write("Prime Numbers: ");
            // foreach (var data in result)
            // {
            //     Console.Write(data + " ");
            // }

            //Using Task to improve processing speed by split Task child
            // Task<List<int>>[] tasks = new Task<List<int>>[5];
            // tasks[0] = GetPrimeNumbersAsync(min, 2000000, IsPrimeNumberMaster,1);
            // tasks[1] = GetPrimeNumbersAsync(2000001, 4000000, IsPrimeNumberMaster,2);
            // tasks[2] = GetPrimeNumbersAsync(4000001, 6000000, IsPrimeNumberMaster,3);
            // tasks[3] = GetPrimeNumbersAsync(6000001, 8000000, IsPrimeNumberMaster,4);
            // tasks[4] = GetPrimeNumbersAsync(8000001, max, IsPrimeNumberMaster,5);

            // var result = await Task.WhenAll(tasks);
            // Console.WriteLine($"Total Numbers: {result.Sum(x => x.Count)}");
            // foreach(var list in result){
            //     Console.WriteLine(list.Count);
            // }
            // Using Async to find Prime Numbers 
            // And make Comparison between IsPrimeNumber vs IsPrimeNumberMaster
            //Advanced: Using Stopwatch method to see total elapsed Times
            var results1 = await GetPrimeNumbersAsync(min, max, IsPrimeNumberMaster, 2);
            Console.WriteLine($"Total Number: {results1.Count}");
            Console.Write("Prime Numbers: ");
            foreach (var data in results1)
            {
                Console.Write(data + " ");
            }

            var results2 = await GetPrimeNumbersAsync(min, max, IsPrimeNumber, 3);
            Console.WriteLine($"Total Number: {results2.Count}");
            Console.Write("Prime Numbers: ");
            foreach (var data in results2)
            {
                Console.Write(data + " ");
            }
            Console.WriteLine("\nMain Done!");

        }

        static List<int> GetPrimeNumbers(int min, int max, int index)
        {
            var sw = new Stopwatch();
            sw.Start();
            var result = new List<int>();
            for (int i = min; i <= max; i++)
            {
                if (IsPrimeNumber(i))
                {
                    result.Add(i);
                }
            }
            Console.WriteLine($"\nDone [{index}] in: [{sw.ElapsedMilliseconds}]");
            return result;
        }
        static async Task<List<int>> GetPrimeNumbersAsync(int min, int max, CheckNumberIsPrime check, int index)
        {
            var sw = new Stopwatch();
            sw.Start();
            var list = new List<int>();
            var data = await Task.Factory.StartNew(() =>
            {
                for (int i = min; i <= max; i++)
                {
                    if (check(i))
                    {
                        list.Add(i);
                    }
                }
                return list;
            });
            Console.WriteLine($"\nDone [{index}] in: [{sw.ElapsedMilliseconds}]");
            return data;

        }
        static bool IsPrimeNumber(int number)
        {
            int i;
            for (i = 2; i <= (number - 1); i++)
            {
                if (number % i == 0) return false;
            }
            if (i == number || number >= 2) return true;
            else return false;
        }
        static bool IsPrimeNumberMaster(int number)
        {
            int i;
            var boundary = (int)Math.Floor(Math.Sqrt(number));
            for (i = 2; i <= boundary; i++)
            {
                if (number % i == 0) return false;
            }
            if (i == number || number >= 2) return true;
            else return false;
        }

    }
}