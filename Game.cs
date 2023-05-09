using System;
using System.Collections.Generic;
using System.IO;

namespace TextBasedGame
{
    class Game
    {
        private bool isScoreSaved;
        private GameState gameState;
        private Player player;
        private List<Enemy> enemies;
        private int currentRound;

        public Game()
        {
            isScoreSaved = false;
            gameState = GameState.MainMenu;
        }

        public void Run()
        {
            while (true)
            {
                switch (gameState)
                {
                    case GameState.MainMenu:
                        MainMenu.DisplayMainMenu(this);
                        break;
                    case GameState.Battle:
                        Battle.StartBattle(this);
                        break;
                    case GameState.City:
                        City.VisitCity(this);
                        break;
                    case GameState.Dead:
                        if (!isScoreSaved)
                        {
                            GameOver();
                            isScoreSaved = true;
                        }
                        break;
                }

                if (gameState == GameState.MainMenu)
                {
                    break;
                }
            }
        }

        public void NewGame()
        {
            Console.Clear();
            Console.WriteLine("Enter your name: ");
            string playerName = Console.ReadLine();
            player = new Player(playerName);

            currentRound = 1;
            gameState = GameState.Battle;
        }

        public void SaveGame()
        {
            string saveData = player.Name + "," + player.Score;
            File.WriteAllText("save.txt", saveData);
            Console.WriteLine("Game saved successfully!");
        }

        public void LoadGame()
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
                        player = new Player(playerName);
                        player.Score = score;
                        Console.WriteLine("Game loaded successfully!");
                        gameState = GameState.City;
                    }
                }
            }
            else
            {
                Console.WriteLine("No saved game found.");
            }
        }

        public void GameOver()
        {
            Console.Clear();
            Console.WriteLine("Game Over!");
            Console.WriteLine("Your final score: " + player.Score);

            SaveScore(player.Score);
            DisplayTopScores();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            Environment.Exit(0);
        }

        private void SaveScore(int score)
        {
            // Code to save the score to a file or database
        }

        private void DisplayTopScores()
        {
            // Code to display the top scores from a file or database
        }
    }
}
