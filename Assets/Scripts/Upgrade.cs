using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Upgrade
{
    [Tooltip("Description of upgrade")]
    [SerializeField] private string upgradeDescription = "Placeholder Text";
    [Tooltip("How much should upgrade cost?")]
    [SerializeField] private uint upgradeCost = 100;
    [Tooltip("Event to attach upgrade function to. This can be done in code or in Unity")]
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
