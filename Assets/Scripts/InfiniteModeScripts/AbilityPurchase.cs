using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityPurchase : MonoBehaviour, IDataPersistence
{
    [SerializeField] Button purchaseSlowAbilityButton, purchaseInvincibilityButton, purchaseBombAbilityButton, purchaseInfiniteAmmoButton;
    [SerializeField] TextMeshProUGUI slowText, invincText, bombText, AmmoText;
    private bool isSlowAbilityPurchasable, isInvincibilityAbilityPurchasable, isBombAbilityPurchasable, isInfiniteAmmoPurchasable;

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
            if(!isSlowAbilityPurchasable)
            {
                slowText.text = "Purchased!";
            }
        }

        if (isInvincibilityAbilityPurchasable && GameDataHolder.money >= 5000)
        {
            purchaseInvincibilityButton.GetComponent<Image>().color = Color.green;
        }
        else
        {
            purchaseInvincibilityButton.interactable = false;
            purchaseInvincibilityButton.GetComponent<Image>().color = Color.red;
            if(!isInvincibilityAbilityPurchasable)
            {
                invincText.text = "Purchased!";
            }
        }

        if (isBombAbilityPurchasable && GameDataHolder.money >= 5000)
        {
            purchaseBombAbilityButton.GetComponent<Image>().color = Color.green;
        }
        else
        {
            purchaseBombAbilityButton.interactable = false;
            purchaseBombAbilityButton.GetComponent<Image>().color = Color.red;
            if(!isBombAbilityPurchasable)
            {
                bombText.text = "Purchased!";
            }
        }

        if (isInfiniteAmmoPurchasable && GameDataHolder.money >= 25000)
        {
            purchaseInfiniteAmmoButton.GetComponent<Image>().color = Color.green;
        }
        else
        {
            purchaseInfiniteAmmoButton.interactable = false;
            purchaseInfiniteAmmoButton.GetComponent<Image>().color = Color.red;
            if(!isInfiniteAmmoPurchasable)
            {
                AmmoText.text = "Purchased!";
            }
        }

    }

    public void LoadData(GameData data)
    {
        isSlowAbilityPurchasable = data.isSlowAbilityPurchasable;
        isInvincibilityAbilityPurchasable = data.isInvincibilityAbilityPurchasable;
        isBombAbilityPurchasable = data.isBombAbilityPurchasable;
        isInfiniteAmmoPurchasable = data.isInfiniteAmmoAbilityPurchasable;
    }

    public void SaveData(GameData data)
    {
        data.isSlowAbilityPurchasable = isSlowAbilityPurchasable;
        data.isInvincibilityAbilityPurchasable = isInvincibilityAbilityPurchasable;
        data.isBombAbilityPurchasable = isBombAbilityPurchasable;
        data.isInfiniteAmmoAbilityPurchasable = isInfiniteAmmoPurchasable;
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

    public void PurchaseBombAbility()
    {
        if (isBombAbilityPurchasable && GameDataHolder.money >= 5000)
        {
            isBombAbilityPurchasable = false;
            purchaseBombAbilityButton.GetComponent<Image>().color = Color.red;
            purchaseBombAbilityButton.interactable = false;
            GameDataHolder.money -= 5000;
            MoneyHolderUI.instance.moneyUI.text = GameDataHolder.money.ToString();
            GameDataHolder.bombAbilityPurchased = true;
            Debug.Log("Bomb ability is now available");
        }
    }

    public void PurchaseInfiniteAmmo()
    {
        if (isInfiniteAmmoPurchasable && GameDataHolder.money >= 25000)
        {
            isInfiniteAmmoPurchasable = false;
            purchaseInfiniteAmmoButton.GetComponent<Image>().color = Color.red;
            purchaseInfiniteAmmoButton.interactable = false;
            GameDataHolder.money -= 25000;
            MoneyHolderUI.instance.moneyUI.text = GameDataHolder.money.ToString();
            GameDataHolder.infiniteAmmoPurchased = true;
            Debug.Log("Infinite Ammo ability is now available");
        }
    }
}
