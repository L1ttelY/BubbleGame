using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EatDaoju : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Points")
        {
        Debug.Log("<color=yellow>�Ե�����");
        Destroy(gameObject);
        }
    }
    
}
