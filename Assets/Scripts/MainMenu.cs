using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void Options()
    {

    }

    public void Quit()
    {
        Application.Quit(0);
    }
}
