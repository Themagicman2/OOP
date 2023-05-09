using System;

namespace TextBasedGame
{
    static class MainMenu
    {
        public static void DisplayMainMenu(Game game)
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Text-Based Game!");
            Console.WriteLine("1. Start New Game");
            Console.WriteLine("2. Quit");

            int choice = GetNumericInput(1, 2);

            if (choice == 1)
            {
                game.NewGame();
                Battle.StartBattle(game);
            }
            else
            {
                Environment.Exit(0);
            }
        }

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