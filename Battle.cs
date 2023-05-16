using System;
using System.Collections.Generic;
using System.IO;

namespace OOP
{
    class Battle
    {
        public static void StartBattle(Game game)
        {
            Console.Clear();
            Console.WriteLine("Round " + game.CurrentRound);
            Console.WriteLine(game.Player.Name + " vs Enemies");

            // Initialize enemies for the current round
            game.Enemies.Add(new Enemy("Enemy 1", 30, 20, 5));
            game.Enemies.Add(new Enemy("Enemy 2", 20, 30, 10));

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
                        if (!game.Enemies[i].IsAlive)
                        {
                            Console.WriteLine("You defeated " + game.Enemies[i].Name + "!");
                            game.Player.IncreaseScore(10);
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
                    if (!enemy.IsAlive)
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
                    game.CurrentRound++; // Access CurrentRound using the 'game' object

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

    enum GameState
    {
        MainMenu,
        Battle,
        City,
        Dead
    }

    class Game
    {
        private bool isScoreSaved; // Updated: Changed naming convention to camelCase
        public GameState GameState { get; set; } // Updated: Changed naming convention to PascalCase
        public Player Player { get; set; } // Updated: Changed naming convention to PascalCase
        public List<Enemy> Enemies { get; set; } // Updated: Changed naming convention to PascalCase
        public int CurrentRound { get; set; } // Updated: Changed naming convention to PascalCase

        public Game()
        {
            isScoreSaved = false; 
            GameState = GameState.MainMenu;
            Player = null;
            Enemies = new List<Enemy>();
            CurrentRound = 1;
        }

        private void VisitCity()
        {
            City.VisitCity(this);
        }

        public void Run()
        {
            while (true)
            {
                switch (GameState)
                {
                    case GameState.MainMenu:
                        DisplayMainMenu();
                        break;
                    case GameState.Battle:
                        Battle.StartBattle(this);
                        break;
                    case GameState.City:
                        VisitCity();
                        break;
                    case GameState.Dead:
                        if (!isScoreSaved) // Check if the score has already been saved
                        {
                            GameOver();
                            isScoreSaved = true; // Set the flag to indicate that the score has been saved
                        }
                        break;
                }

                // Check game state to exit the loop
                if (GameState == GameState.MainMenu)
                {
                    break;
                }
            }
        }

        private static void DisplayMainMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Text-Based Game!");
            Console.WriteLine("1. Start New Game");
            Console.WriteLine("2. Load Game");
            Console.WriteLine("3. Quit");

            int choice = GetNumericInput(1, 3);

            if (choice == 1)
            {
                NewGame();
                GameState = GameState.Battle;
            }
            else if (choice == 2)
            {
                LoadGame();
                GameState = GameState.City;
            }
            else
            {
                Environment.Exit(0);
            }
        }

        private static void NewGame()
        {
            Console.Clear();
            Console.WriteLine("Enter your name: ");
            string playerName = Console.ReadLine();
            Player = new Player(playerName);

            CurrentRound = 1;
        }

        private static void GameOver()
        {
            Console.Clear();
            Console.WriteLine("Game Over!");
            Console.WriteLine("Your final score: " + Player.Score);

            Console.WriteLine();
            Console.WriteLine("1. Save and Quit");
            Console.WriteLine("2. Quit without saving");

            int choice = GetNumericInput(1, 2);

            if (choice == 1)
            {
                SaveGame();
            }

            Environment.Exit(0);
        }

        public static void SaveGame()
        {
            string saveData = Player.Name + "," + Player.Score;
            File.WriteAllText("save.txt", saveData);
            Console.WriteLine("Game saved successfully!");
        }

        private static void LoadGame()
        {
            if (File.Exists("save.txt"))
            {
                string saveData = File.ReadAllText("save.txt");
                string[] values = saveData.Split(',');

                if (values.Length == 2)
                {
                    string playerName = values[0];
                    int score;

                    if (int.TryParse(values[1], out score))
                    {
                        Player = new Player(playerName);
                        Player.Score = score;
                        Console.WriteLine("Game loaded successfully!");
                    }
                }
            }
            else
            {
                Console.WriteLine("No saved game found.");
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
