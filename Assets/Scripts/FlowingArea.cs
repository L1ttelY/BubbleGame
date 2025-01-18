using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FlowingArea:MonoBehaviour {

	public static HashSet<FlowingArea> instances=new HashSet<FlowingArea>();

	public Vector2 velocity;
	public Collider2D collider;

	private void OnEnable() {
		collider=GetComponent<Collider2D>();
		instances.Add(this);
	}
	private void OnDisable() {
		instances.Remove(this);
	}

}
