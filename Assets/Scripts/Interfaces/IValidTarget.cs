using UnityEngine;

/// <summary>
/// Interface to be used to find valid classes inside a radius. Functions as a generic identifier
/// </summary>
public interface IValidTarget
{
    public Transform Transform { get; }
}