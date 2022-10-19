[System.Serializable]
public class GameData
{
    public int pistolDamage;
    public int money;
    public bool isPistolDmgPurchasable, isPistolRoFPurchasable, isPistolAmmoPurchasable, isPistolReloadPurchasable;
    public bool slowAbilityPurchased, invincibilityAbilityPurchased, bombAbilityPurchased, infiniteAmmoPurchased;
    public bool isSlowAbilityPurchasable, isInvincibilityAbilityPurchasable, isBombAbilityPurchasable, isInfiniteAmmoAbilityPurchasable;
    public float pistolFireRate;
    public int pistolMagazine;
    public float pistolReloadTime;
    public GameData()
    {
        pistolDamage = 25;
        money = 0;
        pistolFireRate = 4f;
        pistolMagazine = 16;
        pistolReloadTime = 2f;

        isPistolDmgPurchasable = true;
        isPistolRoFPurchasable = true;
        isPistolAmmoPurchasable = true;
        isPistolReloadPurchasable = true;

        isSlowAbilityPurchasable = true;
        isInvincibilityAbilityPurchasable = true;
        isBombAbilityPurchasable = true;
        isInfiniteAmmoAbilityPurchasable = true;

        invincibilityAbilityPurchased = false;
        slowAbilityPurchased = false;
        bombAbilityPurchased = false;
        infiniteAmmoPurchased = false;
    }
}
