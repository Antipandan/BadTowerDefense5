using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WhiteBloonStats", menuName = "Scriptable Objects/Special Bloons/WhiteBloonStats")]
public sealed class WhiteBloonStats : BloonStats
{
    [SerializeField] [Range(2, 100)] private uint childrenSpawned = 2;
}