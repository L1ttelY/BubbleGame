using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class SoftBody : MonoBehaviour
{
    public SpriteShapeController spriteShape;
    public Transform[] points;
    public const float splineOffset = 0.5f;

    private void Awake()
    {
        UpdateVerticies();
    }

    private void Update()
    {
        UpdateVerticies();
    }

    public void UpdateVerticies()
    {
        for (int i = 0; i < points.Length-1; i++)
        { 
            Vector2 _vertex= points[i].localPosition;
            Vector2 _towardsCenter = (Vector2.zero - _vertex).normalized;
            float _colliderRadius = points[i].gameObject.GetComponent<CircleCollider2D>().radius;
            try 
            {
                spriteShape.spline.SetPosition(i,(_vertex - _towardsCenter * _colliderRadius));
            }
            catch 
            {
                Debug.Log("<color=red>¾àÀë¹ý³¤</color>");
                spriteShape.spline.SetPosition(i, (_vertex - _towardsCenter * (_colliderRadius + splineOffset)));
            }
            Vector2 LT = spriteShape.spline.GetLeftTangent(i);
            Vector2 newRT = Vector2.Perpendicular(_towardsCenter) * LT.magnitude;
            Vector2 newLT = Vector2.zero - (newRT);

            spriteShape.spline.SetRightTangent(i,newRT);
            spriteShape.spline.SetLeftTangent(i,newLT);
        }
    }
}
