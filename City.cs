using System;
using System.IO;


namespace OOP
{
    static class City
    {
        
        public static void VisitCity(Game game)
        {
            Console.Clear();
            Console.WriteLine("Welcome to the City!");
            Console.WriteLine("1. Rest and Recover Health");
            Console.WriteLine("2. Buy Health Potion");
            Console.WriteLine("3. Save Game");
            Console.WriteLine("4. Return to Main Menu");

            int choice = GetNumericInput(1, 4);

            switch (choice)
            {
                case 1:
                    game.Player.Rest();
                    break;
                case 2:
                    game.Player.BuyHealthPotion();
                    break;
                case 3:
                    game.SaveGame();
                    break;
                case 4:
                    game.GameState = GameState.MainMenu;
                    break;
            }
        }

        internal static void VisitCity()
        {
            throw new NotImplementedException();
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