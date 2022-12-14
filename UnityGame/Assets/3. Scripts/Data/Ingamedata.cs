using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

[System.Serializable]
public class SaveData
{
    public SaveData(bool _sound, bool _backgroundsound, int[] _passiveskill, int[] _activeskill, int _heart, int _money)
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


public static class SaveSystem
{
    private static string SavePath => Application.persistentDataPath + "/saves/";

    public static void Save(SaveData saveData, string saveFileName)
    {
        if (!Directory.Exists(SavePath))
        {
            Directory.CreateDirectory(SavePath);
        }

        string saveJson = JsonUtility.ToJson(saveData);

        string saveFilePath = SavePath + saveFileName + ".json";
        File.WriteAllText(saveFilePath, saveJson);
        Debug.Log("Save Success: " + saveFilePath);
    }

    public static SaveData Load(string saveFileName)
    {
        string saveFilePath = SavePath + saveFileName + ".json";

        while (true)
        {
            if (!File.Exists(saveFilePath))
            {
                int[] x = {0,0,0,0};
                SaveData gamedata = new SaveData(true, false, x, x, 3, 0);
                SaveSystem.Save(gamedata, "user_data");
                continue;
            }

            string saveFile = File.ReadAllText(saveFilePath);
            SaveData saveData = JsonUtility.FromJson<SaveData>(saveFile);
            Debug.Log("load success");
            return saveData;
        }

    }


}

public class Ingamedata : MonoBehaviour
{
    public void loadgamedata()
    {
        SaveData loadData = SaveSystem.Load("user_data");
        UserDataManager UserDataManager = gameObject.AddComponent<UserDataManager>();
        UserDataManager.instance.sound = loadData.sound;
        UserDataManager.instance.backgroundsound = loadData.backgroundsound;
        UserDataManager.instance.passiveskill = loadData.passiveskill;
        UserDataManager.instance.activeskill = loadData.activeskill;
        UserDataManager.instance.heart = loadData.heart;
        UserDataManager.instance.money = loadData.money;
        DontDestroyOnLoad(UserDataManager);
    }
}
