using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DefenceScoreboardEntryUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI entryTimeText = null;

    public void Initialise(DefenceScoreboardEntryData scoreboardEntryData)
    {
        entryTimeText.text = scoreboardEntryData.entryTime.ToString("n1");
    }
}
