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
        public static Evaluator evaluator;
        Inserter inserter;

        public static void Main(string[] args)
        {
            Console.WriteLine("Enter sudoku:");
            inputString = Console.ReadLine();

            //save every number as a separate int in the intList
            intList = inputString.Split(' ', StringSplitOptions.RemoveEmptyEntries) .Select(int.Parse) .ToList();

            inputSudoku = MakeSudokuFromInput(intList);
            blocks = MakeBlocksFromSudoku(inputSudoku);

            

            InsertValues(blocks, inputSudoku);
            
            SearchOperator.CheckHValue();
            Console.WriteLine("h-value is 0");
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




    }

}
