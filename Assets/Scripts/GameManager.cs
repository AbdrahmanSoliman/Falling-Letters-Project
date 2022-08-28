using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // For WordSelector.cs when Player wins call the WordCoroutine();

    [SerializeField] TextMeshProUGUI wordText;

    public GameObject gameOverPanel;
    [SerializeField] TextMeshProUGUI bestScoree;
    public static bool isGamePlayable { get; set; } // for MoveDown to tell it when to start the game, and set for WordSelector & ClickAndDestroy
    public static bool isGameOver { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        StartCoroutine(WordCoroutine("First Time")); // String name doesn't matter at all, just used string argument(any other arguement won't matter) to overload the WordCoroutine() function
    }


    public IEnumerator WordCoroutine(string firstTime) // Wait 3 seconds to show the first word and then disappear the word and start the game
    {
        isGamePlayable = false;
        wordText.gameObject.SetActive(true);
        wordText.text = WordSelector.chosenWord;
        yield return new WaitForSeconds(3f);
        wordText.gameObject.SetActive(false);
        isGamePlayable = true;

    }
    public IEnumerator WordCoroutine() // Wait 1 second to show the new word and then disappear the word and resume the game for each word
    {
        isGamePlayable = false;
        wordText.gameObject.SetActive(true);
        wordText.text = WordSelector.chosenWord;
        yield return new WaitForSeconds(1f);
        wordText.gameObject.SetActive(false);
        isGamePlayable = true;
        
    }

    public void GameOver()
    {
        MenuManager.Instance.LoadBestScore();

        if (MenuManager.bestScore <= Score.score)
        {
            MenuManager.bestScore = Score.score;
            MenuManager.Instance.SaveBestScore();
            bestScoree.text = "Best score: " + MenuManager.bestScore;
        }
        else
        {
            bestScoree.text = "Best score: " + MenuManager.bestScore;
        }


        gameOverPanel.gameObject.SetActive(true);
    }


    public void PlayAgain()
    {
        //Score.Instance.ResetScore();
        gameOverPanel.SetActive(false);
        isGameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        //Score.Instance.ResetScore();
        gameOverPanel.SetActive(false);
        isGameOver = false;
        
        // + using SceneLoader.cs to load another scene, cause isChanged bool in its script is important to update the Menu Best Score text
    }



}
