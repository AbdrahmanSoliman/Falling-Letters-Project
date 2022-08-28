using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DestroyOutOfBounds : MonoBehaviour
{
    public static DestroyOutOfBounds Instance;

    private void Start()
    {
        Instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -5.4)
        {
            GameObject child1 = gameObject.transform.GetChild(0).gameObject;
            GameObject childOfChild1 = child1.transform.GetChild(0).gameObject;
            string destroyedLetter = childOfChild1.GetComponent<TextMeshProUGUI>().text;

            WordSelector.Instance.CurrentLetter(destroyedLetter); // needed letter destroyed itself
            Destroy(gameObject);
        }

        if(GameManager.isGamePlayable == false && !GameManager.isGameOver) //If the game is OFF (displaying the word for 3 seconds) and game not over yet!
        {
            Destroy(gameObject);
        }
    }
}
