using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameEvents : MonoBehaviour
{
    public Action<uint> onMoneySpent;
    public Action<uint> onMoneyEarned;
    public Action<uint> onLivesLost;
    public Func<uint> currentMoney;
    public Action onGameLost;
    public Action onGameWon;
    public Func<List<Collider2D>> onGetMapCollider2Ds;
    public Action<bool, List<Collider2D>> onChangeMapCollider2DsState;
    
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
}
