using UnityEngine;
using UnityEngine.UI;

public class PistolUpgrades : MonoBehaviour, IDataPersistence
{
    [SerializeField] Button pistolDmgButton, pistolRofButton, pistolAmmoButton, pistolReloadButton;
    private bool isPistolDmgPurchasable, isPistolRoFPurchasable, isPistolAmmoPurchasable, isPistolReloadPurchasable;

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

        if(isPistolRoFPurchasable && GameDataHolder.money >= 1000)
        {
            pistolRofButton.GetComponent<Image>().color = Color.green;
        }
        else
        {
            pistolRofButton.interactable = false;
            pistolRofButton.GetComponent<Image>().color = Color.red;
        }

        if(isPistolAmmoPurchasable && GameDataHolder.money >= 100)
        {
            pistolAmmoButton.GetComponent<Image>().color = Color.green;
        }
        else
        {
            pistolAmmoButton.interactable = false;
            pistolAmmoButton.GetComponent<Image>().color = Color.red;
        }
        
        if(isPistolReloadPurchasable && GameDataHolder.money >= 100)
        {
            pistolReloadButton.GetComponent<Image>().color = Color.green;
        }
        else
        {
            pistolReloadButton.interactable = false;
            pistolReloadButton.GetComponent<Image>().color = Color.red;
        }
    }
    public void LoadData(GameData data)
    {
        isPistolDmgPurchasable  = data.isPistolDmgPurchasable;
        isPistolRoFPurchasable = data.isPistolRoFPurchasable;
        isPistolAmmoPurchasable = data.isPistolAmmoPurchasable;
        isPistolReloadPurchasable = data.isPistolReloadPurchasable;
    }
    public void SaveData(GameData data)
    {
        data.isPistolDmgPurchasable = isPistolDmgPurchasable;
        data.isPistolRoFPurchasable = isPistolRoFPurchasable;
        data.isPistolAmmoPurchasable = isPistolAmmoPurchasable;
        data.isPistolReloadPurchasable = isPistolReloadPurchasable;
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
        if (isPistolRoFPurchasable && GameDataHolder.money >= 1000)
        {
            isPistolRoFPurchasable = false;
            pistolRofButton.GetComponent<Image>().color = Color.red;
            pistolRofButton.interactable = false;
            GameDataHolder.money -= 1000;
            MoneyHolderUI.instance.moneyUI.text = GameDataHolder.money.ToString();
            GameDataHolder.pistolFireRate += 2;
            Debug.Log("Pistol RoF is now equal to " + GameDataHolder.pistolFireRate);
        }
    }

    public void PurchasePistolAmmo()
    {
        if (isPistolAmmoPurchasable && GameDataHolder.money >= 1000)
        {
            isPistolAmmoPurchasable = false;
            pistolAmmoButton.GetComponent<Image>().color = Color.red;
            pistolAmmoButton.interactable = false;
            GameDataHolder.money -= 1000;
            MoneyHolderUI.instance.moneyUI.text = GameDataHolder.money.ToString();
            GameDataHolder.pistolMagazine += 4;
            Debug.Log("Pistol RoF is now equal to " + GameDataHolder.pistolFireRate);
        }
    }

    public void PurchasePistolReload()
    {
        if(isPistolReloadPurchasable && GameDataHolder.money >= 1000)
        {
            isPistolReloadPurchasable = false;
            pistolReloadButton.GetComponent<Image>().color = Color.red;
            pistolReloadButton.interactable = false;
            GameDataHolder.money -= 1000;
            MoneyHolderUI.instance.moneyUI.text = GameDataHolder.money.ToString();
            GameDataHolder.pistolReloadTime -= 1.0f;
            Debug.Log("Pistol Reload Time is now " + GameDataHolder.pistolReloadTime);
        }
    }
}
