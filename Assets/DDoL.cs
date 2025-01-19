using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDoL:MonoBehaviour {

	static Dictionary<string,DDoL> instances = new Dictionary<string,DDoL>();

	void Start() {
		if(instances.ContainsKey(gameObject.name)) {
			Destroy(gameObject);
		} else DontDestroyOnLoad(gameObject);
	}

	void Update() {

	}
}
