using UnityEngine;

public class PackedBloon : Bloon
{
    // detta kanske inte är det bästa sättet att hantera en speciell
    // bloon på men det borde fungera gentemot längre tid att implementera!
    [Tooltip("How many bloons should spawn when parentBloonPopped")]
    [SerializeField] [Range(2, 100)] private uint nrBloonsSpawned = 2;

    protected override void OnLayerPopped()
    {
        base.OnLayerPopped();
    }
}