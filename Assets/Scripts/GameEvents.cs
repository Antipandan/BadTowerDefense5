using System;

public abstract class GameEvents
{
    public Action<uint> onMoneySpent;
    public Action<uint> onMoneyEarned;
    public Action<uint> onLivesLost;
    public Action onGameLost;
    public Action onGameWon;
}
