using System;

public abstract class GameEvents
{
    public Action<uint> onMoneySpent;
    public Action<uint> onMoneyEarned;
    public Action<uint> onLivesLost;
    public Action onGameLost;
    public Action onGameWon;
    
    public void PublishMoneySpent(uint amount)
    {
        onMoneySpent?.Invoke(amount);
    }

    public void PublishMoneyEarned(uint amount)
    {
        onMoneyEarned?.Invoke(amount);
    }
    
    public void PublishLivesLost(uint amount)
    {
        onLivesLost?.Invoke(amount);
    }

    public void PublishGameLost()
    {
        onGameLost?.Invoke();
    }

    public void PublishGameWon()
    {
        onGameWon?.Invoke();
    }
}
