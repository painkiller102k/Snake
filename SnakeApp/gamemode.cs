namespace SnakeApp
{
    public class GameMode 
    {
        public string Name { get; }
        public int Speed { get; } 
        public int PointsPerFood { get; }
        public int Width { get; }
        public int Height { get; }

        public GameMode(string name, int speed, int pointsPerFood, int width, int height) // kõik väärtused
        {
            Name = name;
            Speed = speed;
            PointsPerFood = pointsPerFood;
            Width = width;
            Height = height;
        }

        public static GameMode Choose()
        {
            Console.Clear();
            Console.WriteLine("Vali raskustase:");
            Console.WriteLine("1 - Easy");
            Console.WriteLine("2 - Medium");
            Console.WriteLine("3 - Hard");
            Console.Write("Sinu valik: ");
            string choice = Console.ReadLine();

            return choice switch // seaded
            {
                "1" => new GameMode("Easy", 150, 10, 80, 25),
                "2" => new GameMode("Medium", 100, 20, 70, 20),
                "3" => new GameMode("Hard", 60, 30, 60, 18),
                _ => new GameMode("Easy", 150, 10, 80, 25)
            }; //
        }
    }
}