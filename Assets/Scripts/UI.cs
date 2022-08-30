using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[CreateAssetMenu(fileName ="UI",menuName ="ShipData")]
public class UI : ScriptableObject
{
    // Start is called before the first frame update
    public Ships[] shipsData;
    public int scorePoints;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
}
[System.Serializable]
public class Ships
{
    public string shipName;
    public int shippoints;
    public int shipSpeed;
    public int upgradepoints;
    public int upgrades;
}
