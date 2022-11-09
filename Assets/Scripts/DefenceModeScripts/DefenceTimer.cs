using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DefenceTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    public static float timer;

    private void Awake()
    {
        timer = 0f;
        timerText.text = timer.ToString("n1");
    }
    private void Update()
    {
        timer += Time.deltaTime;
        timerText.text = timer.ToString("n1");
    }
}
