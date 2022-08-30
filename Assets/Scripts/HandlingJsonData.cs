using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace JsonData
{
    public class HandlingJsonData : MonoBehaviour
    {
        public ShipDataList shipdata;
        string path;
        ShipDataList shiplist=new ShipDataList();
        private static HandlingJsonData instance;
        public static HandlingJsonData Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new HandlingJsonData();
                }
                return (instance);
            }
        }
        private void Awake()
        {
            //path = Path.Combine("{0}/{1}",Application.streamingAssetsPath, "gamedata.json");
            path = Path.Combine(Application.persistentDataPath, "GameData.json");

            Debug.Log(path);
        }
        private void Start()
        {
            //path = Path.Combine(Application.dataPath, "GameData.json");
            WriteFile(JsonUtility.ToJson(shipdata));
        }
        public ShipDataList ReadFile()
        {
            path = Path.Combine(Application.persistentDataPath, "GameData.json");
            //path = Path.Combine(Application.streamingAssetsPath, "gamedata.json");
            Debug.Log(path);

            return (JsonUtility.FromJson<ShipDataList>(File.ReadAllText(path)));
        }
        public void UpdateFile(ShipDataList shipdata)
        {
            //path = Path.Combine(Application.streamingAssetsPath, "gamedata.json");
            path = Path.Combine(Application.persistentDataPath, "GameData.json");
            File.WriteAllText(path, JsonUtility.ToJson(shipdata));
        }
        public void WriteFile(string data)
        {
            if (!File.Exists(path))
            {
                File.WriteAllText(path, data);
            }

        }
        public string GetTrueShip()
        {
            shiplist = ReadFile();
            for (int i = 0; i < shiplist.ships.Length; i++)
            {
                if(shiplist.ships[i].status==true)
                    return shiplist.ships[i].name;
            }
            return null;
        }
    }
    [System.Serializable]
    public class ShipData
    {
        public string name;
        public int shipPoints;
        public int shipSpeed;
        public int shipupgrades;
        public int upgrades;
        public bool status;
    }
    [System.Serializable]
    public class ShipDataList
    {
        public ShipData[] ships;
        public int score;
    }
}
