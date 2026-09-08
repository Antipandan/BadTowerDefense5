/// <summary>
/// Interface is to be used on Towers that are to be upgraded. 
/// </summary>
public interface IUpgradable
{
    public void Upgrade(LevelPath path);
    
    public void Upgrade01();
    public void Upgrade02();
    public void Upgrade10();
    public void Upgrade20();

    public Level TowerLevel
    {
        get;
    }
}
