using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToastMessage : MonoBehaviour
{
    public TextMeshProUGUI toast;

    void Start()
    {
        toast.enabled = false;
    }
    public void showMessage(float durationTime)
    {
        StartCoroutine(showMessageCoroutine(durationTime));
    }

    private IEnumerator showMessageCoroutine(float durationTime)
    {
        toast.enabled = true;

        yield return new WaitForSecondsRealtime(durationTime);
        toast.enabled = false;
    }


}