using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ButtonClick:MonoBehaviour {
	public Animator animator;
	public SpawnController chukou;
    public string prefabTag = "Points";
    public void Onclick() {
		Debug.Log("����ķ��");
		animator.SetTrigger("Click");
		Invoke("shengcheng",1.5f);

	}
	public void shengcheng() {
		SpawnController.instance.TrySpawn();
    }
    public void YijianXiaohui()
	{
        GameObject existingObject = GameObject.FindWithTag(prefabTag);
		Destroy(existingObject);
    }
}
