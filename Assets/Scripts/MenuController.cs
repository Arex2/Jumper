using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetSceneByName("ChangeColorsScene").isLoaded)
        {
            CloseOptions();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && !SceneManager.GetSceneByName("ChangeColorsScene").isLoaded)
        {
            OpenOptions();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            //restart scene
            //Reload();
        }
    }

    private void Reload()
    {
        SceneManager.LoadSceneAsync(5);
    }
    private void OpenOptions()
    {
        SceneManager.LoadScene("ChangeColorsScene", LoadSceneMode.Additive);
        Time.timeScale = 0;
    }

    private void CloseOptions()
    {
        SceneManager.UnloadSceneAsync("ChangeColorsScene");
        Time.timeScale = 1;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
