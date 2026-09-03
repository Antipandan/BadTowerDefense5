using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Upgrade
{
     [SerializeField] private string upgradeDescription = "Placeholder Text";
     [SerializeField] private uint upgradeCost = 100;
     public UnityEvent<IUpgradable> upgradeFunction;
     
     public string UpgradeDescription
     {
        get => upgradeDescription;
     }
     
     public uint UpgradeCost
     {
        get => upgradeCost;
     }
}
