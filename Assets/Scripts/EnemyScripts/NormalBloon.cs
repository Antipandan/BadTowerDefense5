public class NormalBloon : Bloon<BloonStats>
{
    public override void TakeDamage(Projectile projectile)
    {
        audioSource.Play();
        Destroy(gameObject);
    }
}