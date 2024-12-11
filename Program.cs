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
        public static Evaluator evaluator = new Evaluator(); 
        Inserter inserter;

        public static void Main(string[] args)
        {
            Console.WriteLine("Enter sudoku:");
            inputString = Console.ReadLine();

            //save every number as a separate int in the intList
            intList = inputString.Split(' ', StringSplitOptions.RemoveEmptyEntries) .Select(int.Parse) .ToList();

            inputSudoku = MakeSudokuFromInput(intList);
            blocks = MakeBlocksFromSudoku(inputSudoku);

            InsertValues(blocks);

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


        public static void InsertValues(Block block)
        {
            for (int g = 0; g < 3; g++)
            {
                for (int h = 0; h < 3; h++)
                {
                    Inserter.InsertValue(block, g, h);
                }
            }
        }

        

        
    }

}
