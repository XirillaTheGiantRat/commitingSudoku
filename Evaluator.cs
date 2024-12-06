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

        public int[] ReadVertical(Sudoku sudoku, int r) // r is de row van de sudoku gelezen met 0-indexing
        {
            int[] numbersInRow = new int[9];
            int value;

            for (int i = 0; i < 9; i++)
            {
                value = sudoku.allIndexes[i, r];
                numbersInRow[i] = value;
            }

            return numbersInRow;
        }

        public int Read0Values(int[] input)
        {
            int amountOf0Values = 0;

            foreach (int i in input)
            {
                if(i == 0) amountOf0Values++;
            }

            return amountOf0Values;
        }

        
        public bool IsThereANumber(int[] input, int n)
        {// kijkt of nummer n in de lijst zit. false als hij er niet in zit en true als hij er wel in zit.
            foreach (int value in input)
            {
                if (value == n) return true;
            }
            return false;
        }

        public (bool, List<int>) AreAllNumbersIncludedInList(int[] input) //unit test
        { // Kijkt of alle getallen van 1-9 in een lijst zitten en houdt bij welke getallen er missen
            List<int> missingNumbers = new List<int>();

            for (int n = 1; n <= 9; n++)
            {
                if (!IsThereANumber(input, n)) missingNumbers.Add(n);
            }

            bool allNumbersIncluded = (missingNumbers.Count() == 0);
            return (allNumbersIncluded, missingNumbers);
        }

        public int HeuristicFunctionPerRowOrColumn(int[] input) // unit test
        {
            (bool, List<int>) tuple = AreAllNumbersIncludedInList(input);
            // als de bool true is, is de lijst dus compleet van alle waarden 1-9
            // heuristische waarde moet dan 0 zijn
            if (tuple.Item1) return 0; 
            else 
            {
                // anders wordt de heuristische waarde verhoogd voor elk nummer die nog mist in de lijst
                List<int> missingNumbers = tuple.Item2;
                int newValue = missingNumbers.Count();
                return newValue;
            }
        }
    }
}
