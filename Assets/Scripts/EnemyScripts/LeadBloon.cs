using UnityEngine;

public class LeadBloon : Bloon<LeadBloonStats>
{
    public override void TakeDamage(Projectile projectile)
    {
        if (projectile is null) return;
        if (stats.ResistantDamageTypes.Contains(projectile.Stats.DamageType))
        {
            GameObject obj = Instantiate(soundPlayerPrefab);
            SoundPlayer player = obj.GetComponent<SoundPlayer>();
            player?.PlaySound(stats.FailedPopSound);
        }
        else
        {
            base.TakeDamage(projectile);
        }
    }
}