using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Random = UnityEngine.Random; // Because "using System;" also has his own System.Random and that caused ambigious

public class Letters : MonoBehaviour
{
    //char[] alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
    string[] alpha = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
    string[] customizedAlpha = { "A", "B", "C", "D", "E", "F", "V", "W", "X", "Y", "Z" }; // to be used for letters that are at the start/end of array like a,b,c or x,y,z

    [SerializeField] TextMeshProUGUI letterText;
    public static string chosenLetter;
    private int rangeBetLetters = 3; // for having Range that the chosenLetter is inside it.
    // Start is called before the first frame update
    void Start()
    {
        RandomLetters();
    }

    void RandomLetters()
    {
        string currentLetter = WordSelector.Instance.currentLetterOfWord;
        int letterPos = Array.IndexOf(alpha, currentLetter);
        if(letterPos <= 2 || letterPos >= 23) // if the letterPos(refering to currentLetter) is 0,1,2(a,b,c) or 23,24,25(x,y,z)
        {
            chosenLetter = customizedAlpha[Random.Range(0, customizedAlpha.Length)]; // then use the array customizedAlpha instead of alpha
            letterText.text = chosenLetter;
        }
        else
        {
            chosenLetter = alpha[Random.Range(letterPos - rangeBetLetters, letterPos + rangeBetLetters)]; // Randomly choose In range of the chosenLetter (Letter -3 ~ Letter +3)
            letterText.text = chosenLetter;
        }
    }
}
