using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public class GameData
{
    public int pistolDamage;
    public int money;
    public bool purchasable;
    
    public GameData()
    {
        pistolDamage = 25;
        money = 0;
        purchasable = true;
    }
}
