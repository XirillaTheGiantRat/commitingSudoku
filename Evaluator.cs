using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class Evaluator
    {
        public Sudoku sudoku;
        public Evaluator(Sudoku s)
        {
            sudoku = s;
        }

        // Read the numbers of the row in the sudoku (with 0-indexing)
        public int[] ReadRow(int r) 
        {
            int[] numbersInRow = new int[9];
            int value;

            for (int i = 0; i < 9; i++) 
            {
                value = sudoku.allIndexes[r, i];
                numbersInRow[i] = value;
            }

            return numbersInRow;
        }

        // Read the numbers of the vertical row in the sudoku (with 0-indexing)
        public int[] ReadColumn(int c) 
        {
            int[] numbersInColumn = new int[9];
            int value;

            for (int i = 0; i < 9; i++)
            {
                value = sudoku.allIndexes[i, c];
                numbersInColumn[i] = value;
            }

            return numbersInColumn;
        }

        // Checks if number n is in the list 
        public static bool NumberInList(int[] input, int n) 
        {
            foreach (int value in input)
            {
                if (value == n) return true; // False is the number is not in the list, and True if it is
            }
            return false;
        }

        // Check if all numbers 1-9 are in the list 
        public static (bool, List<int>) AllNumbersIncludedInList(int[] input) 
        { 

            List<int> missingNumbers = new List<int>(); // keep track of the missing numbers

            for (int n = 1; n <= 9; n++)
            {
                if (!NumberInList(input, n)) missingNumbers.Add(n);
            }

            bool allNumbersIncluded = (missingNumbers.Count() == 0);
            return (allNumbersIncluded, missingNumbers);
        }

        public static int HeuristicFunctionPerRowOrColumn(int[] input) 
        {
            // Boolean value of True implies a complete list, which should have a heuristic value of 0
            (bool, List<int>) tuple = AllNumbersIncludedInList(input); 

            if (tuple.Item1) return 0; 
            else 
            {
                List<int> missingNumbers = tuple.Item2;
                int newValue = missingNumbers.Count();
                return newValue;
            }
        }

        public int[] allRowHeuristics(Sudoku input)
        {
            int[] heuristicValuesPerRow = new int[9];
            for (int i = 0; i < 9; i++)
            {
                int[] row = ReadRow(i);
                int hValue = HeuristicFunctionPerRowOrColumn(row);
                heuristicValuesPerRow[i] = hValue;
            }
            return heuristicValuesPerRow;
        }

        public int[] allColumnHeuristics(Sudoku input)
        {
            int[] heuristicValuesPerColumn = new int[9];
            for (int i = 0; i < 9; i++)
            {
                int[] column = ReadColumn(i);
                int hValue = HeuristicFunctionPerRowOrColumn(column);
                heuristicValuesPerColumn[i] = hValue;
            }
            return heuristicValuesPerColumn;
        }

        public int GetSudokuHValue(Sudoku input)
        {
            int[] heuristicRowValues = allRowHeuristics(input);
            int[] heuristicColumnValues = allColumnHeuristics(input);
            int hValue = HeuristicSums(heuristicRowValues, heuristicColumnValues);
            return hValue;
        }

        // Helper function for GetSudokuHValue
        public int HeuristicSums(int[] rowHeuristics, int[] columnHeuristics)
        {
            int sumOfRows = rowHeuristics.Sum();
            int sumOfColumns = columnHeuristics.Sum();
            return sumOfRows + sumOfColumns;
        }
    }
}
