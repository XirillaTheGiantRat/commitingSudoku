using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography;

namespace Sudoku
{
    public class LocalSearch
    {
        private static Random random = new Random();
        private static Evaluator evaluator = new Evaluator(Program.inputSudoku);
        private static int currentH;
        private static int N;
        private static readonly int StopCriterium = 9;

        public static void CheckHValue(int S)
        {
            currentH = evaluator.GetSudokuHValue(Program.inputSudoku);
            Console.WriteLine(currentH);
            while (currentH > 0)
            {

                ChooseSwap(S);
            }
        }

        public static void ChooseSwap(int S) 
        {
            if (N < StopCriterium) 
            {

                bool didTheSwap = PerformAllSwaps(Program.blocks, random.Next(3), random.Next(3));
                if (didTheSwap)
                {
                    return;
                }
            }
            else
            {
                // After N unsuccesfull swaps, perform random walk
                RandomWalk(S, Program.blocks);

                // CALCULATE AND UPDATE H VALUE AFTER RANDOM WALK AGAIN
                currentH = evaluator.GetSudokuHValue(Program.inputSudoku);
                N = 0;
            }
        }

        public static void RandomWalk(int n, Block block)
        {
            for (int i = 0; i < n; i++)
            {
                // Locate a random block
                (int, int) randomBlockLoc = (random.Next(3), random.Next(3));
                (int, bool)[,] randomBlock = block.blockIndexes[randomBlockLoc.Item1, randomBlockLoc.Item2];

                // Locate two random cells that have to be swapped 
                (int, int) cell1 = (random.Next(3), random.Next(3));
                (int, int) cell2 = (random.Next(3), random.Next(3));

                // Ensure the cells are not fixated 
                while (randomBlock[cell1.Item1, cell1.Item2].Item2)
                {
                    cell1 = (random.Next(3), random.Next(3));
                }

                while (randomBlock[cell2.Item1, cell2.Item2].Item2)
                {
                    cell2 = (random.Next(3), random.Next(3));
                }

                // Swap and update the sudoku
                SwapCells(block.blockIndexes[randomBlockLoc.Item1, randomBlockLoc.Item2], cell1.Item1, cell1.Item2, cell2.Item1, cell2.Item2);
                Program.MapBlocksToSudoku(block, Program.inputSudoku);
            }
        }

        // Perform all possible swaps within a block
        public static bool PerformAllSwaps(Block block, int blockRow, int blockCol)
        {
            var selectedBlock = block.blockIndexes[blockRow, blockCol];

            // Keep track of swaps with hValueList
            List<(int, int, (int, int), (int, int), (int, int))> hValueList = new List<(int, int, (int, int), (int, int), (int, int))>();

            // Loop through all pairs of cells in the block
            for (int row1 = 0; row1 < 3; row1++) 
            {
                for (int column1 = 0; column1 < 3; column1++) 
                {
                    for (int row2 = row1; row2 < 3; row2++) 
                    {
                        for (int column2 = (row2 == row1) ? column1 + 1 : 0; column2 < 3; column2++)
                        {
                            if (!selectedBlock[row1, column1].Item2 && !selectedBlock[row2, column2].Item2) // Checks if cells are not fixed
                            {

                                // Perform the evaluation before the swap per cell 
                                int h1 = Evaluator.HeuristicFunctionPerRowOrColumn(getSudokuColumnWithChangedValue(Program.inputSudoku, selectedBlock, blockRow, blockCol, column1))
                                    + Evaluator.HeuristicFunctionPerRowOrColumn(getSudokuRowWithChangedValue(Program.inputSudoku, selectedBlock, blockRow, blockCol, row1));
                                int h2 = Evaluator.HeuristicFunctionPerRowOrColumn(getSudokuColumnWithChangedValue(Program.inputSudoku, selectedBlock, blockRow, blockCol, column2))
                                    + Evaluator.HeuristicFunctionPerRowOrColumn(getSudokuRowWithChangedValue(Program.inputSudoku, selectedBlock, blockRow, blockCol, row2));
                                int hBefore = h1 + h2;

                                // Swap the cells 
                                SwapCells(selectedBlock, row1, column1, row2, column2);

                                // Perform the evaluation function per cell after swapping
                                int h3 = Evaluator.HeuristicFunctionPerRowOrColumn(getSudokuColumnWithChangedValue(Program.inputSudoku, selectedBlock, blockRow, blockCol, column1)) 
                                    + Evaluator.HeuristicFunctionPerRowOrColumn(getSudokuRowWithChangedValue(Program.inputSudoku, selectedBlock, blockRow, blockCol, row1));
                                int h4 = Evaluator.HeuristicFunctionPerRowOrColumn(getSudokuColumnWithChangedValue(Program.inputSudoku, selectedBlock, blockRow, blockCol, column2)) 
                                    + Evaluator.HeuristicFunctionPerRowOrColumn(getSudokuRowWithChangedValue(Program.inputSudoku, selectedBlock, blockRow, blockCol, row2));
                                int hAfter = h3 + h4;

                                // Add the neccessarry indexes to the list
                                hValueList.Add((hBefore, hAfter, (blockRow, blockCol), (row1, column1), (row2, column2)));

                                // Swap back so next swap can happen on the original sudoku
                                SwapCells(selectedBlock, row1, column1, row2, column2);

                                

                            }
                        }
                    }
                }
            }

            // Find the swap with the best heuristic value 
            int bestHValue = hValueList.Min(x => x.Item2);
            int index = hValueList.FindIndex(x => x.Item2 == bestHValue);
            int hDifference = hValueList[index].Item1 - bestHValue;

            // Perform the swap if it results in an improvement or equal score 
            if (hDifference > 0)
            {
                // Update the currentH value of the whole sudoku after the swap
                currentH = currentH - hDifference;

                // Swap and update the sudoku
                SwapCells(block.blockIndexes[hValueList[index].Item3.Item1, hValueList[index].Item3.Item2], hValueList[index].Item4.Item1, hValueList[index].Item4.Item2, hValueList[index].Item5.Item1, hValueList[index].Item5.Item2);
                Program.MapBlocksToSudoku(block, Program.inputSudoku);

                N = 0;
                return true;
            }
            else if(hDifference == 0)
            {
                currentH = currentH;
                SwapCells(block.blockIndexes[hValueList[index].Item3.Item1, hValueList[index].Item3.Item2], hValueList[index].Item4.Item1, hValueList[index].Item4.Item2, hValueList[index].Item5.Item1, hValueList[index].Item5.Item2);
                Program.MapBlocksToSudoku(block, Program.inputSudoku);

                N++;
                return true;

            }
            else
            {
                N++;
                return false;
            }

        }

        //dit is de meest cracked code ooit sorry basically maakt het een array aan van de hele sudoku maar dan met de values na de swap
        public static int[] getSudokuRowWithChangedValue(Sudoku sudoku, (int, bool)[,] block, int blockRow, int blockCol, int rowInBlock)
        {
            int[] result = new int[9];
            int x = rowInBlock + (blockRow * 3); 

            int b, e;
            if (blockCol == 0)
            {
                b = 0; e = 2;
            }
            else if (blockCol == 1)
            {
                b = 3; e = 5;
            }
            else
            {
                b = 6; e = 8;
            }

            for (int y = 0; y < 9; y++)
            {
                if (y < b || y > e) 
                {
                    result[y] = sudoku.allIndexes[x, y];
                }
                else 
                {
                    if (y < 3)
                    {
                        result[y] = block[rowInBlock, y].Item1; 
                    }
                    else if (y >= 3 && y < 6)
                    {
                        result[y] = block[rowInBlock, y - 3].Item1; 
                    }
                    else
                    {
                        result[y] = block[rowInBlock, y - 6].Item1; 
                    }
                }
            }
            return result;
        }

        public static int[] getSudokuColumnWithChangedValue(Sudoku sudoku, (int, bool)[,] block, int blockRow, int blockCol, int colInBlock)
        {
            int[] result = new int[9];
            int y = colInBlock + (blockCol * 3); 

            int b, e;
            if (blockRow == 0)
            {
                b = 0; e = 2;
            }
            else if (blockRow == 1)
            {
                b = 3; e = 5;
            }
            else
            {
                b = 6; e = 8;
            }

            for (int x = 0; x < 9; x++) 
            {
                if (x < b || x > e) 
                {
                    result[x] = sudoku.allIndexes[x, y]; 
                }
                else 
                {
                    if (x < 3)
                    {
                        result[x] = block[x, colInBlock].Item1; 
                    }
                    else if (x >= 3 && x < 6)
                    {
                        result[x] = block[x - 3, colInBlock].Item1; 
                    }
                    else
                    {
                        result[x] = block[x - 6, colInBlock].Item1; 
                    }
                }
            }
            return result;
        }

        public static void SwapCells((int, bool)[,] block, int row1, int column1, int row2, int column2)
        {
            // CHeck if the cells are not the same 
            if (row1 != row2 || column1 != column2)
            {
                // Get the values of the cells
                int value1 = block[row1, column1].Item1;
                int value2 = block[row2, column2].Item1;
                // Swap the values of the cells
                block[row1, column1] = (value2, block[row1, column1].Item2);
                block[row2, column2] = (value1, block[row2, column2].Item2);
            }
        }
    }
}
