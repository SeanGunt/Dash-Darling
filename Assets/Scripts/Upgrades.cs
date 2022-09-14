using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour, IDataPersistence
{
    [SerializeField] Button button;
    private bool purchasable;

    private void Awake()
    {
        if (purchasable && GameDataHolder.money >= 1000)
        {
            button.GetComponent<Image>().color = Color.green;
        }
        else
        {
            button.interactable = false;
            button.GetComponent<Image>().color = Color.red;
        }
    }
    public void LoadData(GameData data)
    {
        purchasable = data.purchasable;
    }
    public void SaveData(GameData data)
    {
        data.purchasable= purchasable;
    }

    public void PurchasePistolDamage()
    {
        if (purchasable && GameDataHolder.money >= 1000)
        {
            purchasable = false;
            button.interactable = false;
            GameDataHolder.money -= 1000;
            GameDataHolder.pistolDamage += 5;
            Debug.Log("Damage is now equal to " + GameDataHolder.pistolDamage);
        }

    }
}
