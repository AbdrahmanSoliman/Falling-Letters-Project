using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    public static int bestScore;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadBestScore();
    }
    private void Start()
    {
        if(bestScore > 0) // In order to only enable best score text when the player at least played one game to show his best score, instead of showing best score: 0
        {
            MenuUIHandler.Instance.BestScore();
            MenuUIHandler.Instance.bestScoreText.gameObject.SetActive(true);
        }
    }
    private void Update()
    {
        if (bestScore > 0 && SceneManager.GetActiveScene().name == "MenuScene" && SceneLoader.isChanged) // to update only once using isChanged boolean an only update when in menu scene also the best score must be > 0
        {
            MenuUIHandler.Instance.BestScore();
            MenuUIHandler.Instance.bestScoreText.gameObject.SetActive(true);
            SceneLoader.isChanged = false;
        }
    }

    [System.Serializable]

    class SaveData
    {
        public int bestScore;
    }

    public void SaveBestScore()
    {
        SaveData savedata = new SaveData();
        savedata.bestScore = bestScore;

        string json = JsonUtility.ToJson(savedata);
        File.WriteAllText(Application.persistentDataPath + "/savedata.json", json);
    }

    public void LoadBestScore()
    {
        string path = Application.persistentDataPath + "/savedata.json";
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            bestScore = data.bestScore;
        }
    }
}
