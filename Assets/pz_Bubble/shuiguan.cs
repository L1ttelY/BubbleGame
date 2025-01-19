using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class shuiguan:MonoBehaviour {
	[Header("�л��ؿ�")]
	public int sceneIndex;
	[SerializeField] AudioClip sound;

	public void OnTriggerEnter2D(Collider2D collision) {
		if(collision.tag=="Points") {
			AudioPlayer.PlayAudio(transform.position,sound);
			SceneManager.LoadScene(sceneIndex);
		}
	}
}
