using System;
using System.Threading;
using NetCoreAudio;
using System.IO;
using System.Threading.Tasks;

namespace SnakeApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Clear();
            Console.Write("Sisesta oma nimi: ");
            string userName = Console.ReadLine();
            Console.Clear();

            int score = 0;
            var player = new Player();
            
            string basePath = Directory.GetCurrentDirectory();
            string menuSound = Path.Combine(basePath, "sounds", "menusound_fixed.wav");
            string eatSound = Path.Combine(basePath, "sounds", "eatsound_fixed.wav");
            string loseSound = Path.Combine(basePath, "sounds", "losesound_fixed.wav");
            
                await player.Play(menuSound);

            Walls walls = new Walls(80, 25);
            walls.Draw();

            Point p = new Point(4, 5, '*');
            Snake snake = new Snake(p, 4, Direction.RIGHT);
            snake.Draw();
            
            FoodCreator foodCreator = new FoodCreator(80, 25, '$');
            Point food = foodCreator.CreateFood();
            food.Draw();

            while (true)
            {
                if (walls.IsHit(snake) || snake.IsHitTail())
                    break;

                if (snake.Eat(food))
                {
                    score += 10;
                    food = foodCreator.CreateFood();
                    food.Draw();
                    
                    _ = Task.Run(async () => await player.Play(eatSound));
                }
                else
                {
                    snake.Move();
                }


                ShowScore(score);

                Thread.Sleep(100);

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    snake.HandleKey(key.Key);
                }
            }

            WriteGameOver(userName, score);
            
                await player.Play(loseSound);

            LeaderboardManager leaderboard = new LeaderboardManager();
            leaderboard.SavePlayerScore(userName, score);
            leaderboard.AddToLeaderboard(userName, score);
            leaderboard.ShowLeaderboard();
        }

        static void ShowScore(int score)
        {
            Console.SetCursorPosition(2, 0);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"Score: {score}  ");
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
