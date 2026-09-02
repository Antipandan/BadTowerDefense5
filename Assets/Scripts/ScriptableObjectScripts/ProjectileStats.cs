using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileStats", menuName = "Scriptable Objects/ProjectileStats")]
public class ProjectileStats : ScriptableObject
{
    [SerializeField] [Range(0f, 100f)] private float travelSpeed = 1f;
    [SerializeField] private DamageTypes damageType = DamageTypes.Regular;
}