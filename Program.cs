using System;
using System.Security.Cryptography.X509Certificates;

namespace Sudoku
{
    public class Program
    {
        public static string inputString;
        public static List<int> intList;
        public static Sudoku inputSudoku;
        public static Block blocks;
        public static Evaluator evaluator = new Evaluator(); // dit staat er nu in om te testen AKA temp

        public static void Main(string[] args)
        {
            Console.WriteLine("Enter sudoku:");
            inputString = Console.ReadLine();

            //save every number as a separate int in the intList
            intList = inputString.Split(' ', StringSplitOptions.RemoveEmptyEntries) .Select(int.Parse) .ToList();

            inputSudoku = MakeSudokuFromInput(intList);
            blocks = MakeBlocksFromSudoku(inputSudoku);
            
            TestVerticalMethod(); // temp
        }

        public static Sudoku MakeSudokuFromInput(List<int> inputList)
        {
            Sudoku sudokuInMaking = new Sudoku(inputList);
            return sudokuInMaking;
        }

        public static Block MakeBlocksFromSudoku(Sudoku sudoku)
        {
            Block block = new Block(sudoku);
            return block;
        }

        public static int[] TestVerticalMethod() // temp
        {
            return evaluator.ReadVertical(inputSudoku, 2);
        }
    }

}
