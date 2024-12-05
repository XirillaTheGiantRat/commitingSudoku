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
        public int[,][,] blockIndexes = new int[3, 3][,];
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
            int[,] block1 = new int[3, 3];
            int[,] block2 = new int[3, 3];
            int[,] block3 = new int[3, 3];
            int[,] block4 = new int[3, 3];
            int[,] block5 = new int[3, 3];
            int[,] block6 = new int[3, 3];
            int[,] block7 = new int[3, 3];
            int[,] block8 = new int[3, 3];
            int[,] block9 = new int[3, 3];

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    block1[i,j] = sudoku.allIndexes[i, j];
                }
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    block2[i, j] = sudoku.allIndexes[i, j + 3];
                }
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    block3[i, j] = sudoku.allIndexes[i, j + 6];
                }
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    block4[i, j] = sudoku.allIndexes[i + 3, j];
                }
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    block5[i, j] = sudoku.allIndexes[i + 3, j + 3];
                }
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    block6[i, j] = sudoku.allIndexes[i + 3, j + 6];
                }
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    block7[i, j] = sudoku.allIndexes[i + 6, j];
                }
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    block8[i, j] = sudoku.allIndexes[i + 6, j + 3];
                }
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    block9[i, j] = sudoku.allIndexes[i + 6, j + 6];
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
    }
}
