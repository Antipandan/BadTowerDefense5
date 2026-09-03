using System;
using UnityEngine;

public class TowerEvents : MonoBehaviour
{
    public Action OnStatChanged;
    
    public void PublishOnStatChanged()
    {
        OnStatChanged?.Invoke();
    }
}