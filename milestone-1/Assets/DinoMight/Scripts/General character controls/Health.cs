public class Health
{
    // public int maxHealth { get; private set; }
    // public int currentHealth { get; private set; }

    // // Constructor
    // public Health(int max)
    // {
    //     maxHealth = max;
    //     currentHealth = max;
    // }

    public static int MinusHealth(int currentAmount, int amount)
    {
        currentAmount -= amount;
        if (currentAmount < 0)
        {
            currentAmount = 0;
        }
        return currentAmount;
    }

    public static int AddHealth(int maxAmount, int currentAmount, int amount)
    {
        currentAmount += amount;
        if (currentAmount > maxAmount)
        {
            currentAmount = maxAmount;
        }
        return currentAmount;
    }
}
