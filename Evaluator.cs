using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class Evaluator
    {
        public Evaluator()
        {

        }

        public int[] ReadHorizontal(Sudoku sudoku, int c) // stel je zegt c=2, dan is dit de derde column van de sudoku, vanwege 0-indexing
        {
            int[] numbersInColumn = new int[9];
            int value;

            for (int i = 0; i < 9; i++) 
            {
                value = sudoku.allIndexes[c, i];
                numbersInColumn[i] = value;
            }

            return numbersInColumn;
        }

    }
}
