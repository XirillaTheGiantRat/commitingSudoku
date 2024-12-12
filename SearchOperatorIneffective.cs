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

    public class SearchOperator
    {
        private Block block;
        //private static Sudoku sudoku;
        private static Random random = new Random();
        private Evaluator evaluator;
        public SearchOperator(Block block)
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
            List<(int, (int, int), (int, int), (int, int))> hValueList = new List<(int, (int, int), (int, int), (int, int))>();
            //h waarde, (blockrow, blockcol), (x1, y1), (x2, y2)

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

                            // Checks if cells are not fixed
                            if (!selectedBlock[row1, column1].Item2 && !selectedBlock[row2, column2].Item2)
                            {
                                //swap
                                SwapCells(selectedBlock, row1, column1, row2, column2);
                                //add h-value and neccessary indexes to list
                                int h1 = Evaluator.HeuristicFunctionPerRowOrColumn(getSudokuColumnWithChangedValue(Program.inputSudoku, selectedBlock, blockRow, blockCol, column1)) + Evaluator.HeuristicFunctionPerRowOrColumn(getSudokuRowWithChangedValue(Program.inputSudoku, selectedBlock, blockRow, blockCol, row1));
                                int h2 = Evaluator.HeuristicFunctionPerRowOrColumn(getSudokuColumnWithChangedValue(Program.inputSudoku, selectedBlock, blockRow, blockCol, column2)) + Evaluator.HeuristicFunctionPerRowOrColumn(getSudokuRowWithChangedValue(Program.inputSudoku, selectedBlock, blockRow, blockCol, row2));
                                int hValue = h1 + h2;
                                hValueList.Add((hValue, (blockRow, blockCol), (row1, column1), (row2, column2)));
                                //swap back so next swap can happen on the original sudoku
                                SwapCells(selectedBlock, row1, column1, row2, column2);                                
                            }
                        }
                    }
                }
            }
            //find swap with best h value
            int bestHValue = hValueList.Min(x => x.Item1);
            int index = hValueList.FindIndex(x => x.Item1 == bestHValue);
            //perform that swap with the saved indexes
            SwapCells(block.blockIndexes[hValueList[index].Item2.Item1, hValueList[index].Item2.Item2], hValueList[index].Item3.Item1, hValueList[index].Item3.Item2, hValueList[index].Item4.Item1, hValueList[index].Item4.Item2);
            //update sudoku
            Program.MapBlocksToSudoku(block, Program.inputSudoku);
            int uu = 1;
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
            // Check if cells are not the same and not fixated
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
