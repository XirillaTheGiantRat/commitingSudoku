using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class SearchOperator
    {
        public Block block;
        private Random random = new Random();
        public SearchOperator(Block b)
        {
            block = b;
        }

        public List<(int, bool)> GetNonFixatedFields()
        {
            List<(int, bool)> nonFixatedFields = new List<(int, bool)>();
            foreach ((int, bool) field in block.blockIndexes) 
            {
                if (!field.Item2)
                {
                    nonFixatedFields.Add(field);
                }
            }
            return nonFixatedFields;
        }

        public void SwitchTwoRandomValues(List<(int, bool)> nonFixatedFields)
        {
            int length = nonFixatedFields.Count;
            int indexOne = random.Next(0, length);
            int indexTwo = random.Next(0, length);
            if (indexOne != indexTwo)
            {
                (int, bool) switchOne = nonFixatedFields[indexOne];
                (int, bool) switchTwo = nonFixatedFields[indexTwo];

                // wisselen van de waardes komt hier
            }
            else 
            {
                return; // ?? klopt misschien niet
            }

        }
    }
}
