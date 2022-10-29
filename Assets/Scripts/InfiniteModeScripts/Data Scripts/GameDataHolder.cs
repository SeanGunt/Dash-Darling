using UnityEngine;

public class GameDataHolder : MonoBehaviour, IDataPersistence
{
    public static int money;
    public static int pistolDamage;
    public static int turretDamage;
    public static float pistolFireRate;
    public static int pistolMagazine;
    public static float pistolReloadTime;
    public static bool slowAbilityPurchased;
    public static bool invincibilityAbilityPurchased;
    public static bool bombAbilityPurchased;
    public static bool infiniteAmmoPurchased;

    public void LoadData(GameData data)
    {
        money = data.money;
        pistolDamage = data.pistolDamage;
        pistolFireRate = data.pistolFireRate;
        pistolMagazine = data.pistolMagazine;
        pistolReloadTime = data.pistolReloadTime;
        slowAbilityPurchased = data.slowAbilityPurchased;
        invincibilityAbilityPurchased = data.invincibilityAbilityPurchased;
        bombAbilityPurchased = data.bombAbilityPurchased;
        infiniteAmmoPurchased = data.infiniteAmmoPurchased;
        turretDamage = data.turretDamage;
    }

    public void SaveData(GameData data)
    {
        data.money = money;
        data.pistolDamage = pistolDamage;
        data.pistolFireRate = pistolFireRate;
        data.pistolMagazine = pistolMagazine;
        data.pistolReloadTime = pistolReloadTime;
        data.slowAbilityPurchased = slowAbilityPurchased;
        data.invincibilityAbilityPurchased = invincibilityAbilityPurchased;
        data.bombAbilityPurchased = bombAbilityPurchased;
        data.infiniteAmmoPurchased = infiniteAmmoPurchased;
        data.turretDamage = turretDamage;
    }
}
