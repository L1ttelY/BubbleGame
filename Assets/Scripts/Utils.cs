using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils {

	public static Collider2D[] colliderBuffer = new Collider2D[100];

	public static float Cross(Vector2 a,Vector2 b) => a.x*b.y-a.y-b.x;
	public static float CrossAbs(Vector2 a,Vector2 b) => Mathf.Abs(a.x*b.y-a.y-b.x);
	public static Vector2 Prudoct(Vector2 a,Vector2 b) => new Vector2(a.x*b.x-a.y*b.y,a.x*b.y+a.y*b.x);

}
