using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController:MonoBehaviour {

	public float propertyToughness;
	public float propertyElasticness;
	public float propertyDensity;
	public bool propertyWaterTight;

	public BubbleNodeController[] nodes;

	private void Start() {
		nodes=GetComponentsInChildren<BubbleNodeController>();
	}

	private void FixedUpdate() {
		TryFixBubbleShape();
	}

	void TryFixBubbleShape() {
		for(int i = 0;i<nodes.Length;i++) {
			int j = (i+1)%nodes.Length;
			Angle angle0 = new Angle(nodes[i].transform.position-transform.position);
			Angle angle1 = new Angle(nodes[j].transform.position-transform.position);
			if(angle1.IfBetween(angle0,angle0-Angle.Degree(90))) continue;
			Vector3 position0 = nodes[i].transform.position;
			nodes[i].transform.position=nodes[j].transform.position;
			nodes[j].transform.position=position0;

			break;
		}

	}


}
