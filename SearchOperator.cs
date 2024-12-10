using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class SearchOperator
    {
        private static Random random = new Random();
        public SearchOperator()
        {
            
        }

        // Perform a random swap between 2 non-fixated cells 
        public static void swapCells(Block block, int i, int j)
        {
            (int, bool)[,] b = block.blockIndexes[i, j];
            bool done = false;

            while (!done)
            {
                int one = random.Next(3);
                int two = random.Next(3);
                int three = random.Next(3); 
                int four = random.Next(3);

                bool check1 = b[one, two].Item2 == false;
                bool check2 = b[three, four].Item2 == false;

                if (check1 && check2 && !(b[one, two].Item1 == b[three, four].Item1))
                {
                    int save = b[one, two].Item1;
                    b[one, two].Item1 = b[three, four].Item1;
                    b[three, four].Item1 = save;
                    done = true;
                }
            }
        }

        
    }
}
