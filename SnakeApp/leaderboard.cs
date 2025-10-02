using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SnakeApp
{
    class Leaderboard
    {
        private string leaderboardFile;
        private string playersFile;

        public Leaderboard(string leaderboardFile = "leaderboard.txt", string playersFile = "players.txt")
        {
            this.leaderboardFile = leaderboardFile;
            this.playersFile = playersFile;
        }

        public void SavePlayerScore(string userName, int score)
        {
            string line = $"{userName};{score}";
            File.AppendAllText(playersFile, line + Environment.NewLine);
        }

        public void AddToLeaderboard(string userName, int score)
        {
            List<(string name, int score)> entries = new List<(string, int)>(); // loome

            if (File.Exists(leaderboardFile)) // olemas
            {
                var lines = File.ReadAllLines(leaderboardFile);
                foreach (var line in lines)
                {
                    var parts = line.Split(';'); // jagame rida
                    if (parts.Length == 2 && int.TryParse(parts[1].Trim(), out int s)) // kui rida pikkus 
                        entries.Add((parts[0].Trim(), s));
                }
            }

            var existing = entries.FirstOrDefault(e => e.name == userName); // kui selline mängija on, eemaldame ta ja lisame uue.
            if (existing != default && score > existing.score) // kui uuel mängijal on rohkem punkte kui vanal
            {
                entries.Remove(existing);
                entries.Add((userName, score));
            }
            else if (existing == default) // uue
            {
                entries.Add((userName, score));
            }

            var topEntries = entries.OrderByDescending(e => e.score).Take(10).ToList(); // top 10 players
            var linesToWrite = topEntries.Select(e => $"{e.name};{e.score}"); 
            File.WriteAllLines(leaderboardFile, linesToWrite); // kirjutame faili ümber
        }

        public List<(string name, int score)> GetTopPlayers(int top = 10)
        {
            List<(string name, int score)> leaderboard = new List<(string, int)>();

            if (File.Exists(leaderboardFile))
            {
                string[] lines = File.ReadAllLines(leaderboardFile);
                foreach (var line in lines)
                {
                    var parts = line.Split(';');
                    if (parts.Length == 2 && int.TryParse(parts[1].Trim(), out int s))
                        leaderboard.Add((parts[0].Trim(), s));
                }
            }

            return leaderboard.OrderByDescending(x => x.score).Take(top).ToList();
        }
    }
}
