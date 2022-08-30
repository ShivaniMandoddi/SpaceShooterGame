using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class movetween : MonoBehaviour
{
    // Start is called before the first frame update
    //public GameObject prefab;
    public Transform[] waypointstransform;
    List<Vector3> waypoint=new List<Vector3>();
    public Vector3 previousPosition;
    Tween mytween;

   
    void Start()
    {
        previousPosition=this.transform.position;
        foreach (Transform item in waypointstransform)
        {
            waypoint.Add(item.position);
            
        }
        Vector3[] waypoints = waypoint.ToArray();
        //transform.DOMoveX(4, 5);

        //StartCoroutine(Move(waypoints));
        mytween=this.transform.DOPath(waypoints, 5f, PathType.CatmullRom, PathMode.Ignore, 10, Color.yellow).SetLoops(-1, LoopType.Restart).OnWaypointChange(MyCallback);
        //this.transform.DORotate(this.transform.position)
       // this.transform.DOLookAt(this.transform.position,-1f);
     


    }
    /*IEnumerator Move(Vector3[] waypoints) 
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject temp = Instantiate(prefab);
            
            yield return new WaitForSeconds(0.2f);
        }
            
        
    }*/
    // Update is called once per frame
    void Update()
    {
        Rotate();
        Vector3 direction = previousPosition - this.transform.position;
        direction.Normalize();
        // Quaternion rotation=Quaternion.E
        //transform.LookAt(waypoint[index]);
        //Debug.Log(direction);
        //waypoint[index].Normalize();
        Quaternion torotate = Quaternion.LookRotation(Vector3.forward, direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, torotate, 360f * Time.deltaTime);
    }
    public void MyCallback(int index)
    {
        //Debug.Log(index);

        //direction.Normalize();
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //Vector3.Slerp(this.transform.position, direction,1f);
        previousPosition = waypoint[index];
        
        //transform.rotation = Quaternion.AngleAxis(direction.y*Mathf.Rad2Deg, Vector3.forward);
    }
    public void Rotate()
    {
        /*Vector3 direction = transform.position - previousPosition;
        direction.Normalize();
        
        transform.Rotate(direction);
        previousPosition=transform.position;*/
        //transform.LookAt()
    }
    private void OnDrawGizmos()
    {
        
    }
}
