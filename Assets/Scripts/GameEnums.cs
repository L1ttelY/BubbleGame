using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Element {
	Air,
	Hydrogen,
	Water
}

public enum BubbleMaterial {
	None,
	Toughness,
	Elastic,
	Heavy,
	Split,
	Submerge
}

public class EnumUtils {
	public static readonly float[] elementDensity =
		{ 8,1,100 };

}
