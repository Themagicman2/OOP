using System;
using System.IO;


namespace OOP
{
    static class MainMenu
    {


        private static int GetNumericInput(int minValue, int maxValue)
{
    int input;
    while (true)
    {
        Console.Write("Enter your choice: ");
        if (int.TryParse(Console.ReadLine(), out input))
        {
            if (input >= minValue && input <= maxValue)
            {
                return input;
            }
        }
        Console.WriteLine("Invalid input. Please try again.");
            }
        }
    }
}