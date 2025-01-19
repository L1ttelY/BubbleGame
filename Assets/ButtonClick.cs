using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ButtonClick:MonoBehaviour {
	public Animator animator;
	[SerializeField] AudioClip sound;
	public SpawnController chukou;
	public string prefabTag = "Points";
	public void Onclick() {
		Debug.Log("µã»÷¹Ä·ç»ú");
		animator.SetTrigger("Click");
		Invoke("shengcheng",1.5f);
		AudioPlayer.PlayAudio(transform.position,sound);
	}
	public void shengcheng() {
		SpawnController.instance.TrySpawn();
	}
	public void YijianXiaohui() {
		GameObject existingObject = GameObject.FindWithTag(prefabTag);
		Destroy(existingObject);
	}
}
