using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletMovement : MonoBehaviour
{
    #region PUBLIC VARIABLES
    public float bulletSpeed;
    #endregion
    #region PRIVATE VARIABLES

    #endregion
    #region MONOBEHAVIOUR METHODS
    private void Update()
    {
        transform.Translate(Vector2.down * bulletSpeed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
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
