using UnityEngine;
using UnityEngine.UI;

public class PistolUpgrades : MonoBehaviour, IDataPersistence
{
    [SerializeField] Button pistolDmgButton, pistolRofButton;
    private bool isPistolDmgPurchasable, isPistolRoFPurchasable;

    private void Awake()
    {
        if (isPistolDmgPurchasable && GameDataHolder.money >= 1000)
        {
            pistolDmgButton.GetComponent<Image>().color = Color.green;
        }
        else
        {
            pistolDmgButton.interactable = false;
            pistolDmgButton.GetComponent<Image>().color = Color.red;
        }

        if(isPistolRoFPurchasable && GameDataHolder.money >= 10000)
        {
            pistolRofButton.GetComponent<Image>().color = Color.green;
        }
        else
        {
            pistolRofButton.interactable = false;
            pistolRofButton.GetComponent<Image>().color = Color.red;
        }
    }
    public void LoadData(GameData data)
    {
        isPistolDmgPurchasable  = data.isPistolDmgPurchasable;
        isPistolRoFPurchasable = data.isPistolRoFPurchasable;
    }
    public void SaveData(GameData data)
    {
        data.isPistolDmgPurchasable = isPistolDmgPurchasable;
        data.isPistolRoFPurchasable = isPistolRoFPurchasable;
    }

    public void PurchasePistolDamage()
    {
        if (isPistolDmgPurchasable && GameDataHolder.money >= 1000)
        {
            isPistolDmgPurchasable = false;
            pistolDmgButton.GetComponent<Image>().color = Color.red;
            pistolDmgButton.interactable = false;
            GameDataHolder.money -= 1000;
            MoneyHolderUI.instance.moneyUI.text = GameDataHolder.money.ToString();
            GameDataHolder.pistolDamage += 10;
            Debug.Log("Damage is now equal to " + GameDataHolder.pistolDamage);
        }

    }

    public void PurchasePistolRoF()
    {
        if (isPistolRoFPurchasable && GameDataHolder.money >= 10000)
        {
            isPistolRoFPurchasable = false;
            pistolRofButton.GetComponent<Image>().color = Color.red;
            pistolRofButton.interactable = false;
            GameDataHolder.money -= 10000;
            MoneyHolderUI.instance.moneyUI.text = GameDataHolder.money.ToString();
            GameDataHolder.pistolFireRate += 2;
            Debug.Log("Pistol RoF is now equal to " + GameDataHolder.pistolFireRate);
        }
    }
}
