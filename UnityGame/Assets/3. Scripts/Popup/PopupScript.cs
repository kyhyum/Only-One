using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopupScript : MonoBehaviour
{
    public GameObject gameobject;
    public void PopupActive()
    {
        Time.timeScale = 0;
        gameobject.SetActive(true);
    }


    public void PopupUnActive()
    {
        Time.timeScale = 1;
        gameobject.SetActive(false);
    }
    public void PopupUnActive_time()
    {
        gameobject.SetActive(false);
    }
    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }
    public void Mainmenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void NextStage()
    {
        Time.timeScale = 1;
        spwaner.stage++;
        SceneManager.LoadScene(2);
    }
}
