using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityPurchase : MonoBehaviour, IDataPersistence
{
    [SerializeField] Button purchaseSlowAbilityButton;
    private bool isSlowAbilityPurchasable;

    private void Update()
    {
        if (isSlowAbilityPurchasable && GameDataHolder.money >= 2500)
        {
            purchaseSlowAbilityButton.GetComponent<Image>().color = Color.green;
        }
        else
        {
            purchaseSlowAbilityButton.interactable = false;
            purchaseSlowAbilityButton.GetComponent<Image>().color = Color.red;
        }

    }

    public void LoadData(GameData data)
    {
        isSlowAbilityPurchasable = data.isSlowAbilityPurchasable;
    }

    public void SaveData(GameData data)
    {
        data.isSlowAbilityPurchasable = isSlowAbilityPurchasable;
    }

    public void PurchaseSlowAbility()
    {
        if (isSlowAbilityPurchasable && GameDataHolder.money >= 2500)
        {
            isSlowAbilityPurchasable = false;
            purchaseSlowAbilityButton.GetComponent<Image>().color = Color.red;
            purchaseSlowAbilityButton.interactable = false;
            GameDataHolder.money -= 2500;
            MoneyHolderUI.instance.moneyUI.text = GameDataHolder.money.ToString();
            GameDataHolder.slowAbilityPurchased = true;
            Debug.Log("Slow ability is now available");
        }

    }
}
