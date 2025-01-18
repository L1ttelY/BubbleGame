using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class SoftBody : MonoBehaviour
{
    public SpriteShapeController spriteShape;
    public Transform[] points;
    public const float splineOffset = 0.5f;
    public GameObject xiaopaopao;
    public Quaternion spawnRotation = Quaternion.identity; // 初始生成的旋转角度
    private void Awake()
    {
        UpdateVerticies();
    }

    private void Update()
    {
        UpdateVerticies();
        if (Input.GetKeyDown(KeyCode.B))
        {
            CreateSmallBubble();
        }
    }

    public void CreateSmallBubble()
    { 
    int x = Random.Range(0, points.Length-1);
        Vector3 spawnOffset = points[x].position; // 获取当前对象的位置
        Instantiate(xiaopaopao, spawnOffset, spawnRotation);
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
                spriteShape.spline.SetPosition(i,(_vertex));
            }
            catch 
            {
                Debug.Log("<color=red>距离过长</color>");
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
