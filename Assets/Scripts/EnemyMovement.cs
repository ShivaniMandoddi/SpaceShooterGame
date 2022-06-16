using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    #region PUBLIC VARIABLES
    public float enemySpeed;
    public Transform bulletSpawnPoint;
    #endregion
    #region PRIVATE VARIABLES

    #endregion
    #region MONOBEHAVIOUR METHODS
    private void OnEnable()
    {
        if (bulletSpawnPoint != null && this.gameObject.activeInHierarchy == true)
        {
            StartCoroutine(SpawningBullet());
        }
    }
    
    void Update()
    {
        transform.Translate(Vector2.up * enemySpeed * Time.deltaTime);
    }
    IEnumerator SpawningBullet()
    {
        yield return new WaitForSeconds(1);
        GameManager.Instance.SpawnBullet("EnemyBullet",bulletSpawnPoint);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer==8)
        {
            ScoreManager.Instance.ScoreDecrement(1);
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
