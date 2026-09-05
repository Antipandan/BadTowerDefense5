using UnityEngine;
using System;

[CreateAssetMenu(fileName = "BlackBloonStats", menuName = "Scriptable Objects/Special Bloons/BlackBloonStats")]
public class BlackBloonStats : BloonStats
{
    [SerializeField] [Range(2, 100)] private uint childrenSpawned = 2;
}