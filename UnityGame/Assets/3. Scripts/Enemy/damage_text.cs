using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class damage_text : MonoBehaviour
{
    private float speed;
    private float alpha_speed;
    private float destroytime;
    public TextMeshPro text;
    Color alpha;
    public int damage;

    public GameObject mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        speed = 2.0f;
        alpha_speed = 2.0f;
        destroytime = 2.0f;
        mainCamera = GameObject.Find("CamPivot");

        text = GetComponent<TextMeshPro>();
        alpha = text.color;
        text.text = damage.ToString();
        Invoke("DestroyObject", destroytime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alpha_speed);
        text.color = alpha;
        this.transform.LookAt(this.transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
    }

    private void DestroyObject()
    {
        Destroy(this.gameObject);
    }
}
