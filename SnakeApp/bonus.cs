using System;

namespace SnakeApp
{
    class Bonus
    {
        private int width; // laius
        private int height; // kõrgus
        private char bonusSymbol;
        private Random random; // kus ilmub märk
        private DateTime lastBonusTime; // 15 sec bonus kontroll // kui viimati ilmus boonus
        private Point currentBonus; // boonuse kuvamine 
        private bool bonusActive; // true - kaartide boonus kas boonus on praegu aktiivne?
        private int bonusLifetimeSeconds = 5; // boonus elab 5 seb

        public Bonus(int width, int height, char bonusSymbol = '£')
        {
            this.width = width;
            this.height = height;
            this.bonusSymbol = bonusSymbol;
            random = new Random();
            lastBonusTime = DateTime.Now;
            bonusActive = false;
        }

        public void UpdateBonus()
        {
            if (!bonusActive && (DateTime.Now - lastBonusTime).TotalSeconds >= 15) // kui on möödunud 15 sekundit, loob uue boonuse
            {
                CreateBonus();
            }
            
            if (bonusActive && (DateTime.Now - lastBonusTime).TotalSeconds > bonusLifetimeSeconds) // eemaldab boonuse  5 sekundi pärast, kui seda ei ole söödud
            {
                RemoveBonus();
            }
        }
        private void CreateBonus()
        {
            int x = random.Next(2, width - 2);
            int y = random.Next(2, height - 2);
            currentBonus = new Point(x, y, bonusSymbol);
            currentBonus.Draw(); // näitab
            bonusActive = true; // true = boonus kaardil
            lastBonusTime = DateTime.Now; // mäletab, millal viimati oli boonus
        }

        private void RemoveBonus()
        {
            if (bonusActive)
            {
                Console.SetCursorPosition(currentBonus.x, currentBonus.y);
                Console.Write(' ');
                bonusActive = false; 
                lastBonusTime = DateTime.Now; // kui boonus kadus
            }
        }
        
        public bool CheckBonusEaten(Snake snake)
        {
            if (bonusActive && snake.GetHead().IsHit(currentBonus)) // sõi
            {
                bonusActive = false;
                lastBonusTime = DateTime.Now;
                return true;
            }
            return false;
        }
    }
}
