using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowAirController:MonoBehaviour {

	public static BlowAirController instance;
	public float blowTime = 1f;
	float timeAfterBlow;

	[SerializeField] GameObject arrowObject;


	private void Start() {
		instance=this;
	}

	private void Update() {
		timeAfterBlow+=Time.deltaTime;
		if(timeAfterBlow>blowTime) {
			transform.position=Vector3.left*100000000f;
		}


	}

}
