using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    internal class Inserter
    {
        public Inserter(Block blocks) 
        {
            Block block = blocks;
        }

        public static void InsertValue(Block block, int i, int j) 
        {
            List<int> existingNums = new List<int>();
            (int, bool)[,] blockie = block.blockIndexes[i, j];
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    int value = blockie[row, col].Item1;
                    if (value > 0 && value < 10)
                    {
                        existingNums.Add(value);
                    }
                }
            }

            for (int k = 0; k < 10; k++)
            {
                if (!existingNums.Contains(k))
                {
                    bool found0 = false; 

                    for (int row = 0; row < 3; row++)
                    {
                        for (int col = 0; col < 3; col++)
                        {
                            if (blockie[row, col].Item1 == 0)
                            {
                                blockie[row, col].Item1 = k;
                                found0 = true; 
                                break; 
                            }
                        }
                        if (found0) 
                            break;
                    }
                }
            }
        }
    }
}
