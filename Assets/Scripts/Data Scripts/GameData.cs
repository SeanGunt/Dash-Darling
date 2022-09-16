[System.Serializable]
public class GameData
{
    public int pistolDamage;
    public int money;
    public bool purchasable;
    public float pistolFireRate;
    public int pistolMagazine;
    public float pistolReloadTime;
    
    public GameData()
    {
        pistolDamage = 25;
        money = 0;
        purchasable = true;
        pistolFireRate = 4f;
        pistolMagazine = 16;
        pistolReloadTime = 2f;
    }
}
