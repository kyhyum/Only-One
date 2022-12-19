using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExplainWindow : MonoBehaviour
{
    public TextMeshProUGUI Titletext;
    public TextMeshProUGUI Descriptiontext;
    public GameObject Description_Window;
    public int num;
    void Start()
    {
        Description_Window.SetActive(false);
    }

    public void Window_open()
    {
        setText(num);
        Description_Window.SetActive(true);
    }
    public void Window_close()
    {
        Description_Window.SetActive(false);
    }
    public void setText(int a){
        switch (a){
            case 1:
                Titletext.text = "FireWave";
                Descriptiontext.text = "Breathes fire forward.";
                break;
            case 2:
                Titletext.text = "AirSlash";
                Descriptiontext.text = "Unleashes a sword strike forward. Enemies hit are knocked back and stunned for a few seconds.";
                break;
            case 3:
                Titletext.text = "Barrier";
                Descriptiontext.text = "Blocks 1 attack for a few seconds.";
                break;
            case 4:
                Titletext.text = "Rush";
                Descriptiontext.text = "Increases movement and attack speed.";
                break;
            case 5:
                Titletext.text = "Strength";
                Descriptiontext.text = "When full of blood, attacking an enemy will stun them for a few seconds.";
                break;
            case 6:
                Titletext.text = "Magnet";
                Descriptiontext.text = "Attracts nearby Gems.";
                break;
            case 7:
                Titletext.text = "DoubleAttack";
                Descriptiontext.text = "Basic attack turns into a double attack.";
                break;
            case 8:
                Titletext.text = "Heart Plus";
                Descriptiontext.text = "The default number of hearts is increased.";
                break;
        }
    }

}
