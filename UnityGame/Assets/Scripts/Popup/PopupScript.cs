using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopupScript : MonoBehaviour
{

    public void PopupActive()
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);
    }

    public void PopupUnActive()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
    public void Retry()
    {
        SceneManager.LoadScene(1);
    }
    public void Mainmenu()
    {
        SceneManager.LoadScene(0);
    }
}
