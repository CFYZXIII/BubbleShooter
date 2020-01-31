using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{

    public GameObject panel;
  

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }


    public void LoaSettings()
    {
        SceneManager.LoadScene(2);
    }


    public void LoadMain()
    {
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void activatePanel(GameObject panel) {
        panel.SetActive(true);
    }

    public void deactivatePanel(GameObject panel) {
        panel.SetActive(false);
    }

    public void URL() {
        Application.OpenURL("https://vk.com/huntergalaxy");
    }

}
