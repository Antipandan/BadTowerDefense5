using System;
using UnityEngine;

public class Dart : Projectile
{
    private void OnHit()
    {
        throw new NotImplementedException();       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        OnHit();
    }
}