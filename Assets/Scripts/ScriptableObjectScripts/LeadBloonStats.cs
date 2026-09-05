using UnityEngine;
using System;

[CreateAssetMenu(fileName = "LeadBloonStats", menuName = "Scriptable Objects/Special Bloons/LeadBloonStats")]
public class LeadBloonStats : BloonStats
{
    [SerializeField] [Range(2, 100)] private uint childrenSpawned = 2;
    [SerializeField] private AudioClip failedPopSound;
}