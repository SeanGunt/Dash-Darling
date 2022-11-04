using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChocoCoinsManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinsText;
    public static int coins;

    private void Awake()
    {
        coins = 0;
        coinsText.text = coins.ToString();
    }

    private void Update()
    {
        coinsText.text = coins.ToString();
    }
}
