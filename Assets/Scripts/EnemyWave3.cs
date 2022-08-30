using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyWave3 : MonoBehaviour
{
    public GameObject route;
    Vector3[] points;
    Vector3[] enemypositions;
    void Start()
    {
        Debug.Log("zsxdcfvgbh");
       points=new Vector3[route.transform.childCount];
        enemypositions=new Vector3[transform.childCount];
        for (int i = 0; i < route.transform.childCount; i++)
        {
            points[i] = route.transform.GetChild(i).position;
            
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            enemypositions[i] = transform.GetChild(i).localPosition;
           
        }
        StartCoroutine(Move1());
       // transform.DOPath(points, 5f, PathType.CatmullRom, PathMode.Ignore, 10, Color.blue).OnComplete(Move2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Move1()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Sequence myseq = DOTween.Sequence();
            myseq.Append(transform.GetChild(i).DOPath(points, 4f, PathType.CatmullRom, PathMode.Ignore, 10, Color.blue)).Append(transform.GetChild(i).DOMove(enemypositions[i], 0.5f));
            yield return new WaitForSeconds(0.1f);
        }
    }
    public void Move2()
    {
        transform.position = Vector3.zero;
        for (int i = 0; i < enemypositions.Length; i++)
        {
            
            transform.GetChild(i).DOMove(enemypositions[i], 2f);
        }
    }
}
