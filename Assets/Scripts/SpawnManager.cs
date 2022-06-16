using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    #region PUBLIC VARIABLES
    public ObjectPool[] objects;
    #endregion
    #region PRIVATE VARIABLES
    Dictionary<string, List<GameObject>> poolDictionary=new Dictionary<string, List<GameObject>>();
    List<GameObject> spawnList;
    #endregion
    #region SINGLETON CLASS
    private static SpawnManager instance;
    public static SpawnManager Instance
    {
        get
        {
            if(instance==null)
            {
                instance = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
            }
            return instance;
        }
    }
    #endregion
    #region MONOBEHAVIOUR METHODS
    private void Awake()
    {
        for(int i = 0; i < objects.Length; i++)
        {
            Spawn(objects[i].prefab, objects[i].count);
        }
        
    }
    #endregion
    #region PUBLIC METHODS
    public void Spawn(GameObject prefab,int count)
    {
        spawnList = new List<GameObject>();
        GameObject obj = new GameObject(prefab.name + "Prefabs");
        for(int i=0;i<count;i++)
        {
            GameObject temp=Instantiate(prefab);
            temp.name = prefab.name;
            temp.transform.parent = obj.transform;
            temp.SetActive(false);
            spawnList.Add(temp);
        }
        poolDictionary.Add(prefab.name, spawnList);
    }
    public GameObject GetFromPool(string name)
    {
        
        if(poolDictionary.ContainsKey(name))
        {
            for (int i = 0; i < poolDictionary[name].Count; i++)
            {
                if (poolDictionary[name][i].activeInHierarchy==false)
                {
                    return poolDictionary[name][i];
                }
            }
        }
        return null;
    }
    #endregion
    #region PRIVATE METHODS

    #endregion
}
[System.Serializable]
public class ObjectPool
{
    public GameObject prefab;
    public int count;
}
