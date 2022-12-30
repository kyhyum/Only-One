using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store_Purchase : MonoBehaviour
{
    // Start is called before the first frame update
    public UserDataManager userdatamanager;
    void Start()
    {
        userdatamanager = GameObject.FindWithTag("SaveLoad").GetComponent<UserDataManager>();
    }

    public void Purchase(int idx)
    {
        if (idx < 4)
        {
            userdatamanager.activeskill[idx] = 1;
        }
        else
        {
            userdatamanager.passiveskill[idx - 4] = 1;
        }
        userdatamanager.OverwriteData();
    }

}
