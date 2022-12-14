using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    //--- Prefabs ---
    //Enemy
    public GameObject SlimePrefab;
    public GameObject TurtleShellPrefab;
    public GameObject FreeLichPrefab;
    //Item
    public GameObject itemHeartPrefab;
    public GameObject itemDiamondPrefab;
    public GameObject itemHexagonPrefab;
    public GameObject itemPentaPrefab;
    //Bullet
    public GameObject MagicFirePrefab;

    //--- GameObjects ---
    //Enemy
    GameObject[] Slime;
    GameObject[] TurtleShell;
    GameObject[] FreeLich;
    //Item
    GameObject[] itemHeart;
    GameObject[] itemDiamond;
    GameObject[] itemHexagon;
    GameObject[] itemPenta;
    //Bullet
    GameObject[] MagicFire;
    //GameObject for Copy 
    GameObject[] targetPool;


    // Start is called before the first frame update
    void Awake()
    {
        //Enemy
        Slime = new GameObject[10];
        TurtleShell = new GameObject[10];
        FreeLich = new GameObject[10];

        //Item
        itemHeart = new GameObject[6];
        itemDiamond = new GameObject[5];
        itemHexagon = new GameObject[10];
        itemPenta = new GameObject[20];

        //Bullet
        MagicFire = new GameObject[20];

        generate();

    }

    void generate()
    {
        //Enemy
        for (int i = 0; i < Slime.Length; i++)
        {
            Slime[i] = Instantiate(SlimePrefab);
            Slime[i].SetActive(false);
        }
        for (int i = 0; i < TurtleShell.Length; i++)
        {
            TurtleShell[i] = Instantiate(TurtleShellPrefab);
            TurtleShell[i].SetActive(false);
        }
        for(int i = 0; i < FreeLich.Length; i++)
        {
            FreeLich[i] = Instantiate(FreeLichPrefab);
            FreeLich[i].SetActive(false);
        }

        //Item
        for (int i = 0; i < itemHeart.Length; i++)
        {
            itemHeart[i] = Instantiate(itemHeartPrefab);
            itemHeart[i].SetActive(false);
        }
        for (int i = 0; i < itemDiamond.Length; i++)
        {
            itemDiamond[i] = Instantiate(itemDiamondPrefab);
            itemDiamond[i].SetActive(false);
        }
        for (int i = 0; i < itemHexagon.Length; i++)
        {
            itemHexagon[i] = Instantiate(itemHexagonPrefab);
            itemHexagon[i].SetActive(false);
        }
        for (int i = 0; i < itemPenta.Length; i++)
        {
            itemPenta[i] = Instantiate(itemPentaPrefab);
            itemPenta[i].SetActive(false);
        }

        //Bullet
        for (int i = 0; i < MagicFire.Length; i++)
        {
            MagicFire[i] = Instantiate(MagicFirePrefab);
            MagicFire[i].SetActive(false);
        }
    }

    public GameObject makeObj(string type)
    {
        switch (type)
        {
            case "Slime":
                targetPool = Slime;
                break;
            case "TurtleShell":
                targetPool = TurtleShell;
                break;
            case "FreeLich":
                targetPool = FreeLich;
                break;
            case "itemHeart":
                targetPool = itemHeart;
                break;
            case "itemDiamond":
                targetPool = itemDiamond;
                break;
            case "itemHexagon":
                targetPool = itemHexagon;
                break;
            case "itemPenta":
                targetPool = itemPenta;
                break;
            case "MagicFire":
                targetPool = MagicFire;
                break;
        }
        for (int i = 0; i < targetPool.Length; i++)
        {
            if (!targetPool[i].activeSelf)
            {
                targetPool[i].SetActive(true);
                return targetPool[i];
            }
        }
        return null;
    }

    public GameObject[] getPool(string type)
    {
        switch (type)
        {
            case "Slime":
                targetPool = Slime;
                break;
            case "TurtleShell":
                targetPool = TurtleShell;
                break;
            case "FreeLich":
                targetPool = FreeLich;
                break;
            case "itemHeart":
                targetPool = itemHeart;
                break;
            case "itemDiamond":
                targetPool = itemDiamond;
                break;
            case "itemHexagon":
                targetPool = itemHexagon;
                break;
            case "itemPenta":
                targetPool = itemPenta;
                break;
            case "MagicFire":
                targetPool = MagicFire;
                break;
        }
        return targetPool;
    }

}
