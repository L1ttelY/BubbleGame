using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick:MonoBehaviour {
	public Animator animator;
	public SpawnController chukou;
	public void Onclick() {
		Debug.Log("����ķ��");
		animator.SetTrigger("Click");
		Invoke("shengcheng",1.5f);

	}
	public void shengcheng() {
		SpawnController.instance.TrySpawn();
	}
}
