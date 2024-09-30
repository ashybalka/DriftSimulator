using System.IO;
using System;
using UnityEngine;
using System.Runtime.InteropServices;

public class SaveLoadController : MonoBehaviour
{
    public Currencies _currencies = new();
    private string savePath;

    public static SaveLoadController Instance;

    [DllImport("__Internal")]
    private static extern void JS_FileSystem_Sync();


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        savePath = "idbfs/DriftSave/SaveData.json";
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
        if (!Directory.Exists("idbfs/DriftSave"))
        {
            Directory.CreateDirectory("idbfs/DriftSave");
        }
        File.WriteAllText(savePath, JsonUtility.ToJson(_currencies));
        Application.ExternalEval("_JS_FileSystem_Sync();");
    }

    [Serializable]
    public class Currencies
    {
        public int Money = 1000;
        public int Gold = 0;
    }
}
