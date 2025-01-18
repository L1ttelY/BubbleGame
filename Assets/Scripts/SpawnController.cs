using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController:MonoBehaviour {

	[SerializeField] GameObject prefab;
	[SerializeField] Transform targetPosition;

	public void TrySpawn() {
		if(BubbleController.instance) return;
		Instantiate(prefab,targetPosition);
	}

}
