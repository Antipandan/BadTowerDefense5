/// <summary>
/// Interface is to be used on objects that are able to take damage
/// from projectiles most commonly thrown by monkeys / AttackTowers
/// </summary>
public interface IDamageAble
{
    public void TakeDamage(Projectile projectile);
    
    public uint CurrentHealth { get; }
}
