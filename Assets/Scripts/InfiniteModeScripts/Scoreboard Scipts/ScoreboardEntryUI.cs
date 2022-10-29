using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreboardEntryUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI entryTimeText = null;

    public void Initialise(ScoreboardEntryData scoreboardEntryData)
    {
        entryTimeText.text = scoreboardEntryData.entryTime.ToString("n1");
    }
}
