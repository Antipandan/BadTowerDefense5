using UnityEngine;
using System;
using static Utility.Logging;

[CreateAssetMenu(fileName = "LeadBloonStats", menuName = "Scriptable Objects/Special Bloons/LeadBloonStats")]
public class LeadBloonStats : BloonStats
{
    [Tooltip("Sound to be played if a non damaging projectile hits this bloon")]
    [SerializeField] private AudioClip failedPopSound;

    protected override void OnEnable()
    {
        base.OnEnable();
        if (failedPopSound is null) LogNullReferenceError(nameof(failedPopSound), ErrorSeverity.Warning, this);
    }

    public AudioClip FailedPopSound { get => failedPopSound; }
}