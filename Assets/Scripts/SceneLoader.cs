using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public static bool isChanged;
    public void LoadScene(string sceneName)
    {
        isChanged = true;
        SceneManager.LoadScene(sceneName);
    }
}
