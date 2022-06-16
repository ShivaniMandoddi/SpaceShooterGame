using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region PUBLIC VARIABLES
    public float playerSpeed;
    public Transform bulletSpawningPoint;
    #endregion
    #region PRIVATE VARIABLES
    float inputValue;
    float screenwidth;
    #endregion
    #region MONOBEHAVIOUR METHODS
    void Start()
    {
        
        Vector2 screenSize=Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        screenwidth = screenSize.x-1;
     
    }

   
    void Update()
    {
        inputValue = Input.GetAxis("Horizontal");
        if (inputValue != 0)
        {
            transform.Translate(new Vector3(inputValue * playerSpeed * Time.deltaTime, transform.position.y));
            if (transform.position.x > screenwidth)
            {
                transform.position = new Vector2(screenwidth, transform.position.y);
            }
            if (transform.position.x < -screenwidth)
            {
                transform.position = new Vector2(-screenwidth, transform.position.y);
            }
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.Instance.SpawnBullet("Bullet",bulletSpawningPoint);
        }
        
    }
    
    #endregion
    #region PUBLIC METHODS

    #endregion
    #region PRIVATE METHODS

    #endregion

}
