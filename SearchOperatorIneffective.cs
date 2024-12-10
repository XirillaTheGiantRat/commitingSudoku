using System;
using System.Collections.Generic;

namespace Sudoku
{
    /* Oke dit is hoe ik eerst PerformAllSwaps deed maar dit is minder effectief aangezien ik eerst alle pairs zocht
     * Dat zijn like n(n-1)/2 swaps, met n = 9 36 swaps per blok in totaal
     * Pas bij het swappen zelf ging ik checken of de swap wel mogelijk is.
     * 
     * Bij de normale SearchOperator exclude ik sws de cellen die fixated zijn en daarna ga ik naar alle mogelijke pairs kijken
     * 
     * Handig om deze code voor nu te bewaren, want dan kan ik er ook over yappen in het verslag  
     * - 
     */

    public class SearchOperatorIneffective 
    {
        private Block block;
        private static Random random = new Random();
        public SearchOperatorIneffective(Block block)
        {
            this.block = block;
        }

        // Swap swap two cells within a block
        public static void PerformAllSwaps(Block block)
        {

            // Randomly select a block
            int blockRow = random.Next(3);
            int blockCol = random.Next(3);
            var selectedBlock = block.blockIndexes[blockRow, blockCol];

            // Loop through all pairs of cells in the block
            for (int row1 = 0; row1 < 3; row1++)
            {
                for (int column1 = 0; column1 < 3; column1++)
                {
                    for (int row2 = row1; row2 < 3; row2++)
                    {
                        for (int column2 = (row2 == row1) ? column1 + 1 : 0; column2 < 3; column2++)
                        {
                            /* Verwijder later, maar even handig om te begrijpen hoe dit werkt
                             * if row2 == row1, column1 + 1, else 0 (start from a new row)
                             * dus als je beide cellen in dezelfde rij zitten, dan wordt je kolomwaarde die van je eerste cel +1 
                             * anders skip je naar de volgende rij en zet je je kolomwaarde weer naar 0 
                            */

                            // Perform the swap for each unique pair of cells
                            SwapCells(selectedBlock, row1, column1, row2, column2);
                        }
                    }
                }
            }


        }

        public static void SwapCells((int, bool)[,] block, int row1, int column1, int row2, int column2)
        {
            // Check if cells are not the same and not fixated
            if ((row1 != row2 || column1 != column2) && !block[row1, column1].Item2 && !block[row2, column2].Item2)
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
}
