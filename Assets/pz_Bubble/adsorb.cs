using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class adsorb : MonoBehaviour
{

    public List<GameObject> pointList = new List<GameObject>();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        pointList.Clear();
        if (collision.tag == "point")
        {
           pointList.Add(collision.gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }
   
}
