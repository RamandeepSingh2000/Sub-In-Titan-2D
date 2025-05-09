using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Script : MonoBehaviour
{
    [SerializeField] private AudioSource clickSound;
    [SerializeField] private GameObject mainMenu_canvas;
    [SerializeField] private GameObject startGame_canvas;
    [SerializeField] private GameObject settings_canvas;
    [SerializeField] private GameObject back_btn;

    // Start is called before the first frame update
    void Start()
    {
        mainMenu_canvas.SetActive(true);
        startGame_canvas.SetActive(false);
        settings_canvas.SetActive(false);
        back_btn.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void StartGame()
    {
        SceneManager.LoadScene("SmallerMap");
    }


    public void PLayClickSound()
    {
        clickSound.Play();
    }

    public void ExitGame()
    {
        Debug.Log("Exiting program...");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public void LoadGame()
    {
        mainMenu_canvas.SetActive(false);
        startGame_canvas.SetActive(true);
        back_btn.SetActive(true);
    }
    public void Settings()
    {
        mainMenu_canvas.SetActive(false);
        settings_canvas.SetActive(true);
        back_btn.SetActive(true);
    }
    public void BackBtn()
    {
        if (startGame_canvas.activeSelf)
        {
            startGame_canvas.SetActive(false);
            mainMenu_canvas.SetActive(true);
        }
        if (settings_canvas.activeSelf)
        {
            settings_canvas.SetActive(false);
            mainMenu_canvas.SetActive(true);
        }
        back_btn.SetActive(false);
    }

    public void LoadSceneByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }
}
