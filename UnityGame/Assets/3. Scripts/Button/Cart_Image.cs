using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Cart_Image : MonoBehaviour
{
    public RawImage[] Button_Images;
    public Button[] buttons;
    public Texture image;
    public UserDataManager userdatamanager;
    void Start()
    {
        userdatamanager = GameObject.FindWithTag("SaveLoad").GetComponent<UserDataManager>();
        if (this.gameObject.name.Contains("Active"))
        {
            int[] activeskill = userdatamanager.activeskill;
            for (int i = 0; i < 4; i++)
            {
                if (activeskill[i] > 0)
                {
                    Button_Images[i].texture = image;
                    Button_Images[i].color = Color.white;
                    buttons[i].interactable = false;
                }
            }
        }
        else
        {
            int[] passiveskill = userdatamanager.passiveskill;
            for (int i = 0; i < 4; i++)
            {
                if (passiveskill[i] > 0)
                {
                    Button_Images[i].texture = image;
                    Button_Images[i].color = Color.white;
                    buttons[i].interactable = false;
                }
            }
        }

    }
    public void refresh()
    {
        if (this.gameObject.name.Contains("Active"))
        {
            int[] activeskill = userdatamanager.activeskill;
            for (int i = 0; i < 4; i++)
            {
                if (activeskill[i] > 0)
                {
                    Button_Images[i].texture = image;
                    Button_Images[i].color = Color.white;
                    buttons[i].interactable = false;
                }
            }
        }
        else
        {
            int[] passiveskill = userdatamanager.passiveskill;
            for (int i = 0; i < 4; i++)
            {
                if (passiveskill[i] > 0)
                {
                    Button_Images[i].texture = image;
                    Button_Images[i].color = Color.white;
                    buttons[i].interactable = false;
                }
            }
        }
    }
}
