using System;
using System.Security.Cryptography.X509Certificates;

namespace Sudoku
{
    public class Program
    {
        public static string inputString;
        public static List<int> intList;

        public static void Main(string[] args)
        {
            Console.WriteLine("Enter sudoku:");
            inputString = Console.ReadLine();
            intList = inputString.Split(' ', StringSplitOptions.RemoveEmptyEntries) .Select(int.Parse) .ToList(); 
        }

    }

}
