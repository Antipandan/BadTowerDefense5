using System;
using UnityEngine;
using Random = System.Random;

[System.Serializable]
public class Range
{
      [SerializeField] private float min;
      [SerializeField] private float max;
      private Random random;
      public Range(float min, float max)
      {
          this.min = Math.Clamp(min, 0, 1);
          this.max = Math.Clamp(max, 0, 1);
      }

      public float GetRandomValue()
      {
          return (float)random.NextDouble() * (max - min) + min;
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