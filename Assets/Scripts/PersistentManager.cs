using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PersistentManager : MonoBehaviour
{
    public static PersistentManager Instance;

    public string PlayerName;

    public string BestPlayerName;
    public int HighScore;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadPlayerName();
        LoadHighScore();
    }

    public void SetUserName(string name)
    {
        PlayerName = name;
        SavePlayerName();
    }

    public void SetHighScore(int score)
    {
        HighScore = score;
        BestPlayerName = PlayerName;
    }

    [System.Serializable]
    class SaveHighScoreData
    {
        public string PlayerName;
        public int Score;
    }

    public void SaveHighScore()
    {
        SaveHighScoreData data = new SaveHighScoreData();
        data.PlayerName = BestPlayerName;
        data.Score = HighScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/highscore.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/highscore.json";

        if (File.Exists(path))
        {
            SaveHighScoreData data = JsonUtility.FromJson<SaveHighScoreData>(File.ReadAllText(path));

            HighScore = data.Score;
            BestPlayerName = data.PlayerName;
        } else
        {
            HighScore = 0;
            BestPlayerName = PlayerName;
        }
    }

    [System.Serializable]
    class SavePlayerNameData
    {
        public string PlayerName;
    }

    public void SavePlayerName()
    {
        SavePlayerNameData data = new SavePlayerNameData();
        data.PlayerName = PlayerName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/playerName.json", json);
    }

    public void LoadPlayerName()
    {
        string path = Application.persistentDataPath + "/playerName.json";

        if (File.Exists(path))
        {
            SavePlayerNameData data = JsonUtility.FromJson<SavePlayerNameData>(File.ReadAllText(path));

            PlayerName = data.PlayerName;
        }
    }
}
