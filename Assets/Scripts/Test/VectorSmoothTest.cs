
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class VectorSmoothTest :MonoBehaviour
{
    [Header("Links")]
    public Transform PointA;
    public Transform PointB;
    public Transform PointC;

    public float SmoothDistance = 0.15f;
    public float SmoothPower = 1.0f;

    [Header("Debug")]
    [SerializeField]
    private float _pointSize = 0.02f;
    [SerializeField]
    private Vector2 _pointA = new Vector2(0,0);
    [SerializeField]
    private Vector2 _pointB = new Vector2(2, 4);
    [SerializeField]
    private Vector2 _pointC = new Vector2(4, -2);
    [SerializeField]
    private List<Vector2> _additionalPoints = new List<Vector2>();

    [Button]
    private void Calculate()
    {
        _pointA = PointA.position;
        _pointB = PointB.position;
        _pointC = PointC.position;
        _additionalPoints = GameMaths.SmoothAngle(_pointA, _pointB, _pointC, SmoothDistance, SmoothPower, true);
    }

    [Button]
    private void NormalTest()
    {
        _pointA = PointA.position;
        _pointB = PointB.position;
        _pointC = PointC.position;

        Vector2 dir1 = _pointB - _pointA;
        Vector2 dir2 = _pointC - _pointB;
        float crossProduct = GameMaths.CrossProduct(dir1, dir2);
        Vector2 normal1 = GameMaths.GetNormal(dir1);
        Vector2 normal2 = GameMaths.GetNormal(dir2);
        if (crossProduct < 0)
        {
            normal1 *= -1;
            normal2 *= -1;
        }

        Debug.DrawLine(_pointA, _pointA + normal1, Color.green, 3f);
        Debug.DrawLine(_pointC, _pointC + normal2, Color.green, 3f);

        Vector2 intersectionPoint;
        GameMaths.TryGetLineIntersectionPoint(_pointA, normal1, _pointC, normal2, out intersectionPoint);
        DebugFunctions.DrawPoint(intersectionPoint, _pointSize, Color.red, 3f);
    }

    private void OnGUI()
    {
        Debug.DrawLine(_pointA, _pointB, Color.blue);
        Debug.DrawLine(_pointB, _pointC, Color.blue);
        for (int i = 0; i < _additionalPoints.Count; i++)
        {
            DebugFunctions.DrawPoint(_additionalPoints[i], _pointSize, Color.red);
        }
    }

   
}