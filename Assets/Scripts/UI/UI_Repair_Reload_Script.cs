using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Repair_Reload_Script : MonoBehaviour
{
    [SerializeField] GameObject GUI;
    [SerializeField] GameObject Menu_UI;
    [SerializeField] GameObject Menu_frame;
    [SerializeField] GameObject Progress_frame;
    [SerializeField] GameObject Repair_UI;
    [SerializeField] GameObject Reload_UI;
    [SerializeField] GameObject Pause_UI;
    [SerializeField] AudioSource clickSound;
    private bool MenuUI_activated=false;
    private bool PauseUI_activated = false;

    private void Awake()
    {
        GUI.SetActive(true);
        Menu_UI.SetActive(false);
        Pause_UI.SetActive(false);
    }
    private void Update()
    {
        //Activate Repair/Reload UI with the key "R"
        if (Input.GetKeyDown(KeyCode.R) && MenuUI_activated == false)
        {
            Menu_UI.SetActive(true);
            Menu_frame.SetActive(true);
            Progress_frame.SetActive(false);
            MenuUI_activated = true;
        } else if(Input.GetKeyDown(KeyCode.R) && MenuUI_activated == true)//Close Repair/Reload UI with the key "R"
        {
            Menu_UI.SetActive(false);
            MenuUI_activated = false;
        }
        //Activate PauseUII with the key "Esc"
        if (Input.GetKeyDown(KeyCode.Escape) && PauseUI_activated == false)
        {
            Pause_UI.SetActive(true);
            PauseUI_activated = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && PauseUI_activated == true)//Close Pause UI with the key "Esc"
        {
            Pause_UI.SetActive(false);
            PauseUI_activated = false;
        }
    }
    //Display Repair UI
    public void ActivateRepairUI()
    {
        Menu_frame.SetActive(false);
        Progress_frame.SetActive(true);
        Repair_UI.SetActive(true);
        Reload_UI.SetActive(false);
    }
    //Display Reload UI
    public void ActivateReloadUI()
    {
        Menu_frame.SetActive(false);
        Progress_frame.SetActive(true);
        Repair_UI.SetActive(false);
        Reload_UI.SetActive(true);
    }
    //Close Repair UI or Reload UI
    public void CloseUI()
    {
        Menu_frame.SetActive(true);
        Progress_frame.SetActive(false);
        Repair_UI.SetActive(false);
        Reload_UI.SetActive(false);
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
