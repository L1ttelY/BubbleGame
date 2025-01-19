using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyHandler:MonoBehaviour {
	[SerializeField] GameObject target;
	public void DoJob() {
		Destroy(target);
	}
}
