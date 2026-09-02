using System;

public struct Range
{
      private float min;
      private float max;
      
      public Range(float min, float max)
      {
          this.min = Math.Clamp(min, 0, 1);
          this.max = Math.Clamp(max, 0, 1);
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