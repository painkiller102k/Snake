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
        if (CurrentLives > 0) // rohken 
            CurrentLives--; // Kui on, vähendame 1 võrra !
    }

    public void AddLife(int amount = 1)
    {
        CurrentLives = Math.Min(MaxLives, CurrentLives + amount); // kui praegune tervis ei ole suurem kui 2, lisame 1 hp
    }

    public void ResetLives()
    {
        CurrentLives = MaxLives; // tervis reset mängu alustamiseks
    }

    public bool IsAlive()
    {
        return CurrentLives > 0; // kui tervise rohken või 0 me oleme elus
    } // vähem kui 0 me surnud
}