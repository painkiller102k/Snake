using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SnakeApp
{
    class LeaderboardManager
    {
        private string leaderboardFile;

        public LeaderboardManager(string leaderboardFile = "leaderboard.txt")
        {
            this.leaderboardFile = leaderboardFile;
        }

        // player score txt
        public void SavePlayerScore(string userName, int score)
        {
            string fileName = $"{userName}_score.txt";
            File.WriteAllText(fileName, $"Player: {userName}\nScore: {score}");
        }

        // leaderboard.txt
        public void AddToLeaderboard(string userName, int score)
        {
            using (StreamWriter sw = new StreamWriter(leaderboardFile, true))
            {
                sw.WriteLine($"{userName};{score}");
            }
        }

        // leaderboard #10
        public List<(string name, int score)> GetTopPlayers(int top = 10)
        {
            List<(string name, int score)> leaderboard = new List<(string, int)>();

            if (File.Exists(leaderboardFile))
            {
                string[] lines = File.ReadAllLines(leaderboardFile);
                foreach (var line in lines)
                {
                    var parts = line.Split(';');
                    if (parts.Length == 2 && int.TryParse(parts[1], out int s))
                    {
                        leaderboard.Add((parts[0], s));
                    }
                }
            }

            return leaderboard.OrderByDescending(x => x.score).Take(top).ToList();
        }

        // top players
        public void ShowLeaderboard(int top = 10)
        {
            var sorted = GetTopPlayers(top);

            Console.WriteLine("\n===== Leaderboard =====");
            int place = 1;
            foreach (var entry in sorted)
            {
                Console.WriteLine($"{place}. {entry.name} - {entry.score}");
                place++;
            }
            Console.WriteLine("===================");
        }
    }
}
