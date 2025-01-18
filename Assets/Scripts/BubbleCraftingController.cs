using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleCraftingController:MonoBehaviour {

	[HideInInspector]public BubbleMaterial[] materialsSelected = new BubbleMaterial[2];

	public Image[] ingredientDisplays;
	public Image[] ingredientButtons;
	public Sprite[] spriteIngredients;

	public static HashSet<BubbleMaterial> materialsUnlocked=new HashSet<BubbleMaterial>();

	private void Start() {
		for(int i = 0;i<6;i++) materialsUnlocked.Add((BubbleMaterial)i);
	}

	public void OnAddIngredient(int bubbleMaterial) {
		if(materialsSelected[0]==BubbleMaterial.None) {
			materialsSelected[0]=(BubbleMaterial)bubbleMaterial;
		} else if(materialsSelected[1]==BubbleMaterial.None) {
			materialsSelected[1]=(BubbleMaterial)bubbleMaterial;
		} else {
			materialsSelected[0]=materialsSelected[1];
			materialsSelected[1]=(BubbleMaterial)bubbleMaterial;
		}
	}

	public void OnRemoveIngredient(int slot) {
		materialsSelected[slot]=BubbleMaterial.None;
	}

	private void Update() {
		ingredientDisplays[0].sprite=spriteIngredients[(int)materialsSelected[0]];
		ingredientDisplays[1].sprite=spriteIngredients[(int)materialsSelected[1]];
		for(int i=0;i<ingredientButtons.Length;i++){
			ingredientButtons[i].gameObject.SetActive(materialsUnlocked.Contains((BubbleMaterial)i));
		}
	}

}
