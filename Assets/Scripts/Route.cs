using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    [SerializeField]
    private GameObject[] controlPosition;
    private Vector2 gizmosPosition;
    public List<Points> point;
    private void Awake()
    {
        int number = Random.Range(0, point.Count);
        for (int i = 0; i < controlPosition.Length; i++)
        {
            //controlPosition[i].transform.position = point[number].points[i] + this.transform.position;
            //Debug.Log(controlPosition[i].transform.position);
        }
    }
    private void OnDrawGizmos()
    {
        
        for (float t=0;t<=1;t+=0.05f)
        {
            gizmosPosition = Mathf.Pow(1 - t, 3) * controlPosition[0].transform.position+
                3*Mathf.Pow(1-t,2)*t*controlPosition[1].transform.position+
                3 * Mathf.Pow(t, 2) * (1 - t) * controlPosition[2].transform.position+
                Mathf.Pow(t, 3) * controlPosition[3].transform.position;
            Gizmos.DrawSphere(gizmosPosition, 0.15f);
           // Debug.Log(controlPosition[0].transform.position);

        }
       // Gizmos.DrawLine(new Vector2(controlPosition[0].position.x, controlPosition[0].position.y),
         //   new Vector2(controlPosition[1].position.x, controlPosition[1].position.y));
        //Gizmos.DrawLine(new Vector2(controlPosition[2].position.x, controlPosition[2].position.y),
          //  new Vector2(controlPosition[3].position.x, controlPosition[3].position.y));
    }
    
}
[System.Serializable]
public class Points
{
    public Vector3[] points;
}
