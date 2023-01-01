using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FollowCamera : MonoBehaviour
{
    public GameObject player;
    private GameObject eyes;
    public Transform trPlayer;
    private Vector2 prevPoint;
    private static int camV = 1;
    public Button btn;
    public Camera cam1, cam2;
    public TMP_Text CameraText;
    private bool isCam_fix = false;
    private bool re_touch = false;

    private void Start()
    {
        cam1.enabled = true;
        cam2.enabled = false;
    }
    void Update()
    {
        btn.onClick.AddListener(() =>
        {
            if (camV == 1)
            {
                cam1.enabled = false;
                cam2.enabled = true;
                camV = 2;
            }
            else
            {
                cam1.enabled = true;
                cam2.enabled = false;
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
            RaycastHit hit;
            Ray touchray;
            if (camV == 1)
                touchray = cam1.ScreenPointToRay(Input.mousePosition);
            else
                touchray = cam2.ScreenPointToRay(Input.mousePosition);

            if (!EventSystem.current.IsPointerOverGameObject(i))
            {
                Physics.Raycast(touchray, out hit);
                if ((t.position.x > Screen.width / 3))
                {
                    isCam_fix = false;
                    if (hit.collider.tag == "click collider")
                    {
                        eyes = hit.transform.Find("eyes").gameObject;
                        isCam_fix = true;
                        StartCoroutine(enemy_FollowCam(hit));
                    }

                    else if (Input.GetMouseButton(i) && Mathf.Abs(t.deltaPosition.x) > 1)
                    {
                        this.prevPoint = t.position - t.deltaPosition;
                        this.transform.RotateAround(this.player.transform.position, Vector3.up, (t.position.x - this.prevPoint.x) * 0.1f);
                        this.prevPoint = t.position;
                        if (isCam_fix)
                            eyes.SetActive(false);
                        isCam_fix = false;
                    }
                }
            }
        }

    }

    private void followcam()
    {
        Vector3 pos = this.transform.position;
        this.transform.position = Vector3.Lerp(pos, trPlayer.position, 0.5f);

    }

    IEnumerator enemy_FollowCam(RaycastHit hit)
    {
        while (isCam_fix && hit.collider.gameObject.activeInHierarchy)
        { 
            Vector3 vec_sub = hit.collider.transform.position - player.transform.position;
            float distance = Vector3.Distance(hit.collider.transform.position, player.transform.position);
            if (distance > 3)
            {
                Vector3 vec = new Vector3(0, 90 - (90 / (Mathf.Abs(vec_sub.x) + Mathf.Abs(vec_sub.z)) * Mathf.Abs(vec_sub.z)), 0);
                if (vec_sub.x > 0 && vec_sub.z < 0)
                    vec = new Vector3(0, 90 + (90 / (Mathf.Abs(vec_sub.x) + Mathf.Abs(vec_sub.z)) * Mathf.Abs(vec_sub.z)), 0);
                else if (vec_sub.x < 0 && vec_sub.z > 0)
                    vec.y *= -1;
                else if (vec_sub.x < 0 && vec_sub.z < 0)
                    vec = new Vector3(0, -(90 + (90 / (Mathf.Abs(vec_sub.x) + Mathf.Abs(vec_sub.z)) * Mathf.Abs(vec_sub.z))), 0);
                this.transform.rotation = Quaternion.Euler(vec);
                eyes.SetActive(true);
            }
            yield return new WaitForSeconds(0.0001f);
        }
        eyes.SetActive(false);
        re_touch = false;
    }
}
