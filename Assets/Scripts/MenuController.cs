using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject firstMenu;
    public GameObject levelMenu;

    public void ActivateMenu(string menuName)
    {
        if (menuName == "First Menu")
        {
            firstMenu.SetActive(true);
            levelMenu.SetActive(false);
        }
        else if (menuName == "Level Menu")
        {
            levelMenu.SetActive(true);
            firstMenu.SetActive(false);
        }
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    
    public void CloseGame()
    {
        Application.Quit();
    }
}
