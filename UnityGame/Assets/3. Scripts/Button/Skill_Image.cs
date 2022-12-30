using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill_Image : MonoBehaviour
{
    public RawImage[] Button_Images;
    public Button[] buttons;
    public Texture[] image;
    public UserDataManager userdatamanager;
    void Start()
    {
        userdatamanager = GameObject.FindWithTag("SaveLoad").GetComponent<UserDataManager>();
        int[] activeskill = userdatamanager.activeskill;
        for(int i = 0 ; i < 4; i++)
        {
            if (activeskill[i] > 0)
            {
                Button_Images[i].texture = image[i];
                Button_Images[i].color = Color.white;
            }
            else
            {
                buttons[i].interactable = false;
            }
        }
    }
    public void refresh()
    {
        int[] activeskill = userdatamanager.activeskill;
        for (int i = 0; i < 4; i++)
        {
            if (activeskill[i] > 0)
            {
                Button_Images[i].texture = image[i];
                Button_Images[i].color = Color.white;
                buttons[i].interactable = true;
            }
            else
            {
                buttons[i].interactable = false;
            }
        }
    }
    
}
