using System;
using System.Collections.Generic;

{
    
}
    class Battle
    {
        public static void StartBattle(Game game)
        {
            Console.Clear();
            Console.WriteLine("Round " + game.CurrentRound);
            Console.WriteLine(game.Player.Name + " vs Enemies");

            // Initialize enemies for the current round
            game.Enemies = new List<Enemy>();
            game.Enemies.Add(new Enemy("Enemy 1"));
            game.Enemies.Add(new Enemy("Enemy 2"));

            while (game.Player.IsAlive && game.Enemies.Count > 0)
            {
                bool enemyAttacked = false;

                Console.WriteLine();
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("1. Attack");
                Console.WriteLine("2. Check Player Stats");

                int choice = GetNumericInput(1, 2);

                if (choice == 1)
                {
                    game.Player.Attack(game.Enemies);

                    for (int i = game.Enemies.Count - 1; i >= 0; i--)
                    {
                        if (game.Enemies[i].IsDefeated)
                        {
                            Console.WriteLine("You defeated " + game.Enemies[i].Name + "!");
                            game.Player.Score += 10;
                            game.Enemies.RemoveAt(i);
                        }
                    }
                }
                else
                {
                    game.Player.DisplayStats();
                }

                foreach (Enemy enemy in game.Enemies)
                {
                    if (enemy.IsDefeated)
                    {
                        continue;
                    }

                    enemy.Attack(game.Player);
                    enemyAttacked = true;
                    break;
                }

                if (!enemyAttacked)
                {
                    Console.WriteLine("Enemies cannot attack at the moment.");
                }

                if (game.Player.IsAlive && game.Enemies.Count == 0)
                {
                    Console.WriteLine("Congratulations! You have defeated all enemies in round " + game.CurrentRound);
                    game.CurrentRound++;

                    Console.WriteLine();
                    Console.WriteLine("What would you like to do?");
                    Console.WriteLine("1. Continue to the next round");
                    Console.WriteLine("2. Visit the City");

                    choice = GetNumericInput(1, 2);

                    if (choice == 1)
                    {
                        StartBattle(game);
                    }
                    else
                    {
                        game.GameState = GameState.City;
                    }
                }
                else if (!game.Player.IsAlive)
                {
                    game.GameState = GameState.Dead;
                }
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

