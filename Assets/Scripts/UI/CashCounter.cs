using UnityEngine;
using System;
using TMPro;
using Utility;

public class CashCounter : UICounter
{

    private void Awake()
    {
        CheckReferences();
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        gameEvents.onMoneyChanged += ChangeMoneyAmount;
    }

    private void ChangeMoneyAmount()
    {
        if (UIText is null || Economy.Instance is null) return;
        UIText.text = $"$: {Economy.Instance.CurrentMoney}";
    }
    
}