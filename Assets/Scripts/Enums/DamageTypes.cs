using System;
using UnityEngine;

/// <summary>
/// Different damage types. Only supports circa 32 unique damage types.
/// Use binary representation so that several damage types can be combined into a single uint via bit manipulation
/// </summary>
public enum DamageTypes
{
    Regular = 0b1,
    Explosion = 0b10,
    Freeze = 0b100,
}