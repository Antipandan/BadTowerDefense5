using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameEvents : MonoBehaviour
{
    private static GameEvents instance;
    public event Action<long> onMoneySpent;
    public event Action<long> onMoneyEarned;
    public event Action onMoneyChanged;
    public event Action<uint> onLivesLost;
    public event Func<uint> currentMoney;
    public event Action onGameLost;
    public event Action onGameWon;
    public event Func<List<Collider2D>> onGetMapCollider2Ds;
    public event Action<bool, List<Collider2D>> onChangeMapCollider2DsState;
    public event Action onRequestRoundStart;
    public event Action onRoundStarted;

    private void Awake()
    {
        Time.timeScale = 10f;
        if (instance is null) instance = this;
        else Destroy(this);
    }

    public void PublishMoneySpent(long amount)
    {
        onMoneySpent?.Invoke(amount);
        onMoneyChanged?.Invoke();
    }

    public void PublishMoneyEarned(long amount)
    {
        onMoneyEarned?.Invoke(amount);
        onMoneyChanged?.Invoke();
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

    public uint PublishCurrentMoney()
    {
        // rider
        return currentMoney?.Invoke() ?? 0;
    }
    
    public List<Collider2D> PublishOnGetMapCollider2Ds()
    {
        return onGetMapCollider2Ds?.Invoke();
    }
    
    public void PublishChangeMapCollider2DsState(bool newState, List<Collider2D> colliders)
    {
        onChangeMapCollider2DsState?.Invoke(newState, colliders);
    }

    public void PublishOnRequestRoundStart()
    {
        onRequestRoundStart?.Invoke();
    }
    
    public void PublishOnRoundStarted()
    {
        onRoundStarted?.Invoke();
    }
    
}
