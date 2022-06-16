using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    #region PUBLIC VARIABLES
    public float bulletSpeed;
    #endregion
    #region PRIVATE VARIABLES

    #endregion
    #region MONOBEHAVIOUR METHODS
    void Start()
    {

    }
    void Update()
    {
        transform.Translate(Vector2.up * bulletSpeed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer==6)
        {
            ScoreManager.Instance.ScoreIncrement(2);
            collision.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
            
        }
    }
    private void OnBecameInvisible()
    {
        this.gameObject.SetActive(false);
    }

    #endregion

    #region PUBLIC METHODS

    #endregion
    #region PRIVATE METHODS

    #endregion
}
