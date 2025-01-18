using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class shuiguan : MonoBehaviour
{
    public bool isEnd;
    public Transform targetposition;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Points")
        { 
        collision.gameObject.transform.position = targetposition.position;
        }
    }
}
