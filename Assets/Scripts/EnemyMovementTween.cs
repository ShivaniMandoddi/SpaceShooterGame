using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Enemy
{
    public class EnemyMovementTween : MonoBehaviour
    {
        // Start is called before the first frame update
        public Transform[] routepoints;
        Vector3[] points;
        void Start()
        {
            points = new Vector3[routepoints.Length];
            for (int i = 0; i < routepoints.Length; i++)
            {
                points[i] = routepoints[i].position;
            }


            transform.DOPath(points, 6f, PathType.CatmullRom, PathMode.Ignore, 10, Color.blue).OnComplete(Pattern);

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void Pattern()
        {

            float indexx = -1.8f;
            float y = 3f;
            float x = indexx;
            float xlimit = 2f;
            for (int i = 0; i < transform.childCount; i++)
            {

                if (x >= xlimit)
                {
                    y -= 1f;
                    indexx += 0.6f;
                    x = indexx;
                    xlimit -= 0.6f;
                }
                transform.GetChild(i).gameObject.transform.DOMove(new Vector3(x, y, 0f), 3f);
                x += 0.6f;

            }

            //StartCoroutine(Movement());
            // transform.GetChild(i).gameObject.transform.position = Vector3.MoveTowards(transform.GetChild(i).transform.position, new Vector3(x, y, 0f), 3f * Time.deltaTime);

        }
      

    }
    
}
