using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu_Script : MonoBehaviour
{
    [SerializeField] private AudioSource clickSound;

    public void StartGame()
    {
        SceneManager.LoadScene("SmallerMap");
    }
    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void PLayClickSound()
    {
        clickSound.Play();
    }
}
