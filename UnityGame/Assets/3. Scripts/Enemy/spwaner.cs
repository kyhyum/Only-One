using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class spwaner : MonoBehaviour
{
    public GameObject stage_popup;
    public static spwaner Instance;
    public TMP_Text stage_text;
    public static int stage = 1;
    int monster_num;
    int[] monster = new int[3];
    int[] monster_queue_size = new int[3];

    // Monster prefebs
    public GameObject slime;
    public GameObject turtle;
    public GameObject lich;
    public GameObject bullet;
    private int slime_num;
    private int turtle_num;
    private int lich_num;

    // queue
    public Queue<GameObject> slime_queue = new Queue<GameObject>();
    public Queue<GameObject> turtle_queue = new Queue<GameObject>();
    public Queue<GameObject> lich_queue = new Queue<GameObject>();
    public Queue<GameObject> lich_bullet_queue = new Queue<GameObject>();
    void Start()
    {
        Instance = this;
        slime_num = 0;
        turtle_num = 0;
        lich_num = 0;
        for (int i = 0; i < 10; i++)
        {
            GameObject slime_object = Instantiate(slime, this.gameObject.transform);
            GameObject turtle_object = Instantiate(turtle, this.gameObject.transform);
            GameObject lich_object = Instantiate(lich, this.gameObject.transform);
            InsertQueue(slime_object, 0, false);
            InsertQueue(turtle_object, 1, false);
            InsertQueue(lich_object, 2, false);
        }
        monster_on();
        StartCoroutine(MonsterSpwan());
    }
    public void InsertQueue(GameObject add_object, int object_code, bool reinsert)
    {
        switch (object_code)
        {
            case 0:
                slime_queue.Enqueue(add_object);
                if (!reinsert)
                    monster_queue_size[0]++;
                break;
            case 1:
                turtle_queue.Enqueue(add_object);
                if (!reinsert)
                    monster_queue_size[1]++;
                break;
            case 2:
                lich_queue.Enqueue(add_object);
                if (!reinsert)
                    monster_queue_size[2]++;
                break;
            case 3:
                lich_bullet_queue.Enqueue(add_object);
                break;
        }
        add_object.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        stage_text.text = stage.ToString();
    }
    IEnumerator MonsterSpwan()
    {
        yield return new WaitForSeconds(3f);
        while (Monster_check())
        {
            Vector3 vec = new Vector3(0, 0, 0);
            GameObject monster_object = GetMonster();
            monster_object.transform.RotateAround(vec, Vector3.up, Random.Range(-180f, 180f));
            yield return new WaitForSeconds(10f);
        }
    }

    private void monster_on()
    {
        slime_num = 0;
        turtle_num = 0;
        lich_num = 0;
        //몬스터 개체수
        monster_num = stage * 10 - (stage * 3);
        //몬스터 소환 확률
        if (stage < 5)
        {
            for (int i = 0; i < monster_num; i++)
            {
                int rand = Random.Range(1, 100);
                if (rand < 100)
                    lich_num++;
                else
                    turtle_num++;
            }
        }

        else
        {
            for (int i = 0; i < monster_num; i++)
            {
                int rand = Random.Range(1, 100);
                if (rand < 10)
                    slime_num++;
                else if (rand < 80)
                    turtle_num++;
                else
                    lich_num++;
            }
        }
        monster[0] = slime_num;
        monster[1] = turtle_num;
        monster[2] = lich_num;
    }

    public GameObject GetMonster()
    {
        while (true)
        {
            GameObject monster_object;
            int rand = Random.Range(0, 3);
            if (monster[rand] == 0)
                continue;
            monster[rand]--;
            switch (rand)
            {
                case 0:
                    if (Instance.slime_queue.Count == 0)
                    {
                        GameObject slime_object = Instantiate(slime, this.gameObject.transform);
                        slime_queue.Enqueue(slime_object);
                        monster_queue_size[0]++;
                    }
                    monster_object = slime_queue.Dequeue();
                    monster_object.GetComponent<Enemy>().curHealth = monster_object.GetComponent<Enemy>().maxHealth;
                    monster_object.SetActive(true);
                    Debug.Log("슬라임");
                    return monster_object;
                case 1:
                    if (Instance.turtle_queue.Count == 0)
                    {
                        GameObject turtle_object = Instantiate(turtle, this.gameObject.transform);
                        turtle_queue.Enqueue(turtle_object);
                        monster_queue_size[1]++;
                    }
                    monster_object = turtle_queue.Dequeue();
                    monster_object.GetComponent<Enemy>().curHealth = monster_object.GetComponent<Enemy>().maxHealth;
                    monster_object.SetActive(true);
                    Debug.Log("터틀");
                    return monster_object;
                case 2:
                    if (Instance.lich_queue.Count == 0)
                    {
                        GameObject lich_object = Instantiate(lich, this.gameObject.transform);
                        lich_queue.Enqueue(lich_object);
                        monster_queue_size[2]++;
                    }
                    monster_object = lich_queue.Dequeue();
                    monster_object.GetComponent<Enemy>().curHealth = monster_object.GetComponent<Enemy>().maxHealth;
                    monster_object.SetActive(true);
                    Debug.Log("리치");
                    return monster_object;
            }

        }
    }

    public bool Monster_check()
    {
        for (int i = 0; i < monster.Length; i++)
        {
            if (monster[i] != 0)
                return true;
        }
        return false;
    }

    public void Die(Enemy gameobject, string type)
    {
        Debug.Log(gameobject.gameObject);
        if (type == "Slime")
        {
            gameobject.gameObject.SetActive(false);
            gameobject.GetComponent<Enemy>().curHealth = 9;

            Instance.InsertQueue(gameobject.gameObject, 0, true);
        }
        else if (type == "Turtle")
        {
            gameobject.gameObject.SetActive(false);
            gameobject.GetComponent<Enemy>().curHealth = 3;

            Instance.InsertQueue(gameobject.gameObject, 1, true);
        }
        else if (type == "Lich")
        {
            gameobject.gameObject.SetActive(false);
            gameobject.GetComponent<Enemy>().curHealth = 2;

            Instance.InsertQueue(gameobject.gameObject, 2, true);
        }
        stage_clear();
    }
    private void stage_clear()
    {
        Debug.Log("슬라임" + slime_queue.Count);
        Debug.Log("터틀" + turtle_queue.Count);
        Debug.Log("리치 " + lich_queue.Count);
        Debug.Log("슬라임 t" + monster_queue_size[0]);
        Debug.Log("터틀 t" + monster_queue_size[1]);
        Debug.Log("리치 t" + monster_queue_size[2]);
        if (slime_queue.Count == monster_queue_size[0] &&
            turtle_queue.Count == monster_queue_size[1] &&
            lich_queue.Count == monster_queue_size[2])
        {
            Time.timeScale = 0;
            stage_popup.SetActive(true);
        }

    }

    public IEnumerator lich_attack(GameObject lich)
    {
        if (lich_bullet_queue.Count == 0)
        {
            GameObject instantBullet = Instantiate(bullet, lich.transform.position - (Vector3.forward), lich.transform.rotation);
            instantBullet.SetActive(false);
            InsertQueue(instantBullet, 3, false);
        }
        GameObject bullet_object = lich_bullet_queue.Dequeue();
        bullet_object.transform.position = lich.transform.position - (Vector3.forward);
        bullet_object.SetActive(true);
        Rigidbody rigidBullet = bullet_object.GetComponent<Rigidbody>();
        rigidBullet.velocity = lich.transform.forward * 6;

        //10초뒤 투사체 비활성화
        yield return new WaitForSeconds(10f);
        if (bullet.activeInHierarchy)
        {
            bullet_disable(bullet_object);
        }
    }

    public void bullet_disable(GameObject bullet)
    {
        bullet.SetActive(false);
        InsertQueue(bullet, 3, false);
    }

    public void stage_reset()
    {
        stage = 1;
    }
}


