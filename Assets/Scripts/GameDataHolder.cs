using UnityEngine;

public class GameDataHolder : MonoBehaviour, IDataPersistence
{
    public static int money;
    public static int pistolDamage;
    public static float pistolFireRate;
    public static int pistolMagazine;
    public static float pistolReloadTime;

    public void LoadData(GameData data)
    {
        money = data.money;
        pistolDamage = data.pistolDamage;
        pistolFireRate = data.pistolFireRate;
        pistolMagazine = data.pistolMagazine;
        pistolReloadTime = data.pistolReloadTime;
    }

    public void SaveData(GameData data)
    {
        data.money = money;
        data.pistolDamage = pistolDamage;
        data.pistolFireRate = pistolFireRate;
        data.pistolMagazine = pistolMagazine;
        data.pistolReloadTime = pistolReloadTime;
    }
}
