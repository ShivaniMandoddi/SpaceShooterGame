using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    int count = 10;
    public ParticleSystem particle;
    public ParticleSystem blast;
  
    void Start()
    {
        if (this.gameObject.tag == "BossEnemy")
        {
            count = 150;
            StartCoroutine(SpawningBullets());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
        if (collision.gameObject.layer == 9)
        {
            count--;
            particle.transform.position = this.transform.position;
            particle.Play();
            //collision.gameObject.GetComponent<AudioSource>().Play();
            //collision.gameObject.SetActive(false);
            Sequence myseq = DOTween.Sequence();
            myseq.Append(gameObject.GetComponent<SpriteRenderer>().DOFade(0f, 0.3f)).Append(gameObject.GetComponent<SpriteRenderer>().DOFade(1f, 0.3f));
            if (count == 0)
            {
                blast.transform.position = this.transform.position;
                blast.Play();
                //collision.gameObject.SetActive(false);
                this.gameObject.SetActive(false);
            }
        }

    }
}


