using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class Block
    {
        // 2D array of 2D arrays 
        public (int cellValue, bool isFixated)[,][,]blockIndexes = new (int, bool)[3, 3][,];

        public Block(Sudoku sudoku)
        {
            // Create all 9 blocks
            for (int blockRow = 0; blockRow < 3; blockRow++)
            {
                for (int blockCol = 0; blockCol < 3; blockCol++)
                {
                    
                    var block = new (int, bool)[3, 3]; // Create the current block, a grid of 3x3 tuples

                    // Fill in block with cells from the sudoku
                    for (int row = 0; row < 3; row++) 
                    {
                        for (int column = 0; column < 3; column++) 
                        {
                            int value = sudoku.allIndexes[row + blockRow * 3, column + blockCol * 3];
                            block[row, column] = CreateTuple(value);
                        }
                    }

                    blockIndexes[blockRow, blockCol] = block; // Put created block in the 2D array
                }
            }
        }

        private (int, bool) CreateTuple(int value)
        {
            // Set the fixated cells (1-9) to True and the non-fixated cells (0) to False 
            bool isFixated = value > 0 && value <= 9;
            return (value, isFixated);
        }
    }
}
