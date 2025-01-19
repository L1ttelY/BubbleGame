using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindAudioController:MonoBehaviour {

	float timeNotBlowing = 10;
	AudioSource audioSource;
	private void Start() {
		audioSource=GetComponent<AudioSource>();
	}

	private void OnTriggerStay2D(Collider2D collision) {
		if(collision.GetComponent<BubbleNodeController>()) timeNotBlowing=0;
	}
	private void Update() {
		timeNotBlowing+=Time.deltaTime;
		audioSource.volume=(timeNotBlowing<0.1f) ? 1 : 0;
	}


}
