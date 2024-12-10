using System;
using System.Collections.Generic;

namespace Sudoku
{
    public class LocalSearch
    {
        private static Random random = new Random();

        public void PerformLocalSearch(Block block)
        {
            // Select a random block
            int blockRow = random.Next(3);
            int blockCol = random.Next(3);
        }

    }
}
