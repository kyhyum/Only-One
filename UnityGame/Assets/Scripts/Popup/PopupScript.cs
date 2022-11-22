using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupScript : MonoBehaviour
{
    public GameObject gameObject;

    public void PopupActive()
    {
        gameObject.SetActive(true);
    }

    public void PopupUnActive()
    {
        gameObject.SetActive(false);
    }

}
