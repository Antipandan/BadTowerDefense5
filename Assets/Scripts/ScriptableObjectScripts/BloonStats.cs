using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BloonStats", menuName = "Scriptable Objects/BloonStats")]
public class BloonStats : ScriptableObject
{
    [SerializeField] private uint healthToPop = 1;
    [SerializeField] [Range(0f, 100f)]private float movementSpeed = 1f;
    [SerializeField] private List<DamageTypes> resistantDamageTypes;
}
