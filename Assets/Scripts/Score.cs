using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public static Score Instance { get; set; }

    [SerializeField] TextMeshProUGUI scoreText;
    public static int score { get; private set; } // To be able to pass the score without setting its value, using private set; so functions in this script can use it
    // Start is called before the first frame update
    void Awake()
    {
        score = 0;
        Instance = this;
        scoreText.text = score.ToString();
    }

    public void UpdateScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

    public void ResetScore()
    {
        score = 0;
        scoreText.text = score.ToString();
    }
}
