using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using System.ComponentModel;
using static System.Collections.Specialized.BitVector32;

namespace Sudoku
{
    public class Program
    {
        public static string? inputString;
        public static List<int> intList;
        public static Sudoku? inputSudoku;
        public static Block blocks;
        public static Evaluator evaluator;
        public static int S;
        public static void Main(string[] args)
        {

            Console.WriteLine("Enter sudoku:");
            inputString = Console.ReadLine();

            for (int S = 5; S < 50; S += 5)
            {
                Console.WriteLine(inputString); 
                SolveSudokuOnce(inputString, S);
            }
            
            // SolveSudokuOnce(inputString, S);
            // PrintSudoku(inputSudoku); // See the solution of the sudoku
            Console.WriteLine("Micheal the rat congratulates you on solving this sudoku!"); // <3
            
        }

        public static void SolveSudokuOnce(string input, int S)
        {
            Stopwatch stopwatch = new Stopwatch(); // Start stopwatch
            stopwatch.Start();

            intList = input.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

            inputSudoku = MakeSudokuFromInput(intList);
            blocks = MakeBlocksFromSudoku(inputSudoku);

            InsertValues(blocks, inputSudoku);
            LocalSearch.CheckHValue(S);

            stopwatch.Stop(); // Stop stopwatch

            double time = stopwatch.Elapsed.TotalMilliseconds; // Time in ms
            Console.WriteLine($"The sudoku has been solved in {time:F0} ms, with S = {S}");
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
        public static void InsertValues(Block block, Sudoku sudoku)
        {
            for (int g = 0; g < 3; g++)
            {
                for (int h = 0; h < 3; h++)
                {
                    Inserter.InsertValue(block, g, h);
                }
            }
            MapBlocksToSudoku(block, sudoku);
        }

        public static void MapBlocksToSudoku(Block blockObject, Sudoku sudoku)
        {
            for (int blockRow = 0; blockRow < 3; blockRow++)
            {
                for (int blockCol = 0; blockCol < 3; blockCol++)
                {
                    var currentBlock = blockObject.blockIndexes[blockRow, blockCol];

                    for (int localRow = 0; localRow < 3; localRow++)
                    {
                        for (int localCol = 0; localCol < 3; localCol++)
                        {
                            int globalRow = blockRow * 3 + localRow;
                            int globalCol = blockCol * 3 + localCol;

                            sudoku.allIndexes[globalRow, globalCol] = currentBlock[localRow, localCol].cellValue;
                        }
                    }
                }
            }
        }

        public static void PrintSudoku(Sudoku sudoku) // Used to print the output sudoku 
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    Console.Write(sudoku.allIndexes[row, col] + " ");
                }
                Console.WriteLine();
            }
        }




    }

}
