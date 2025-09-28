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
            CurrentLives--; // Kui on, vähendame 1 võrra !
    }

    public void AddLife(int amount = 1)
    {
        CurrentLives = Math.Min(MaxLives, CurrentLives + amount);
    }

    public void ResetLives()
    {
        CurrentLives = MaxLives; // lives reset 
    }

    public bool IsAlive()
    {
        return CurrentLives > 0;
    }
}