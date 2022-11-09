using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DefenceScoreboard : MonoBehaviour
    {
    [SerializeField] private int maxScoreBoardEntries = 8;
    [SerializeField] private Transform highscoresHolderTransform;
    [SerializeField] private GameObject scoreboardEntryObject;

    [Header("Test")]
    [SerializeField] DefenceScoreboardEntryData testEntryData = new DefenceScoreboardEntryData();
    private string SavePath => $"{Application.persistentDataPath}/defencehighscores.json";

    private void Start()
    {
        DefenceScoreboardSaveData savedScores = GetSavedScores();

        UpdateUI(savedScores);

        SaveScores(savedScores);
    }

    [ContextMenu("Add Test Entry")]
    public void AddTestEntry()
    {
        AddEntry(testEntryData);
    }

    public void AddEntry(DefenceScoreboardEntryData scoreboardEntryData)
    {
        DefenceScoreboardSaveData savedScores = GetSavedScores();

        bool scoreAdded = false;

        for(int i = 0; i < savedScores.highscores.Count; i++)
        {
            if (scoreboardEntryData.entryTime > savedScores.highscores[i].entryTime)
            {
                savedScores.highscores.Insert(i, scoreboardEntryData);
                scoreAdded = true;
                break;
            }
        }

        if(!scoreAdded && savedScores.highscores.Count < maxScoreBoardEntries)
        {
            savedScores.highscores.Add(scoreboardEntryData);
        }

        if(savedScores.highscores.Count > maxScoreBoardEntries)
        {
            savedScores.highscores.RemoveRange(maxScoreBoardEntries, savedScores.highscores.Count - maxScoreBoardEntries);
        }

        UpdateUI(savedScores);
        SaveScores(savedScores);
    }

    private void UpdateUI(DefenceScoreboardSaveData savedScores)
    {
        foreach(Transform child in highscoresHolderTransform)
        {
                Destroy(child.gameObject);
        }

        foreach(DefenceScoreboardEntryData highscore in savedScores.highscores)
        {
            Instantiate(scoreboardEntryObject, highscoresHolderTransform).GetComponent<DefenceScoreboardEntryUI>().Initialise(highscore);
        }
    }

    private DefenceScoreboardSaveData GetSavedScores()
    {
        if(!File.Exists(SavePath))
        {
            File.Create(SavePath).Dispose();
            return new DefenceScoreboardSaveData();
        }

        using(StreamReader stream = new StreamReader(SavePath))
        {
            string json = stream.ReadToEnd();

            return JsonUtility.FromJson<DefenceScoreboardSaveData>(json);
        }
    }

    private void SaveScores(DefenceScoreboardSaveData scoreboardSaveData)
    {
        using(StreamWriter stream = new StreamWriter (SavePath))
        {
            string json = JsonUtility.ToJson(scoreboardSaveData, true);
            stream.Write(json);
        }
    }
}

