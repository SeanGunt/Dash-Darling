using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DefenceTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        timerText.text = timer.ToString("n1");
    }
}
