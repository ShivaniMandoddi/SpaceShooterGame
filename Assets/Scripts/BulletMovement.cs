using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    #region PUBLIC VARIABLES
   
    public float bulletSpeed;
    public GameObject player;
   
    Vector3 direction;
    
    #endregion
    #region PRIVATE VARIABLES
    
    #endregion
    #region MONOBEHAVIOUR METHODS

    private void Start()
    {
         
    }
    private void OnEnable()
    {
       
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            direction = this.transform.position - player.transform.position;
            direction.Normalize();
            if (player.name == "Ship1")
                StartCoroutine(Movement());
            else
                StartCoroutine(Movement2());
        }
    }
    IEnumerator Movement()
    {
        while(true)
        {
            transform.Translate(direction * bulletSpeed * Time.deltaTime);
            yield return null;
        }
    }
    IEnumerator Movement2()
    {
        while (true)
        {
            
            transform.Translate(Vector2.up * bulletSpeed * Time.deltaTime);
            yield return null;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.gameObject.layer==6)
        {
          
            gameObject.GetComponent<AudioSource>().Play();
            ScoreManager.Instance.ScoreIncrement();
            //collision.gameObject.SetActive(false);
            StartCoroutine(BackToPool());
            
            
        }
    }
    IEnumerator BackToPool()
    {
        yield return new WaitForSeconds(0.2f);
        this.gameObject.SetActive(false);
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
