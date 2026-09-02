public interface IDamageAble
{
    public void TakeDamage(uint amount);
    
    public uint CurrentHealth { get; }
}
