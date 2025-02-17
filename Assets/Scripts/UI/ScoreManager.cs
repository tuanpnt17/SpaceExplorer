using System.IO;
using UnityEngine;

[System.Serializable]
public class ScoreData
{
    public int bestScore;
}

public class ScoreManager : MonoBehaviour
{
    private static string filePath; // Đường dẫn file JSON

    private void Awake()
    {
        filePath = Application.persistentDataPath + "/bestscore.json";
        Debug.Log("✅ File JSON sẽ được lưu tại: " + filePath);
    }

    /// <summary>
    /// Lưu điểm cao nhất vào file JSON
    /// </summary>
    public void SaveBestScore(int score)
    {
        ScoreData data = new ScoreData();

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            data = JsonUtility.FromJson<ScoreData>(json);
        }

        if (score > data.bestScore)
        {
            data.bestScore = score;
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(filePath, json);
        }
    }

    /// <summary>
    /// Tải điểm cao nhất từ file JSON
    /// </summary>
    public int LoadBestScore()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            ScoreData data = JsonUtility.FromJson<ScoreData>(json);
            return data.bestScore;
        }
        return 0;
    }
}
