using System;
using UnityEngine;
using Random = System.Random;

[System.Serializable]
public struct Range
{
    [Tooltip("Minimum value of this range")]
    [SerializeField] private float min;
    [Tooltip("Maximum value of this range")]
    [SerializeField] private float max;
    public Range(float min, float max)
    {
        this.min = Math.Clamp(min, 0, 1);
        this.max = Math.Clamp(max, 0, 1);
    }

    public float GetRandomValue(Random rand)
    {
        return (float)rand.NextDouble() * (max - min) + min;
    }
      
    public float Min
    {
        get => min;
    }
      
    public float Max
    {
        get => max;
    }
}