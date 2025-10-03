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
                HP hp = new HP(2);
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
                Bonus bonus = new Bonus(gameMode.Width, gameMode.Height, '£');

                while (true)
                {
                    if (walls.IsHit(snake) || snake.IsHitTail())
                    {
                        hp.LoseLife();
                        if (!hp.IsAlive()) // kas madu on elus
                            break;
                        else
                        {
                            snake.Clear();
                            snake = new Snake(new Point(4, 5, '*'), 4, Direction.RIGHT);
                            snake.Draw();
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

                    bonus.UpdateBonus();

                    if (bonus.CheckBonusEaten(snake))
                    {
                        score += 40;
                        hp.AddLife();
                        music.PlayEatSound();
                    }

                    kuvamine.ShowScore(score, gameMode);
                    kuvamine.ShowGameMode(gameMode);
                    kuvamine.ShowLives(hp, gameMode);

                    Thread.Sleep(gameMode.Speed);

                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo key = Console.ReadKey(true); // juhtimine
                        snake.HandleKey(key.Key);
                    }
                }

                kuvamine.WriteGameOver(userName, score);

                music.StopMenuMusic();
                music.PlayLoseSound();

                leaderboard.SavePlayerScore(userName, score);
                leaderboard.AddToLeaderboard(userName, score);

                Console.SetCursorPosition(0, gameMode.Height + 5);
                kuvamine.ShowLeaderboard(leaderboard.GetTopPlayers());

                Console.WriteLine("Tahad veel mängida? (y/n): ");
                string input = Console.ReadLine().Trim().ToLower();
                playAgain = input == "y" || input == "yes";
                Console.Clear();
            }
        }
    }
}