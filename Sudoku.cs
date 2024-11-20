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

        public Sudoku(List<int> allNumbers)
        {

        }
    }

    public class Block
    {
        public int[,] blockIndexes = new int[3, 3];

        public Block()
        {

        }
    }
}
