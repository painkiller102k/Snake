using System;

namespace SnakeApp
{
    public class HP
    {
        public int MaxLives { get; private set; }
        public int CurrentLives { get; private set; }

        public HP(int maxLives = 2)
        {
            MaxLives = maxLives;
            CurrentLives = maxLives;
        }

        public void LoseLife()
        {
            if (CurrentLives > 0)
                CurrentLives--;
        }

        public bool IsAlive()
        {
            return CurrentLives > 0;
        }
    }
}