using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpMovement : MonoBehaviour
{
    

  
    void Update()
    {
        transform.Translate(Vector2.down * 3f * Time.deltaTime);
    }
    private void OnBecameInvisible()
    {
        this.gameObject.SetActive(false);
    }
}
