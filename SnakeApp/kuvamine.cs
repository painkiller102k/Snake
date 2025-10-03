using System;
using System.Collections.Generic;

namespace SnakeApp
{
    public static class kuvamine
    {
        public static void ShowScore(int score, GameMode gameMode)
        {
            int xOffset = gameMode.Width + 5;
            int yOffset = 4;

            Console.SetCursorPosition(xOffset, yOffset);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"Score: {score}   ");
            Console.ResetColor();
        }

        public static void ShowGameMode(GameMode gameMode)
        {
            int xOffset = gameMode.Width + 5;
            int yOffset = 2;

            Console.SetCursorPosition(xOffset, yOffset);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write($"Mode: {gameMode.Name}   ");
            Console.ResetColor();
        }

        public static void ShowLives(HP hp, GameMode gameMode)
        {
            int xOffset = gameMode.Width + 5;
            int yOffset = 0;

            Console.SetCursorPosition(xOffset, yOffset);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"Lives: {hp.CurrentLives}/{hp.MaxLives}   ");
            Console.ResetColor();
        }

        public static void ShowLeaderboard(List<(string name, int score)> topPlayers)
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

        public static void WriteGameOver(string userName, int score)
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

        public static void WriteText(string text, int xOffset, int yOffset)
        {
            Console.SetCursorPosition(xOffset, yOffset);
            Console.WriteLine(text);
        }
    }
}
