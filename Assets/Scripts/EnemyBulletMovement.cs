using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletMovement : MonoBehaviour
{
    #region PUBLIC VARIABLES
    public float bulletSpeed;
    public GameObject enemy;
    Vector3 direction;
    #endregion
    #region PRIVATE VARIABLES

    #endregion
    #region MONOBEHAVIOUR METHODS
    private void OnEnable()
    {
        enemy = GameObject.FindGameObjectWithTag("BossEnemy");
        if (enemy!=null && enemy.activeInHierarchy==true)
        {
            
            direction = this.transform.position - enemy.transform.position;
            direction.Normalize();
            StartCoroutine(BossBullets());
        }
        else
           StartCoroutine(Movement());
    }
    
    IEnumerator BossBullets()
    {
        while(true)
        {
           
            transform.Translate(direction* 1f * Time.deltaTime);
            yield return null;
        }
    
    }
    IEnumerator Movement()
    {
        while(true)
        {
            transform.Translate(Vector2.down * bulletSpeed * Time.deltaTime);
            yield return null;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            ScoreManager.Instance.ScoreDecrement();
            collision.gameObject.SetActive(false);
            GameManager.Instance.GameOver();
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
