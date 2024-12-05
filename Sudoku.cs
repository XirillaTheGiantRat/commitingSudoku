using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class Sudoku
    {
        //saves all 81 numbers in the sudoku
        public int[,] allIndexes = new int[9, 9];

        public Sudoku(List<int> allNumbers) // allnumbers count = 81
        {
            for (int i = 0; i < allNumbers.Count(); i++)
            {
                int value = allNumbers[i];

                int row = i / 9;      
                int col = i % 9;      

                this.allIndexes[row, col] = value;
            }
        }


    }

    public class Block
    {
        // 2D array of 2D arrays 
        public (int cellValue, bool isFixated)[,][,] blockIndexes = new (int, bool)[3, 3][,]; 
        /*
        +---+---+---+
        | 1 | 2 | 3 |
        +---+---+---+
        | 4 | 5 | 6 |
        +---+---+---+
        | 7 | 8 | 9 |
        +---+---+---+
        */
        public Block(Sudoku sudoku)
        {
            // Grid of 3x3 tuples (sudoku value 1-9, true/false for fixated cells)
            (int, bool)[,] block1 = new (int, bool)[3, 3];
            (int, bool)[,] block2 = new (int, bool)[3, 3];
            (int, bool)[,] block3 = new (int, bool)[3, 3];
            (int, bool)[,] block4 = new (int, bool)[3, 3];
            (int, bool)[,] block5 = new (int, bool)[3, 3];
            (int, bool)[,] block6 = new (int, bool)[3, 3];
            (int, bool)[,] block7 = new (int, bool)[3, 3];
            (int, bool)[,] block8 = new (int, bool)[3, 3];
            (int, bool)[,] block9 = new (int, bool)[3, 3];

            for (int i = 0; i < 3; i++) // Iterate over rows of blocks
            {
                for (int j = 0; j < 3; j++) // Collumns 
                {
                    block1[i, j] = CreateTuple(sudoku.allIndexes[i, j]);
                    block2[i, j] = CreateTuple(sudoku.allIndexes[i, j + 3]);
                    block3[i, j] = CreateTuple(sudoku.allIndexes[i, j + 6]);
                    block4[i, j] = CreateTuple(sudoku.allIndexes[i + 3, j]);
                    block5[i, j] = CreateTuple(sudoku.allIndexes[i + 3, j + 3]);
                    block6[i, j] = CreateTuple(sudoku.allIndexes[i + 3, j + 6]);
                    block7[i, j] = CreateTuple(sudoku.allIndexes[i + 6, j]);
                    block8[i, j] = CreateTuple(sudoku.allIndexes[i + 6, j + 3]);
                    block9[i, j] = CreateTuple(sudoku.allIndexes[i + 6, j + 6]);
                }
            }

            blockIndexes[0, 0] = block1;
            blockIndexes[0, 1] = block2;
            blockIndexes[0, 2] = block3;
            blockIndexes[1, 0] = block4;
            blockIndexes[1, 1] = block5;
            blockIndexes[1, 2] = block6;
            blockIndexes[2, 0] = block7;
            blockIndexes[2, 1] = block8;
            blockIndexes[2, 2] = block9;
        }

        private (int, bool) CreateTuple(int value)
        {
            // The fixated values are True 
            bool isFixated = value > 0 && value <= 9;
            return (value, isFixated);
        }
    }
}
