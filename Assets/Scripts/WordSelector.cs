using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordSelector : MonoBehaviour
{
    public static WordSelector Instance;
    public static string chosenWord { get; private set; }
    protected string[] chosenWordLetters;
    private static int indexLetter = 0;

    public string currentLetterOfWord { get; private set; }

    private int maxLettersLong = 6; // to use it in chosenWordLetters array to have slots for Four Letters or Five or Six.. instead of using chosenWord.Length at first as it will result of having array of 3 slots only!

    public Animator animWordCompletion;
    //[SerializeField] TextMeshProUGUI wordCompletionText;
    private void Awake()
    {
        Instance = this;

        MoveDown.fallingSpeed = 4;

        chosenWord = ThreeLettersDictionary();
        Debug.Log(chosenWord);
        chosenWordLetters = new string[maxLettersLong]; // for now there will be only up to six letters long, read maxLettersLong to understand tho
        LettersOfWord();
        currentLetterOfWord = chosenWordLetters[0];
    }


    void LettersOfWord()
    {
        int i = 0;
        foreach (char c in chosenWord)
        {
            chosenWordLetters[i] = c.ToString();
            i++;
        }
    }

    public void CurrentLetter(int x, string letterDestroyed) // For ClickAndDestroy.cs
    {
        Debug.Log("currentLetterOfWord: " + currentLetterOfWord + " // While chosenLetter: " + letterDestroyed);
        if (currentLetterOfWord == letterDestroyed) // if the player hit the right letter
        {
            indexLetter += x;
            if (indexLetter < chosenWord.Length) // if the word didn't finish
            {
                currentLetterOfWord = chosenWordLetters[indexLetter];
            }
            else //finished!
            {
                //Player finished the word, so AddScore() or something like that and get him another word
                Debug.Log("You winn!"); // For now; will be deleted later.
                animWordCompletion.SetBool("hasWon", true); // Show +1 word completion, by playing fading in & out anim state.
                Score.Instance.UpdateScore(); // For completing a word!

                chosenWord = WordLongPicker(); //WordLongPicker();
                Debug.Log(chosenWord);

                StartCoroutine(GameManager.Instance.WordCoroutine()); // WordCoroutine bta3 el GameManager for 1 second not 3 seconds as during the game not at the beginning

                // Will use wordCompletionText.gameObject.SetActive(false) in WordLongPicker() disabling it so when we reactivate it again the animation plays
                // as it is the best place to put it for now, as LettersOfWord() checks -> chosenWord that calls -> WordLongPicker();

                LettersOfWord(); // setting the LettersOfWord to the "new" word.
                indexLetter = 0;  // resetting it is a must, as it is static it will continue from where it stopped instead from 0 if we didn't reset it
                currentLetterOfWord = chosenWordLetters[indexLetter]; // resetting it as well, to look at first letter.



            }
        }
        else // player hit the wrong letter
        {
            // gameover() or something like that
            indexLetter = 0;
            GameManager.isGamePlayable = false;
            GameManager.isGameOver = true;
            GameManager.Instance.GameOver();
        }
    }

    public void CurrentLetter(string letterDestroyed) // For DestroyOutOfBounds.cs
    {
        if(currentLetterOfWord == letterDestroyed)
        {
            indexLetter = 0;
            // gameover() or something like that
            GameManager.isGamePlayable = false;
            GameManager.isGameOver = true;
            GameManager.Instance.GameOver();
            Debug.Log("Letter destroyed itself, you lost!");
            //Score.Instance.ResetScore();
        }
    }

    string ThreeLettersDictionary()
    {
        string[] threeLettersWord = new string[] { "AXE", "PET", "BEE", "DOG", "NUN", "JAM", "AGE", "AGO", "AIR", "AIM", "APE", "ART",
        "EGO" , "EXE", "FAN", "FAM", "FAR", "FIT", "GUM", "GUY", "HAM", "HAT", "HOT", "HEY", "HIM", "HER", "DID", "DRY", "DIE", "DOT",
        "HOW", "LAY", "LOW", "MAD", "MAP", "MAX", "MIN", "OAK", "ODD", "PAD", "PAY", "PEN", "RED", "SUN", "SIT", "SUB", "TED", "TEE",
        "THE", "WAX", "WHO", "YOU", "EAR", "ECO", "EGG", "END", "EYE", "FLY", "FUN", "FRY", "GAP", "GAG", "GYM", "GUN", "HMM", "ICE",
        "INK", "ION", "INN", "ICY", "ILL", "JAW", "JET", "JOB", "KEY", "LAB", "LAD", "LOG", "LOW", "LIE", "MOM", "MOD", "MUG", "MUM",
        "NAP", "NET", "NUT", "OIL", "OAT", "ORB", "OOF", "OWN", "PEW", "PIE", "PIC", "PIN", "RUN", "TAN", "TAG", "VAN", "VOW", "VIA",
        "WAX", "WAR", "YUP", "YAY", "ZED", "ZAG"};
        return threeLettersWord[Random.Range(0, threeLettersWord.Length)];
    }

    string FourLettersDictionary()
    {
        string[] fourLettersWord = new string[] { "ACID", "ANTS", "AREA"};
        return fourLettersWord[Random.Range(0, fourLettersWord.Length)];
    }
    string FiveLettersDictionary()
    {
        string[] fiveLettersWord = new string[] { };
        return fiveLettersWord[Random.Range(0, fiveLettersWord.Length)];
    }
    string SixLettersDictionary()
    {
        string[] sixLettersWord = new string[] { };
        return sixLettersWord[Random.Range(0, sixLettersWord.Length)];
    }


    string WordLongPicker() // Decides which (Dictionary Letters Long) to select
    {
        //wordCompletionText.gameObject.SetActive(false); // disabling it so when we reactivate it again the animation plays, it set active when word completion

        if (Score.score <= 2)
        {
            return ThreeLettersDictionary();
        }
        else if (Score.score <= 6)
        {
            MoveDown.fallingSpeed = 4.5f; // or 5 then 5.5, 6, 6.5 ...
            return FourLettersDictionary();
        }
        //else if (Score.score <= 150)
        //{
        //MoveDown.fallingSpeed = 5;
        //    return FiveLettersDictionary();
        //}
        //else if (Score.score <= 200)
        //{
        //    return SixLettersDictionary();
        //}
        return ThreeLettersDictionary();
    }
}
