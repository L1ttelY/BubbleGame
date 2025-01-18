using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController:MonoBehaviour {

	public float propertyToughness;
	public float propertyElasticness;
	public float propertyDensity;
	public bool propertyWaterTight;

	public float currentSize;
	public float currentEccentricity;
	public float currentSurface;
	public float currentVolume;

	public Element currentContentOriginal;
	public Element currentContentNew;
	public float currentElementTransitionPosition;

	Rigidbody2D rigidBody;
	new PolygonCollider2D collider;

	PolygonCollider2D[] internalColliders;

	private void Start() {
		rigidBody=GetComponent<Rigidbody2D>();
		collider=GetComponent<PolygonCollider2D>();
	}

	private void Update() {
		/*	
		localAirVelocity=Vector3.zero;
		foreach(var internalCollider in internalColliders) {
			foreach(var testFlowingArea in FlowingArea.instances) {
				if(!internalCollider.IsTouching(testFlowingArea.collider)) continue;
				localAirVelocity+=(1f/internalColliders.Length)*testFlowingArea.velocity;
			}
		}
		*/
	}

	Vector2 localAirVelocity;
	static Dictionary<Vector2,Vector2> dictProbe = new Dictionary<Vector2,Vector2>();
	readonly static float probeStep;

	private void FixedUpdate() {

		//生成probe
		dictProbe.Clear();
		Bounds bound = collider.bounds;
		for(float x = bound.min.x;x<=bound.max.x;x+=probeStep) {
			for(float y = bound.min.y;y<=bound.max.y;y+=probeStep) {
				Vector2 probeCurrent = new Vector2(x,y);
				if(!collider.OverlapPoint(probeCurrent)) continue;

				foreach(var testFlowingArea in FlowingArea.instances) {
					if(testFlowingArea.collider.OverlapPoint(probeCurrent)) {
						dictProbe.Add(probeCurrent,testFlowingArea.velocity);
						continue;
					}
				}
				if(!dictProbe.ContainsKey(probeCurrent))
					dictProbe.Add(probeCurrent,Vector2.zero);
			}

		}

		//对于迎风probe进行物理计算
		foreach(var probe in dictProbe){
			
		}
		 
	}
}
