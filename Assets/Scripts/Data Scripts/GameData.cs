using UnityEngine;
using System;

[System.Serializable]
public class GameData
{
    public int pistolDamage;
    public int money;
    
    public GameData()
    {
        this.pistolDamage = 25;
        this.money = 0;
    }
}
