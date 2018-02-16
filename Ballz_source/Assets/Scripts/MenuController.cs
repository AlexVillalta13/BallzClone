using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    private const string RATE_GAME_URL = ""; // Write url to rate the game

    public void OnPlayClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

    public void OnRateClick()
    {
        Application.OpenURL(RATE_GAME_URL);
    }

    public void SoundClick()
    {

    }

    public void TutorialClick()
    {

    }
}
