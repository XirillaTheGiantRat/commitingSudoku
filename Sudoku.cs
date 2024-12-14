using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class Sudoku
    {
        // Save all 81 numbers in the sudoku
        public int[,] allIndexes = new int[9, 9];

        public Sudoku(List<int> allNumbers) // Create the whole sudoku (9x9 grid)
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
}
