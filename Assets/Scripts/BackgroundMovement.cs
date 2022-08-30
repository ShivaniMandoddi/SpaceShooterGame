using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    #region PUBLIC VARIABLES
    public float backgroundSpeed;
    #endregion
    #region PRIVATE VARIABLES

    #endregion
    #region MONOBEHAVIOUR METHODS
    void Start()
    {
        
    }

    
    void Update()
    {
        transform.Translate(Vector3.left * backgroundSpeed * Time.deltaTime);
        if(transform.position.x<=-19f)
        {
            transform.position=new Vector3(38f, transform.position.y, transform.position.z);
        }
    }
    #endregion
    #region PUBLIC METHODS

    #endregion
    #region PRIVATE METHODS

    #endregion

}
