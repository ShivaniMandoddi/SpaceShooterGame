using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region PUBLIC VARIABLES
    public float playerSpeed;
    
    public Transform[] bulletSpawningPoints;
    //public Transform bulletSpawningPoint2;
    #endregion
    #region PRIVATE VARIABLES
    float inputValue;
    float screenwidth;
    float screenheight;
    bool IsPowerUp = false;
    int i;
    #endregion
    #region MONOBEHAVIOUR METHODS
    
    private void OnEnable()
    {
        Vector2 screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        screenwidth = screenSize.x - 1;
        screenheight = screenSize.y - 1;
        i = 1;
        StartCoroutine(SpawningBullet());
    }
    void Update()
    {
        
        if (!GameManager.Instance.IsGameOver)
        {
            Vector3 position = Input.mousePosition;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
            worldPosition.x = Mathf.Clamp(worldPosition.x, -screenwidth-0.5f, screenwidth+0.5f);
            worldPosition.y = Mathf.Clamp(worldPosition.y, -screenheight, screenheight);
            worldPosition.z = transform.position.z;
            transform.position = Vector3.MoveTowards(transform.position, worldPosition, playerSpeed * Time.deltaTime);
           
        }
        
    }
    IEnumerator SpawningBullet()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
           
            
                for (int j = 0; j < i; j++)
                {
                    
                    GameManager.Instance.SpawnBullet("Bullet", bulletSpawningPoints[j]);  
                   
                }
            
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="BulletPowerUp")
        {
            collision.gameObject.SetActive(false);
            if (i < bulletSpawningPoints.Length)
                i++;
            
        }
        if(collision.gameObject.tag=="ShipPowerUp")
        {
            collision.gameObject.SetActive(false);
            GameManager.Instance.ChangingShip();

        }
    }

    #endregion
    #region PUBLIC METHODS

    #endregion
    #region PRIVATE METHODS

    #endregion

}
