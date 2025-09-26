using System;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;

namespace SnakeApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            bool playAgain = true;

            while (playAgain)
            {
                Console.Clear();
                Console.Write("Sisesta oma nimi: ");
                string userName = Console.ReadLine();
                Console.Clear();

                GameMode gameMode = GameMode.Choose();
                Console.Clear();

                int score = 0;
                HP hp = new HP(2); // 2 жизни
                string basePath = Directory.GetCurrentDirectory();
                MusicManager music = new MusicManager(basePath);

                music.PlayMenuMusic();

                Walls walls = new Walls(gameMode.Width, gameMode.Height);
                walls.Draw();

                Point p = new Point(4, 5, '*');
                Snake snake = new Snake(p, 4, Direction.RIGHT);
                snake.Draw();

                FoodCreator foodCreator = new FoodCreator(gameMode.Width, gameMode.Height, '$');
                Point food = foodCreator.CreateFood();
                food.Draw();

                Leaderboard leaderboard = new Leaderboard();

                while (true)
                {
                    if (walls.IsHit(snake) || snake.IsHitTail())
                    {
                        hp.LoseLife();
                        if (!hp.IsAlive())
                            break;
                        else
                        {
                            snake = new Snake(new Point(4, 5, '*'), 4, Direction.RIGHT);
                        }
                    }

                    if (snake.Eat(food))
                    {
                        score += gameMode.PointsPerFood;
                        food = foodCreator.CreateFood();
                        food.Draw();

                        music.PlayEatSound();
                    }
                    else
                    {
                        snake.Move();
                    }

                    ShowScore(score, gameMode);
                    ShowGameMode(gameMode);
                    ShowLives(hp, gameMode);

                    Thread.Sleep(gameMode.Speed);

                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo key = Console.ReadKey(true);
                        snake.HandleKey(key.Key);
                    }
                }

                WriteGameOver(userName, score);

                music.StopMenuMusic();
                music.PlayLoseSound();

                leaderboard.SavePlayerScore(userName, score);
                leaderboard.AddToLeaderboard(userName, score);

                Console.SetCursorPosition(0, gameMode.Height + 5);
                ShowLeaderboard(leaderboard.GetTopPlayers());

                Console.WriteLine("Tahad veel mängida? (y/n): ");
                string input = Console.ReadLine().Trim().ToLower();
                playAgain = input == "y" || input == "yes";
                Console.Clear();
            }
        }

        static void ShowScore(int score, GameMode gameMode)
        {
            int xOffset = gameMode.Width + 5;
            int yOffset = 4;

            Console.SetCursorPosition(xOffset, yOffset);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"Score: {score}   ");
            Console.ResetColor();
        }

        static void ShowGameMode(GameMode gameMode)
        {
            int xOffset = gameMode.Width + 5;
            int yOffset = 2;

            Console.SetCursorPosition(xOffset, yOffset);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write($"Mode: {gameMode.Name}   ");
            Console.ResetColor();
        }

        static void ShowLives(HP hp, GameMode gameMode)
        {
            int xOffset = gameMode.Width + 5;
            int yOffset = 0;

            Console.SetCursorPosition(xOffset, yOffset);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"Lives: {hp.CurrentLives}/{hp.MaxLives}   ");
            Console.ResetColor();
        }

        static void ShowLeaderboard(List<(string name, int score)> topPlayers)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("===== Edetabel =====");
            int place = 1;
            foreach (var entry in topPlayers)
            {
                Console.WriteLine($"{place}. {entry.name} - {entry.score}");
                place++;
                if (place > 10) break;
            }
            Console.WriteLine("=======================");
            Console.ResetColor();
        }

        static void WriteGameOver(string userName, int score)
        {
            int xOffset = 25;
            int yOffset = 8;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(xOffset, yOffset++);
            WriteText("------------------------", xOffset, yOffset++);
            WriteText("--------GAME OVER---------", xOffset + 1, yOffset++);
            yOffset++;
            WriteText($"Player: {userName}", xOffset + 2, yOffset++);
            WriteText($"Score: {score}", xOffset + 2, yOffset++);
            WriteText("------------------------", xOffset, yOffset++);
            Console.ResetColor();
        }

        static void WriteText(string text, int xOffset, int yOffset)
        {
            Console.SetCursorPosition(xOffset, yOffset);
            Console.WriteLine(text);
        }
    }
}
