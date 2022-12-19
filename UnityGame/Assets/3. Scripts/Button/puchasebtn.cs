using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puchasebtn : MonoBehaviour
{
    public GameObject explorpurch;
    public bool isclicked = false;
    // Start is called before the first frame update
    
    void Start()
    {
        explorpurch.SetActive(false);
    }

    public void click(){
            if (isclicked == true)
            {
                isclicked = false;
                explorpurch.SetActive(false);
            }
            else
            {
                isclicked = true;
                explorpurch.SetActive(true);
            }
        
    }
}
