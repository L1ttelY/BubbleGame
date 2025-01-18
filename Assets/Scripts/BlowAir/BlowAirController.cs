using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowAirController:MonoBehaviour {

	public static BlowAirController instance;
	public float blowTime = 1f;
	float timeAfterBlow = float.MaxValue;
	FlowingArea flowArea;

	[SerializeField] GameObject arrowObject;
	[SerializeField] float blowSpeed;

	enum State {
		Choosing,
		Working,
		Idle
	}
	State currentState = State.Idle;

	private void Start() {
		instance=this;
		flowArea=GetComponentInChildren<FlowingArea>();
	}

	private void Update() {
		timeAfterBlow+=Time.deltaTime;

		Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		switch(currentState) {
		case State.Choosing:
			Angle angle = new Angle(mousePosition-(Vector2)arrowObject.transform.position);
			arrowObject.transform.rotation=angle.quaternion;
			if(!Input.GetMouseButton(0)) {
				transform.position=arrowObject.transform.position;
				transform.rotation=arrowObject.transform.rotation;

				arrowObject.transform.position=Vector3.left*100000f;
				timeAfterBlow=0;
				flowArea.velocity=angle.vector*blowSpeed;

				currentState=State.Working;
			}
			break;
		case State.Working:
			timeAfterBlow+=Time.deltaTime;
			if(timeAfterBlow>blowTime) {
				transform.position=Vector3.left*100000f;
				arrowObject.transform.position=Vector3.left*100000f;
				currentState=State.Idle;
			}
			break;
		case State.Idle:

			transform.position=Vector3.left*100000f;
			arrowObject.transform.position=Vector3.left*100000f;

			if(Input.GetMouseButtonDown(0)) {
				if(!Physics2D.OverlapPoint(mousePosition,LayerMask.GetMask("Collider","ColliderSticky","Spike","Bubble","BubbleElastic","Liquid"))) {
					arrowObject.transform.position=mousePosition;
					currentState=State.Choosing;
				}
			}
			break;
		}

	}

}
