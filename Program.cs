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
        public static string inputString;
        public static List<int> intList;
        public static Sudoku inputSudoku;
        public static Block blocks;
        public static Evaluator evaluator;
        public static int S;
        public static int currentH;


        public static void Main(string[] args)
        {
            
            Console.WriteLine("Enter sudoku:");
            inputString = Console.ReadLine();

            S = 2; // Optimal S value 
            SolveSudokuOnce(inputString, S);
            PrintSudoku(inputSudoku); // See the solution of the sudoku
           
            /* 
            // Analysis to search for the best S value
            var listS1 = new List<(int, double)>();

            for (int S1 = 1; S1 < 11; S1 = S1 + 1)
            {
                double timeS1 = SolveMultipleTimes(inputString, S1);
                listS1.Add((S1, timeS1));
            }
            
            double optimisedS1 = Analysis.GetBestS(listS1);
            Console.WriteLine($"Best S for this puzzle: {optimisedS1}");
            */

            Console.WriteLine("Micheal the rat congratulates you on solving this sudoku!"); // <3
            
        }

        public static double SolveMultipleTimes(string input, int S)
        {
            List<double> times = new List<double>(); // List of all measured times for a certain S value 
            
            // Solve the sudoku # times for a certain S value
            for (int i = 0; i < 10; i++) 
            {
                double t = SolveSudokuOnce(inputString, S);
                times.Add(t);
            }

            // Calculate the mean and standard deviation 
            (double mean, double std) = Analysis.MeanAndStd(times);
            Console.WriteLine($"S = {S} took {mean:F2} +/- {std:F2} ms");
            return mean; 
        }
        public static double SolveSudokuOnce(string input, int S)
        {
            Stopwatch stopwatch = new Stopwatch(); // Start stopwatch
            stopwatch.Start();
            
            // Prepare the sudoku
            intList = input.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            inputSudoku = MakeSudokuFromInput(intList);
            blocks = MakeBlocksFromSudoku(inputSudoku);
            InsertValues(blocks, inputSudoku);
            // Perform local search
            LocalSearch.CheckHValue(S);

            stopwatch.Stop(); // Stop stopwatch

            double time = stopwatch.Elapsed.TotalMilliseconds; // Time in ms
            // Console.WriteLine($"The sudoku has been solved in {time:F0} ms, with S = {S}");

            return time;
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
