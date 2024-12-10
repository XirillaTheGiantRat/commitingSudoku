using System;
using System.Collections.Generic;

namespace Sudoku
{
    public class SearchOperator
    {
        private Block block;
        private static Random random = new Random();
        public SearchOperator(Block block)
        {
            this.block = block;
        }

        // Swap two cells within a block
        public static void PerformAllSwaps(Block block)
        {

            // Randomly select a block
            int blockRow = random.Next(3);
            int blockCol = random.Next(3);
            var selectedBlock = block.blockIndexes[blockRow, blockCol];

            List<(int, int)> nonFixatedCells = GetNonFixatedCells(selectedBlock);

            for (int i = 0; i < nonFixatedCells.Count; i++)
            {
                for (int j = i + 1; j < nonFixatedCells.Count; j++) 
                {
                    var (row1, col1) = nonFixatedCells[i];
                    var (row2, col2) = nonFixatedCells[j];

                    // Perform the swap for each pair of nonfixated cells 
                    SwapCells(selectedBlock, row1, col1, row2, col2);
                }
            }

            // AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
        }
        public static List<(int, int)> GetNonFixatedCells((int, bool)[,] block)
        {
            List<(int, int)> nonFixatedCells = new List<(int, int)>();

            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (!block[row, col].Item2)  // Add the cell to the list if it's not fixated 
                    {
                        nonFixatedCells.Add((row, col));
                    }
                }
            }
            return nonFixatedCells;
        }

        public static void SwapCells((int, bool)[,] block, int row1, int column1, int row2, int column2)
        {
            // Get the values of the cells
            int value1 = block[row1, column2].Item1;
            int value2 = block[row2, column2].Item1;

            // Swap the values of the cells
            block[row1, column1] = (value2, block[row1, column1].Item2);
            block[row2, column2] = (value1, block[row2, column2].Item2);

        }
    }
}
