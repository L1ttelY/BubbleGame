using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController:MonoBehaviour {

	[SerializeField] GameObject prefab;
	[SerializeField] Transform targetPosition;

	public void TrySpawn() {
		if(BubbleController.instance) return;
		BubbleController newBubble = Instantiate(prefab,targetPosition).GetComponent<BubbleController>();
		newBubble.propertyDensity=BubbleCraftingController.ContainsMaterial(BubbleMaterial.Heavy);
		newBubble.propertyElasticness=BubbleCraftingController.ContainsMaterial(BubbleMaterial.Elastic);
		newBubble.propertyToughness=BubbleCraftingController.ContainsMaterial(BubbleMaterial.Toughness);
		newBubble.propertyWaterThrough=BubbleCraftingController.ContainsMaterial(BubbleMaterial.Submerge);
		newBubble.propertyCanSplit=BubbleCraftingController.ContainsMaterial(BubbleMaterial.Split);
	}

}
