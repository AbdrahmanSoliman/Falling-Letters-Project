using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIHandler : MonoBehaviour
{
    public static MenuUIHandler Instance;
    public TextMeshProUGUI bestScoreText;

    private void Awake()
    {
        Instance = this;
    }
    public void BestScore()
    {
        MenuManager.Instance.LoadBestScore();
        //bestScoreText.gameObject.SetActive(true);
        bestScoreText.text = "Best score: " + MenuManager.bestScore;
    }
}
