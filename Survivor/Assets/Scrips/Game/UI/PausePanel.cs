using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{
    public void ContinueGame()
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }
    public void BackMenu()
    {
        Time.timeScale=1.0f;
        SceneManager.LoadScene("MainMenu");
    }
}
