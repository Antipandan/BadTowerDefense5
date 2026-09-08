using UnityEngine;
using System;

[CreateAssetMenu(fileName = "LeadBloonStats", menuName = "Scriptable Objects/Special Bloons/LeadBloonStats")]
public class LeadBloonStats : BloonStats
{
    [Tooltip("Sound to be played if a non damaging projectile hits this bloon")]
    [SerializeField] private AudioClip failedPopSound;

    protected override void OnEnable()
    {
        base.OnEnable();
        if (failedPopSound is null) Utility.Utility.LogWarningStandardNullReference(failedPopSound);
    }

    public AudioClip FailedPopSound { get => failedPopSound; }
}