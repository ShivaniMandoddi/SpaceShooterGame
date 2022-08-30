using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class EnemyWave1 : MonoBehaviour
{
    Vector3 firstPosition;
    void Start()
    {
        firstPosition = transform.position;
            StartCoroutine(EnableEnemy());
       
    }
   

    IEnumerator EnableEnemy()
    {

        //if (!GameManager.Instance.IsGameOver)
        //{
            for (int i = 0; i < transform.childCount; i++)
            {

               Vector3 startPosition=transform.GetChild(i).position;
            
                transform.GetChild(i).gameObject.SetActive(true);
            
                yield return new WaitForSeconds(0.2f);
            }
        //}

    }
   
              

        

    }
