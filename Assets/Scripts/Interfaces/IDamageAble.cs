public interface IDamageAble
{
    public void TakeDamage(Projectile projectile);
    
    public uint CurrentHealth { get; }
}
