using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataHolder : MonoBehaviour, IDataPersistence
{
    public static int money;
    public static int pistolDamage;

    public void LoadData(GameData data)
    {
        money = data.money;
        pistolDamage = data.pistolDamage;
    }

    public void SaveData(GameData data)
    {
        data.money = money;
        data.pistolDamage = pistolDamage;
    }
}
