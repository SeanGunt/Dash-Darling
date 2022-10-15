using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityPurchase : MonoBehaviour, IDataPersistence
{
    [SerializeField] Button purchaseSlowAbilityButton, purchaseInvincibilityButton;
    private bool isSlowAbilityPurchasable, isInvincibilityAbilityPurchasable;

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

        if (isInvincibilityAbilityPurchasable && GameDataHolder.money >= 5000)
        {
            purchaseInvincibilityButton.GetComponent<Image>().color = Color.green;
        }
        else
        {
            purchaseInvincibilityButton.interactable = false;
            purchaseInvincibilityButton.GetComponent<Image>().color = Color.red;
        }

    }

    public void LoadData(GameData data)
    {
        isSlowAbilityPurchasable = data.isSlowAbilityPurchasable;
        isInvincibilityAbilityPurchasable = data.isInvincibilityAbilityPurchasable;
    }

    public void SaveData(GameData data)
    {
        data.isSlowAbilityPurchasable = isSlowAbilityPurchasable;
        data.isInvincibilityAbilityPurchasable = isInvincibilityAbilityPurchasable;
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

    public void PurcahseInvincibilityAbility()
    {
        if (isInvincibilityAbilityPurchasable && GameDataHolder.money >= 5000)
        {
            isInvincibilityAbilityPurchasable = false;
            purchaseInvincibilityButton.GetComponent<Image>().color = Color.red;
            purchaseInvincibilityButton.interactable = false;
            GameDataHolder.money -= 5000;
            MoneyHolderUI.instance.moneyUI.text = GameDataHolder.money.ToString();
            GameDataHolder.invincibilityAbilityPurchased = true;
            Debug.Log("Invincibility ability is now available");
        }
    }
}
