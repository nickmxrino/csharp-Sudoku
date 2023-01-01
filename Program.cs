using System;

namespace Sudoku
{
    internal class Program
    {
        static int InputInteger(string msg)
        {
            int result;
            bool valid;
            do
            {
                Console.Write(msg);
                valid = int.TryParse(Console.ReadLine(), out result);
                if (!valid)
                    Console.WriteLine("You must enter an integer, try again.");
            } while (!valid);
            return result;
        }

        static int InputIntegerBetweenIncl(string msg, int lowBound, int highBound)
        {
            int result;
            do
            {
                result = InputInteger(msg);
                if (result >= lowBound && result <= highBound)
                    return result;
            } while (true);
        }

        static void Play()
        {
            Board table = new Board();
            int num;
            int row;
            int col;
            while (!table.Solved())
            {
                table.Display();
                row = InputIntegerBetweenIncl("Provide a row: ", 1, 9);
                col = InputIntegerBetweenIncl("Provide a column: ", 1, 9);
                num = InputIntegerBetweenIncl("Provide a number: ", 1, 9);
                table.PlaceInteger(num, row-1, col-1);
            }
            Console.WriteLine("Congratulations, you've won!");
        }

        static void Main(string[] args)
        {
            Play();
            Console.WriteLine("Press to close...");
            Console.ReadKey();
        }
    }
}
