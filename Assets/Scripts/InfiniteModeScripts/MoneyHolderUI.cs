using UnityEngine;
using TMPro;

public class MoneyHolderUI : MonoBehaviour
{
    public static MoneyHolderUI instance {get; private set;}
    public TextMeshProUGUI moneyUI;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one MoneyHolder manager in the scene");
        }
        instance = this;
    }

    private void Start()
    {
        moneyUI.text = GameDataHolder.money.ToString();
    }
}
