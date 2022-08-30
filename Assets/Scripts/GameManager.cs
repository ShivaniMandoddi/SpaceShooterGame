using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using JsonData;

public class GameManager : MonoBehaviour
{
    #region PUBLIC VARIABLES
    public GameObject[] ships;
    public GameObject enemy;
    public GameObject routes;
    public GameObject gameOverPanel;
    public GameObject gameWinPanel;
    public bool IsGameOver=false;
    int waves;
    int currentwave;
    
    #endregion
    #region PRIVATE VARIABLES
    int number;
    Vector2 screenSize;
    GameObject currentenemy;
    GameObject currentroute;
    int i;
    int currentship;
    float shoottime = 5f;
    float powertime = 3f;
    int randomPowerUp;
    string playername;
    #endregion
    #region SINGLETON CLASS
    private static GameManager instance;
    //public UI shipData;
    ShipDataList shipdataList;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.Find("GameManager").GetComponent<GameManager>();
            }
            return instance;
        }
    }
    #endregion
    #region MONOBEHAVIOUR METHODS
    private void Awake()
    {
         StartCoroutine(EnemySpawning());
         playername = HandlingJsonData.Instance.GetTrueShip();
         ShipData item=GetShipData(playername);
        ScoreManager.Instance.ChangeValue(item.shipPoints);
        for (int i = 0; i < ships.Length; i++)
        {
            if (ships[i].name == playername)
            {
                ships[i].SetActive(true);
                Debug.Log("GameManager Awake" + item.shipPoints);
                
                ships[i].GetComponent<PlayerMovement>().playerSpeed = item.shipSpeed;
                break;
            }
        }
        screenSize =Camera.main.ScreenToWorldPoint(new Vector2(Screen.width,Screen.height));
        currentwave = 0;
    }
    private void Start()
    {
        waves = enemy.transform.childCount;
       
        currentwave = 0;
        currentenemy=enemy.transform.GetChild(currentwave).gameObject;
        currentenemy.SetActive(true);
        currentroute = routes.transform.GetChild(currentwave).gameObject;
        currentroute.SetActive(true);
        StartCoroutine(EnemyShooting());
        StartCoroutine(PowerUpSpawning());
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        for(i=0;i<currentenemy.transform.childCount;i++)
        {
            if (currentenemy.transform.GetChild(i).gameObject.activeInHierarchy)
                break;
        }
        if(i==currentenemy.transform.childCount)
        {
           
            StartCoroutine(NextWave());
        }
       
   }
    IEnumerator EnemySpawning()
    {
        while(true)
        {
           
            yield return new WaitForSeconds(2);
            number = Random.Range(1, 4);
            
        }
       
    }
    IEnumerator PowerUpSpawning()
    {
        while (true)
        {
            yield return new WaitForSeconds(powertime);
            randomPowerUp = Random.Range(0, 2);
            SpawnPowerUp(randomPowerUp);
        }

    }
    IEnumerator NextWave()
    {
        
        if(currentwave==waves-1)
        {
            GameWin();
        }
        SetAllWavesFalse();
        currentwave=Mathf.Clamp(currentwave+1,0,waves-1);
        shoottime -= 0.5f;
  
        currentenemy = enemy.transform.GetChild(currentwave).gameObject;
        currentenemy.SetActive(true);
        currentroute=routes.transform.GetChild(currentwave).gameObject;
        currentroute.SetActive(true);
        yield return new WaitForSeconds(2);

    }
    IEnumerator EnemyShooting()
    {
        while (true)
        {
            if (!IsGameOver)
            {
                yield return new WaitForSeconds(shoottime);
                
                SpawnEnemyBullet();
            }
        }
    }
    #endregion
    #region PUBLIC METHODS
    public void SpawnPowerUp(int value)
    {
        GameObject temp;
        if (value == 0)
            temp = SpawnManager.Instance.GetFromPool("ShipPowerUp");
        else
            temp = SpawnManager.Instance.GetFromPool("BulletPowerUp");
        temp.SetActive(true);
        temp.transform.position= new Vector3(Random.Range(-screenSize.x, screenSize.x), screenSize.y - 0.5f, this.transform.position.z);
    }
    public void SpawnEnemy(int number)
    {
        string enemyname = "Enemy"+number.ToString();
        GameObject temp=SpawnManager.Instance.GetFromPool(enemyname);
        temp.transform.position = new Vector3(Random.Range(-screenSize.x, screenSize.x), screenSize.y+1, transform.position.z);
        temp.SetActive(true);

    }
    public void SpawnEnemyBullet()
    {
        GameObject temp = SpawnManager.Instance.GetFromPool("EnemyBullet");
        GameObject enemy=GetActiveEnemy();
        if(enemy!=null && enemy.tag!="BossEnemy")
        {
            temp.transform.position=enemy.transform.position;
            temp.SetActive(true);
        }
    }
    public GameObject GetActiveEnemy()
    {
        
            int number = Random.Range(0, currentenemy.transform.childCount);
            if(currentenemy.transform.GetChild(number).gameObject.activeInHierarchy)
            {
                return (currentenemy.transform.GetChild(number).gameObject);
            }
        
        return null;
        /*for (int i = 0; i < currentenemy.transform.childCount; i++)
        {
            if (currentenemy.transform.GetChild(i).gameObject.activeInHierarchy)
            {
                
                return currentenemy.transform.GetChild(i).gameObject;
            }

        }*/
       
    }
    public void SpawnBullet(string name,Transform spawnPoint)
    {
        if (!IsGameOver)
        {
            GameObject temp = SpawnManager.Instance.GetFromPool(name);

            temp.transform.position = spawnPoint.position;
            temp.SetActive(true);
        }
    }
    
    public void SetAllWavesFalse()
    {
        for(i=0;i<waves;i++)
        {
            enemy.transform.GetChild(i).gameObject.SetActive(false);
            routes.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    public void ChangingShip()
    {
        Vector3 currentPosition;
        currentPosition = ships[currentship].transform.position;
        if (currentship < ships.Length - 1)
            currentship++;
        else
            currentship = 0;
        GameObject.FindGameObjectWithTag("Player").gameObject.SetActive(false);
        ships[currentship].SetActive(true);
        ships[currentship].transform.position = currentPosition;
        ShipData item = GetShipData(ships[currentship].name);
        ScoreManager.Instance.ChangeValue(item.shipPoints);
        ships[currentship].GetComponent<PlayerMovement>().playerSpeed = item.shipSpeed;
    }
    public void GameOver()
    {
        IsGameOver = true;
        shipdataList.score += ScoreManager.Instance.GetScore();
        HandlingJsonData.Instance.UpdateFile(shipdataList);
        gameOverPanel.SetActive(true);
        Invoke("GoToMenu", 2f);
        StopAllCoroutines();
        currentwave = 0;

        
    }
    public void GameWin()
    {
        IsGameOver = true;
        StopAllCoroutines();
        
        shipdataList.score += ScoreManager.Instance.GetScore();
        HandlingJsonData.Instance.UpdateFile(shipdataList);
        gameWinPanel.SetActive(true);
        //Application.Quit();
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public ShipData GetShipData(string value)
    {
       
        shipdataList = HandlingJsonData.Instance.ReadFile();
        for (int i = 0; i < shipdataList.ships.Length; i++)
        {
            if (value == shipdataList.ships[i].name)
                return (shipdataList.ships[i]);
        }
        return null;
    }
    #endregion
    #region PRIVATE METHODS

    #endregion
}
