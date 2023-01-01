using System;
using System.Collections.Generic;

namespace Sudoku
{
    internal class Board
    {
        private List<List<int>> board = new List<List<int>>();
        private List<List<int>> solution = new List<List<int>>();

        // Constructor
        public Board()
        {
            for (int i = 0; i < 9; i++)
            {
                board.Add(new List<int>());
                solution.Add(new List<int>());
            }
            FindSolution();
        }

        // Returns whether the board has met the solution
        public bool Solved() => (board == solution);

        // Return integers between two argument bounds, inclusive
        private int GenerateNum(int lowBound, int highBound)
        {
            Random rand = new Random();
            return rand.Next(highBound + 1) + lowBound;
        }

        private void FindSolution()
        {
            for (int i = 0; i < 9; i += 3)
                FillSubBoard(i);
            Solve(0, 3);
            solution = board;
            HideElements():
        }

        private void FillSubBoard(int corner)
        {
            int num;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    do
                        num = GenerateNum(1, 9);
                    while (!NotInSubBoard(num, corner, corner));
                    board[corner + i][corner + j] = num;
                }
            }
        }

        private bool Solve(int col, int row)
        {
            if (row >= 9 && col < 9-1)
            {
                col++;
                row = 0;
            }
            if (row >= 9 && col >= 9)
                return true;
            if (col < 3)
            {
                if (col < 3)
                    col = 3;
            }
            else if (col < 9 - Math.Sqrt(9))
            {
                if (col == row / 3 * 3)
                    col += 3;
            }
            else
            {
                if (col == 9 - Math.Sqrt(9))
                {
                    row++;
                    col = 0;
                    if (row >= 9) return true;
                }
            }
            for (int i = 1; i <= 9; i++)
            {
                // If the number does not exist within the confinds of a sub-board, row, or column...
                if (NotInSubBoard(i, row, col) && NotInRow(i, row) && NotInCol(i, col))
                {
                    board[row][col] = i; // Sets position to our number
                    // Checks if we can move onto the next position...
                    if (Solve(row, col + 1))
                        return true;
                    board[row][col] = 0; // If not, we set to 0 and move on
                }
            }
            return false;
        }

        // PRIVATE VOID HideElements
        // Hides *48* integers on the board by turning them into zeros
        private void HideElements()
        {
            // Defines count of elements to hide
            int count = 48;
            int xPos;
            int yPos;
            while (count != 0)
            {
                // Generating coordinates...
                xPos = GenerateNum(1, 9);
                yPos = GenerateNum(1, 9);

                // Checks if the target is already a zero...
                if (board[yPos][xPos] != 0)
                {
                    count--;
                    board[yPos][xPos] = 0;
                }
            }
        }

        // PRIVATE BOOLEAN NotInSubBoard
        // Returns boolean if the num parameter exists anywhere in a sub-board
        private bool NotInSubBoard(int num, int row, int col)
        {
            // Initializes start pos of sub-board
            int rowStart = row - (row % 3);
            int colStart = col - (col % 3);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    // If the number belongs anywhere else in the board, returns false
                    if (board[rowStart + i][colStart + j] == num)
                        return false;
                }
            } return true;
        }

        // PRIVATE BOOLEAN NotInRow
        // Returns boolean if the num parameter exists within the specified row
        private bool NotInRow(int num, int row)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (board[i][j] == num)
                        return false;
                }
            }
            return true;
        }

        // PRIVATE BOOLEAN NotInCol
        // Returns boolean if the num parameter exists within the specified column
        private bool NotInCol(int num, int col)
        {
            for (int i = 0; i < 9; i++)
            {
                if (board[i][col] == num)
                    return false;
            }
            return true;
        }
    }
}
