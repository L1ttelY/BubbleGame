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

	}

	Vector2 localAirVelocity;
	static Dictionary<Vector2,Vector2> dictProbe = new Dictionary<Vector2,Vector2>();
	readonly static float probeStep;

	private void FixedUpdate() {

		
		 
	}
}
