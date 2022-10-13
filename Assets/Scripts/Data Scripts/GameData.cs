[System.Serializable]
public class GameData
{
    public int pistolDamage;
    public int money;
    public bool isPistolDmgPurchasable, isPistolRoFPurchasable, isPistolAmmoPurchasable, isPistolReloadPurchasable;
    public bool slowAbilityPurchased;
    public bool isSlowAbilityPurchasable;
    public float pistolFireRate;
    public int pistolMagazine;
    public float pistolReloadTime;
    public GameData()
    {
        pistolDamage = 25;
        money = 0;
        isPistolDmgPurchasable = true;
        isPistolRoFPurchasable = true;
        isPistolAmmoPurchasable = true;
        isPistolReloadPurchasable = true;
        isSlowAbilityPurchasable = true;
        slowAbilityPurchased = false;
        pistolFireRate = 4f;
        pistolMagazine = 16;
        pistolReloadTime = 2f;
    }
}
