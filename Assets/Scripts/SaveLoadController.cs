using System.IO;
using System;
using UnityEngine;

public class SaveLoadController : MonoBehaviour
{
    public Currencies _currencies = new();
    private string savePath;

    void Awake()
    {
        savePath = Application.persistentDataPath + "/SaveData.json";
        LoadFromJson();
        SaveToJson();
    }

    public void LoadFromJson()
    {
        if (File.Exists(savePath))
        {
            _currencies = JsonUtility.FromJson<Currencies>(File.ReadAllText(savePath));
        }
        else
        {
            Debug.Log("Using default currencies.");
        }
    }

    public void SaveToJson()
    {
        File.WriteAllText(savePath, JsonUtility.ToJson(_currencies));
    }

    [Serializable]
    public class Currencies
    {
        public int Money = 1000;
        public int Gold = 0;
    }
}
