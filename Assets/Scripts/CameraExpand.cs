using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraExpand:MonoBehaviour {

	float referenceAspect = 1920f/1080f;
	float referenceSize = 5;

	void Update() {
		Camera camera = GetComponent<Camera>();
		if(camera.aspect<referenceAspect) {
			camera.orthographicSize=referenceSize*referenceAspect/camera.aspect;
		} else camera.orthographicSize=referenceSize;


	}

}
