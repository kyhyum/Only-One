using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FollowCamera : MonoBehaviour
{
    public GameObject player;
    public Transform trPlayer;
    private Vector2 prevPoint;
    private int camV;
    public Button btn;
    public Camera cam;
    public TMP_Text CameraText;

    private void Start()
    {
        camV = 1;
    }
    void Update()
    {
        btn.onClick.AddListener(() =>
        {
            if (camV == 1)
            {
                Vector3 vec = new Vector3(cam.transform.position.x, cam.transform.position.y + 4, cam.transform.position.z - 4);
                cam.transform.position = vec;
                if (cam.transform.position == vec)
                    camV = 2;
            }
            else
            {
                Vector3 vec = new Vector3(cam.transform.position.x, cam.transform.position.y - 4, cam.transform.position.z + 4);
                cam.transform.position = vec;
                if (cam.transform.position == vec)
                    camV = 1;
            }
            CameraText.text = camV.ToString();
        });

        TouchInput();
        followcam();
    }

    private void TouchInput()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch t = Input.GetTouch(i);
            if (!EventSystem.current.IsPointerOverGameObject(i))
            {
                if (Input.GetMouseButton(i) && (t.position.x > Screen.width / 2))
                {
                    this.prevPoint = t.position - t.deltaPosition;
                    this.transform.RotateAround(this.player.transform.position, Vector3.up, (t.position.x - this.prevPoint.x) * 0.1f);
                    this.prevPoint = t.position;
                }
            }
        }

    }

    private void followcam()
    {
        Vector3 pos = this.transform.position;
        this.transform.position = Vector3.Lerp(pos, trPlayer.position, 0.5f);

    }
    
    
    
}
