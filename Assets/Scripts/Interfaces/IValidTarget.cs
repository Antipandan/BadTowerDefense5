using UnityEngine;

// work around för att kunna hitta både bloons och towers inom en radie. Så fungerar det just nu i alla fall...
public interface IValidTarget
{
    public Transform Transform { get; }
}