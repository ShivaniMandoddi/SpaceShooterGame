using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiezerFollow : MonoBehaviour
{
    [SerializeField]
    private Transform[] routes;
    private int routeToGo;
    private float tParam;
    private Vector3 enemyPosition;
    private float speedModifier;
    private bool coroutineAllowed;
    int count=20;
   
    void Start()
    {
       // point = transform.position;
        routeToGo = 0;
        tParam= 0f;
        speedModifier = 0.25f;
        coroutineAllowed = true;
        if (this.gameObject.tag == "BossEnemy")
        {
            count = 150;
            StartCoroutine(SpawningBullets());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.IsGameOver)
        {
            if (coroutineAllowed)
                StartCoroutine(GoToWave(routeToGo));
           

        }
        if(GameManager.Instance.IsGameOver)
        {
            StopAllCoroutines();
        }
       
      

    }
    IEnumerator GoToWave(int routeNumber)
    {
        coroutineAllowed = false;
        Vector2 p0 = routes[routeNumber].GetChild(0).position;
        Vector2 p1 = routes[routeNumber].GetChild(1).position;
        Vector2 p2 = routes[routeNumber].GetChild(2).position;
        Vector2 p3 = routes[routeNumber].GetChild(3).position;
        
        while (tParam<1)
        {
          
            tParam += Time.deltaTime * speedModifier;
            enemyPosition = Mathf.Pow(1 - tParam, 3) * p0 +
                3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
                3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
                Mathf.Pow(tParam, 3) * p3;
           
           // Quaternion torotate = Quaternion.LookRotation(Vector3.forward, enemyPosition);
            //this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, torotate, 360f * Time.deltaTime);
            transform.position = enemyPosition;
            yield return new WaitForEndOfFrame();
        }
        tParam = 0f;
        routeToGo += 1;
        if (routeToGo > routes.Length - 1)
            routeToGo = 0;
        coroutineAllowed = true;
    }
    IEnumerator SpawningBullets()
    {
        while (true)
        {
            yield return new WaitForSeconds(4.5f);
            for (int i = 0; i < this.transform.childCount; i++)
            {
               
                GameObject temp = SpawnManager.Instance.GetFromPool("EnemyBullet");
                temp.transform.position = transform.GetChild(i).position;
                temp.SetActive(true);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer==9)
        {
            count--;
            if(count==0)
            {
                
                collision.gameObject.SetActive(false);
                this.gameObject.SetActive(false);
            }
        }
        
    }
}
