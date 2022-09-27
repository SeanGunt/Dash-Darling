using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class Leaderboard : MonoBehaviour
{
    private Transform container;
    private Transform template;
    private List<Transform> highscoreEntryTransformList;

    private void Awake()
    {
        container = transform.Find("Container");
        template = container.Find("Template");

        template.gameObject.SetActive(false);

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
            {
                if (highscores.highscoreEntryList[j].time > highscores.highscoreEntryList[i].time)
                {
                    HighScoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp;
                }
            }
        }
        highscoreEntryTransformList = new List<Transform>();
        foreach (HighScoreEntry highscoreEntry in highscores.highscoreEntryList)
        {
            CreateHighScoreEntryTransform(highscoreEntry, container, highscoreEntryTransformList);
        }
    }
    
    private void CreateHighScoreEntryTransform(HighScoreEntry highscoreEntry, Transform container2, List<Transform> transformList)
    {
        float templateHeight = 70f;
        Transform entryTransform = Instantiate(template, container2);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        float time = highscoreEntry.time;
        entryTransform.Find("Time").GetComponent<TextMeshProUGUI>().text = time.ToString("n1");
        entryTransform.Find("Rank").GetComponent<TextMeshProUGUI>().text = rank.ToString();

        transformList.Add(entryTransform);
    }

    public void AddHighscoreEntry(float time)
    {
        HighScoreEntry highscoreEntry = new HighScoreEntry { time = time };

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        highscores.highscoreEntryList.Add(highscoreEntry);
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }
    private class Highscores 
    {
        public List<HighScoreEntry> highscoreEntryList;
    }

    [System.Serializable]
    private class HighScoreEntry
    {
        public float time;
    }
}
