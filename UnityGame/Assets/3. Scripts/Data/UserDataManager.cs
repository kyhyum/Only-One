using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

[System.Serializable]
public class OverwriteSaveData
{
    public OverwriteSaveData(bool _sound, bool _backgroundsound, int[] _passiveskill, int[] _activeskill, int _heart, int _money)
    {
        sound = _sound;
        backgroundsound = _backgroundsound;
        passiveskill = _passiveskill;
        activeskill = _activeskill;
        heart = _heart;
        money = _money;

    }

    public bool sound;
    public bool backgroundsound;
    public int[] passiveskill = new int[4];
    public int[] activeskill = new int[4];
    public int heart;
    public int money;
}

public class UserDataManager : MonoBehaviour
{
    private static string SavePath => Application.persistentDataPath + "/saves/";
    public static UserDataManager instance;

    public bool sound;
    public bool backgroundsound;
    public int[] passiveskill = new int[4];
    public int[] activeskill = new int[4];
    public int heart;
    public int money;

    void Awake()
    {
        instance = this;
    }

    public void OverwriteData()
    {
        OverwriteSaveData saveData = new OverwriteSaveData(sound, backgroundsound, passiveskill, activeskill, heart, money);
        string saveJson = JsonUtility.ToJson(saveData);
        string saveFilePath = SavePath + "user_data" + ".json";
        File.WriteAllText(saveFilePath, saveJson);
    }


}
