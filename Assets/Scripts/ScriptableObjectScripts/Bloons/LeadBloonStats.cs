using UnityEngine;
using System;

[CreateAssetMenu(fileName = "LeadBloonStats", menuName = "Scriptable Objects/Special Bloons/LeadBloonStats")]
public class LeadBloonStats : BloonStats
{
    [Tooltip("How many children should spawn if bloon is popped?")]
    [SerializeField] [Range(2, 100)] private uint childrenSpawned = 2;
    [Tooltip("Sound to be played if a non damaging projectile hits this bloon")]
    [SerializeField] private AudioClip failedPopSound;
}