using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI totalMoneyText;
    void Awake()
    {
        totalMoneyText.text = GameDataHolder.money.ToString();
    }
}
