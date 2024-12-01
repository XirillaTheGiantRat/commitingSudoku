﻿using System;
using System.Security.Cryptography.X509Certificates;

namespace Sudoku
{
    public class Program
    {
        public static string inputString;
        public static List<int> intList;

        public static void Main(string[] args)
        {
            Console.WriteLine("Enter sudoku:");
            inputString = Console.ReadLine();

            //save every number as a separate int in the intList
            intList = inputString.Split(' ', StringSplitOptions.RemoveEmptyEntries) .Select(int.Parse) .ToList();

            MakeSudokuFromInput(intList);
        }


        public static Sudoku MakeSudokuFromInput(List<int> inputList)
        {
            Sudoku inputSudoku = new Sudoku(inputList);
            return inputSudoku;
        }
    }

}
