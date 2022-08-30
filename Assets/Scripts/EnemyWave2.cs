using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyWave2 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject route;
    void Start()
    {
        StartCoroutine(Move1());
        StartCoroutine(Move2());

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Move1()
    {
        float j = 0f;
        for (int i = 0; i < 8; i++)
        {
            Sequence myseq = DOTween.Sequence();
            transform.GetChild(i).position = new Vector3(2.5f, -1f, 0f);

            myseq.Append(transform.GetChild(i).transform.DOMoveX(j, 1f)).Append(transform.GetChild(i).DOMove(route.transform.GetChild(i).position, 2f)).Append(transform.GetChild(i).transform.DORotate(new Vector3(0f, 0f, 180f), 1f).SetLoops(int.MaxValue, LoopType.Yoyo));
            j += 0.2f;
            
            yield return new WaitForSeconds(0.2f);
           
        }
       
    }
    IEnumerator Move2()
    {
        float j = 0f;
        for (int i = 8; i < 16; i++)
        {
            Sequence myseq = DOTween.Sequence();
            transform.GetChild(i).position = new Vector3(-2.5f, -1f, 0f);
           
            myseq.Append(transform.GetChild(i).transform.DOMoveX(j, 1f)).Append(transform.GetChild(i).DOMove(route.transform.GetChild(i).position, 2f)).OnComplete(Move3);
            j -= 0.2f;
            yield return new WaitForSeconds(0.2f);
        }
    }
    public void Move3()
    {
        transform.DOMoveY(2f, 5f).SetDelay(2f).SetLoops(int.MaxValue, LoopType.Yoyo);
    }

}
