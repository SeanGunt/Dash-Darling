using UnityEngine;

public class GameDataHolder : MonoBehaviour, IDataPersistence
{
    public static int money;
    public static int pistolDamage;
    public static float pistolFireRate;
    public static int pistolMagazine;
    public static float pistolReloadTime;
    public static bool slowAbilityPurchased;
    public static bool invincibilityAbilityPurchased;

    public void LoadData(GameData data)
    {
        money = data.money;
        pistolDamage = data.pistolDamage;
        pistolFireRate = data.pistolFireRate;
        pistolMagazine = data.pistolMagazine;
        pistolReloadTime = data.pistolReloadTime;
        slowAbilityPurchased = data.slowAbilityPurchased;
        invincibilityAbilityPurchased = data.invincibilityAbilityPurchased;
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
    }
}
