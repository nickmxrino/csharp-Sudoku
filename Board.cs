using System;

namespace Sudoku
{
    internal class Board
    {
        private int[,] board = new int[9,9];
        private int[,] solution = new int[9,9];

        // Constructor
        public Board()
        {
            FindSolution();
        }

        // Returns whether the board has met the solution
        public bool Solved()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (board[i,j] != solution[i,j])
                        return false;
                }
            }
            return true;
        }

        // Inserts the num parameter into the coordinates provided
        public void PlaceInteger(int num, int row, int column) => board[row,column] = num;

        // Return integers between two argument bounds, inclusive
        private int GenerateNum(int lowBound, int highBound)
        {
            Random rand = new Random();
            return rand.Next(lowBound, highBound + 1);
        }
        
        public void Display()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("  1 2 3 4 5 6 7 8 9");
            Console.ResetColor();
            for (int i = 0; i < 9; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(i+1 + " ");
                Console.ResetColor();
                for (int j = 0; j < 9; j++)
                {
                    if (board[i, j] == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(board[i, j] + " ");
                        Console.ResetColor();
                    }
                    else if (board[i,j] != solution[i,j])
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write(board[i, j] + " ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(board[i, j] + " ");
                    }
                }
                Console.WriteLine();
            }
        }

        private void FindSolution()
        {
            int count = 0;
            for (int i = 0; count < 3; i += 3)
            {
                FillSubBoard(i);
                count++;
            }
            Solve(0,3);
            Array.Copy(board, solution, 81);
            HideElements();
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
                    board[corner + i,corner + j] = num;
                }
            }
        }

        private bool Solve(int row, int col)
        {
            if (row < 8 && col >= 9)
            {
                row++;
                col = 0;
            }
            if (row >= 9 && col >= 9)
                return true;
            if (row < 3)
            {
                if (col < 3)
                    col = 3;
            }
            else if (row < 6)
            {
                if (col == row / 3 * 3)
                    col += 3;
            }
            else
            {
                if (col == 6)
                {
                    row++;
                    col = 0;
                    if (row >= 9) return true;
                }
            }
            for (int i = 1; i <= 9; i++)
            {
                // If the number does not exist within the confinds of a sub-board, row, or column...
                if (NotInConflict(i, row, col))
                {
                    board[row,col] = i; // Sets position to our number
                    // Checks if we can move onto the next position...
                    if (Solve(row, col + 1))
                        return true;
                    board[row,col] = 0; // If not, we set to 0 and move on
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
            int row;
            int col;
            int index;
            while (count != 0)
            {
                // Generating coordinates...
                index = GenerateNum(1, 80);
                row = index / 9;
                col = index % 9;

                // Checks if the target is already a zero...
                if (board[row,col] != 0)
                {
                    count--;
                    board[row,col] = 0;
                }
            }
        }

        // PRIVATE BOOL NotInConflict
        // Returns boolean if the num parameter is safe to place and not in conflict
        private bool NotInConflict(int num, int row, int col)
        {
            return (NotInSubBoard(num, row, col) && NotInRow(num, row) && NotInCol(num, col));
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
                    if (board[rowStart + i,colStart + j] == num)
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
                if (board[row,i] == num)
                    return false;
            }
            return true;
        }

        // PRIVATE BOOLEAN NotInCol
        // Returns boolean if the num parameter exists within the specified column
        private bool NotInCol(int num, int col)
        {
            for (int i = 0; i < 9; i++)
            {
                if (board[i,col] == num)
                    return false;
            }
            return true;
        }
    }
}
