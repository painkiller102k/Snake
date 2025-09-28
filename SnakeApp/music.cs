using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace SnakeApp
{
    public class MusicManager
    {
        private string menuSound;
        private string eatSound;
        private string loseSound;
        private Process menuProcess;

        public MusicManager(string basePath)
        {
            menuSound = Path.Combine(basePath, "sounds", "menusound_fixed.wav");
            eatSound = Path.Combine(basePath, "sounds", "eatsound_fixed.wav");
            loseSound = Path.Combine(basePath, "sounds", "losesound_fixed.wav");
        }

        public void PlayMenuMusic() // menu
        {
            menuProcess = Process.Start("afplay", menuSound);
        }

        public void StopMenuMusic()
        {
            if (menuProcess != null && !menuProcess.HasExited)
            {
                menuProcess.Kill();
            }
        }

        public void PlayEatSound()
        {
            Task.Run(() => Process.Start("afplay", eatSound));
        }

        public void PlayLoseSound()
        {
            Task.Run(() => Process.Start("afplay", loseSound));
        }
    }
}