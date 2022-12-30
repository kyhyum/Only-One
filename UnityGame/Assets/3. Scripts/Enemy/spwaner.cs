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
    private int num = 0;

    // Monster prefebs
    public GameObject slime;
    public GameObject turtle;
    public GameObject lich;
    public GameObject bullet;
    private int slime_num;
    private int turtle_num;
    private int lich_num;

    // Item prefebs 
    public GameObject itemPenta;
    public GameObject itemHexagon;
    public GameObject itemHeart;
    public GameObject itemDiamond;

    // Monster queue
    public Queue<GameObject> slime_queue = new Queue<GameObject>();
    public Queue<GameObject> turtle_queue = new Queue<GameObject>();
    public Queue<GameObject> lich_queue = new Queue<GameObject>();
    public Queue<GameObject> lich_bullet_queue = new Queue<GameObject>();
    // Item queue
    public Queue<GameObject> itemPenta_queue = new Queue<GameObject>();
    public Queue<GameObject> itemHexagon_queue = new Queue<GameObject>();
    public Queue<GameObject> itemHeart_queue = new Queue<GameObject>();
    public Queue<GameObject> itemDiamond_queue = new Queue<GameObject>();
    void Start()
    {
        Instance = this;
        slime_num = 0;
        turtle_num = 0;
        lich_num = 0;

        //생성
        for (int i = 0; i < 10; i++)
        {
            //몬스터
            GameObject slime_object = Instantiate(slime, this.gameObject.transform);
            GameObject turtle_object = Instantiate(turtle, this.gameObject.transform);
            GameObject lich_object = Instantiate(lich, this.gameObject.transform);
            InsertQueue(slime_object, 4, false);
            InsertQueue(turtle_object, 5, false);
            InsertQueue(lich_object, 6, false);
            //아이템
            GameObject itemPenta_object = Instantiate(itemPenta, this.gameObject.transform);
            GameObject itemHexagon_object = Instantiate(itemHexagon, this.gameObject.transform);
            GameObject itemDiamond_object = Instantiate(itemDiamond, this.gameObject.transform);
            GameObject itemHeart_object = Instantiate(itemHeart, this.gameObject.transform);
            InsertQueue(itemPenta_object, 0, false);
            InsertQueue(itemHexagon_object, 1, false);
            InsertQueue(itemDiamond_object, 2, false);
            InsertQueue(itemHeart_object, 3, false);
        }
        monster_on();
        StartCoroutine(MonsterSpwan());
    }

    public void InsertQueue(GameObject add_object, int object_code, bool reinsert)
    {
        switch (object_code)
        {
            case 0:
                itemPenta_queue.Enqueue(add_object);
                break;
            case 1:
                itemHexagon_queue.Enqueue(add_object);
                break;
            case 2:
                itemDiamond_queue.Enqueue(add_object);
                break;
            case 3:
                itemHeart_queue.Enqueue(add_object);
                break;
            //0~3 아이템
            case 4:
                slime_queue.Enqueue(add_object);
                if (!reinsert)
                    monster_queue_size[0]++;
                break;
            case 5:
                turtle_queue.Enqueue(add_object);
                if (!reinsert)
                    monster_queue_size[1]++;
                break;
            case 6:
                lich_queue.Enqueue(add_object);
                if (!reinsert)
                    monster_queue_size[2]++;
                break;
            case 7:
                lich_bullet_queue.Enqueue(add_object);
                break;
                //4~ 몬스터
        }
        add_object.SetActive(false);
    }

    void Update()
    {
        stage_text.text = stage.ToString();
    }
    //아이템 스폰
    public void dropitem(Vector3 vector, string type)
    {
        int num = 0;
        vector += Vector3.up;
        switch (type)
        {
            case "Slime":
                num = Random.Range(0, 15);
                break;
            case "Turtle":
                num = Random.Range(5, 30);
                break;
            case "Lich":
                num = Random.Range(10, 30);
                break;
        }
        if (num >= 0 && num < 10)
        {
            GameObject dropitem = GetItem(ref itemPenta_queue, itemPenta);
            dropitem.transform.position = vector;
        }
        else if (num >= 10 && num < 20)
        {
            GameObject dropitem = GetItem(ref itemHexagon_queue, itemHexagon);
            dropitem.transform.position = vector;
        }
        else if (num >= 20 && num <= 25)
        {
            GameObject dropitem = GetItem(ref itemDiamond_queue, itemDiamond);
            dropitem.transform.position = vector;

        }
        else if (num > 25 && num <= 30)
        {
            GameObject dropitem = GetItem(ref itemHeart_queue, itemDiamond);
            dropitem.transform.position = vector;
        }
    }

    //몬스터 스폰
    IEnumerator MonsterSpwan()
    {
        yield return new WaitForSeconds(3f);
        while (Monster_check())
        {
            Vector3 vec = new Vector3(0, 0, 0);
            num++;
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
        monster_num = 2;
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
    //아이템 활성화
    public GameObject GetItem(ref Queue<GameObject> pointer, GameObject gameObject)
    {
        Queue<GameObject> Item_queue = pointer;
        if (Item_queue.Count == 0)
        {
            GameObject ItemObject = Instantiate(gameObject, this.gameObject.transform);
            Item_queue.Enqueue(ItemObject);
        }
        GameObject Item_Object = Item_queue.Dequeue();
        Item_Object.SetActive(true);
        return Item_Object;
    }

    //몬스터 활성화
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
        Vector3 vector = gameobject.gameObject.GetComponent<Transform>().position;
        dropitem(vector, type);
        if (type == "Slime")
        {
            gameobject.gameObject.SetActive(false);
            gameobject.GetComponent<Enemy>().curHealth = 9;

            Instance.InsertQueue(gameobject.gameObject, 4, true);
        }
        else if (type == "Turtle")
        {
            gameobject.gameObject.SetActive(false);
            gameobject.GetComponent<Enemy>().curHealth = 3;

            Instance.InsertQueue(gameobject.gameObject, 5, true);
        }
        else if (type == "Lich")
        {
            gameobject.gameObject.SetActive(false);
            gameobject.GetComponent<Enemy>().curHealth = 2;

            Instance.InsertQueue(gameobject.gameObject, 6, true);
        }
        StartCoroutine(stage_clear());
    }

    public IEnumerator stage_clear()
    {
        if (slime_queue.Count == monster_queue_size[0] &&
            turtle_queue.Count == monster_queue_size[1] &&
            lich_queue.Count == monster_queue_size[2] &&
            num == monster_num)
        {
            yield return new WaitForSeconds(3f);
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
            InsertQueue(instantBullet, 7, false);
        }
        GameObject bullet_object = lich_bullet_queue.Dequeue();
        bullet_object.transform.position = lich.transform.position - (Vector3.forward);
        bullet_object.SetActive(true);
        Rigidbody rigidBullet = bullet_object.GetComponent<Rigidbody>();
        rigidBullet.velocity = lich.transform.forward * 6;

        //10초뒤 투사체 비활성화
        yield return new WaitForSeconds(3f);
        if (bullet_object.activeInHierarchy)
        {
            bullet_disable(bullet_object);
        }
    }

    public void bullet_disable(GameObject bullet)
    {
        bullet.SetActive(false);
        InsertQueue(bullet, 7, false);
    }

    public void stage_reset()
    {
        stage = 1;
    }
}

