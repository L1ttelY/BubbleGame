using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class shuiguan : MonoBehaviour
{
    [Header("ÇÐ»»¹Ø¿¨")]
    public int sceneIndex;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Points")
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }
}
