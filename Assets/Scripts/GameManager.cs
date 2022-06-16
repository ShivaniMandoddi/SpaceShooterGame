using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region PUBLIC VARIABLES

    #endregion
    #region PRIVATE VARIABLES
    int number;
    Vector2 screenSize;
    #endregion
    #region SINGLETON CLASS
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameManager();
            }
            return instance;
        }
    }
    #endregion
    #region MONOBEHAVIOUR METHODS
    private void Awake()
    {
         StartCoroutine(EnemySpawning());
        screenSize=Camera.main.ScreenToWorldPoint(new Vector2(Screen.width,Screen.height));
    }
    private void Start()
    {

        
    }
    IEnumerator EnemySpawning()
    {
        while(true)
        {
           
            yield return new WaitForSeconds(2);
            number = Random.Range(1, 4);
            SpawnEnemy(number);
        }
       
    }
    #endregion
    #region PUBLIC METHODS
    public void SpawnEnemy(int number)
    {
        string enemyname = "Enemy"+number.ToString();
        GameObject temp=SpawnManager.Instance.GetFromPool(enemyname);
        temp.transform.position = new Vector3(Random.Range(-screenSize.x, screenSize.x), screenSize.y+1, transform.position.z);
        temp.SetActive(true);

    }
    public void SpawnBullet(string name,Transform spawnPoint)
    {
        
        GameObject temp = SpawnManager.Instance.GetFromPool(name);

        temp.transform.position = spawnPoint.position;
        temp.SetActive(true);
    }
    #endregion
    #region PRIVATE METHODS

    #endregion
}
