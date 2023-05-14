using System.Collections.Generic;
using UnityEngine;

public enum LineRendererTextureType
{
    Stretch,
    ByPoints,
    ByDistance
}

public enum LineRendererTextureDirection
{
    Horizontal,
    Vertical,
}

//                          1--точка    UV(1,1)                                                               
// side point  2   _________*________   3   side point                                                                     
//                |4        ^     5 /|                                                           
//                |               / 1|                                                           
//                |              /   |                                                           
//                |             /    |                                                           
//                |    B       /     |                                                           
//                |          /       |            0, 1, 2, 3, 4, 5 - вертиксы                                               
//                |         /        |            A, B - треугольники                                               
//                |        /         |                                                           
//                |      /     A     |                                                           
//                |     /            |                                                           
//                |    /             |                                                           
//                |  /               |                                                           
//                |3/  0    ^       2|                                                           
// side point  0  |/________*________|   1  side point                                                                       
//          UV(0,0)         0--точка                                                                  

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class LineRendererNew : MonoBehaviour
{
    private MeshFilter _meshFilter;
    private MeshRenderer _meshRenderer;

    private Mesh _mesh;
   

    [SerializeField] private LineRendererTextureType _textureType = LineRendererTextureType.Stretch;
    [SerializeField] private float _texturePeriod = 1;
    [SerializeField] private LineRendererTextureDirection _textureDirection = LineRendererTextureDirection.Horizontal;
    [SerializeField] private bool _textureInverted = false;
    [SerializeField] private Axis _axis;
    [SerializeField] private AnimationCurve _widthCurve = AnimationCurve.Linear(0, 1, 1, 1);
    [SerializeField] private float _widthMultiplier = 0.25f;
    [SerializeField] private bool _smoothAngles = true;
    [SerializeField] private float _smoothDistance = 0.25f;
    [SerializeField] private float _smoothPower = 1.5f;
    [SerializeField] private bool _worldPos = true;

    [SerializeField] private List<Vector3> _points;
    [SerializeField] private List<Vector3> _sidePoints;
    [SerializeField] private Vector3[] _vertices;
    [SerializeField] private int[] _triangles;
    [SerializeField] private Vector2[] _uvs;

    public LineRendererTextureType TextureType { get => _textureType; set => _textureType = value; }
    public float TexturePeriod { get => _texturePeriod; set => _texturePeriod = value; }
    public LineRendererTextureDirection TextureDirection { get => _textureDirection; set => _textureDirection = value; }
    public bool TextureInverted { get => _textureInverted; set => _textureInverted = value; }
    public Axis Axis { get => _axis; set => _axis = value; }
    public AnimationCurve WidthCurve { get => _widthCurve; set => _widthCurve = value; }
    public float WidthMultiplier { get => _widthMultiplier; set => _widthMultiplier = value; }
    public bool SmoothAngles { get => _smoothAngles; set => _smoothAngles = value; }
    public bool WorldPos { get => _worldPos; set => _worldPos = value; }


    readonly int VERT_NUMBER = 6;
    readonly List<int> VERT_POSITIONS = new List<int>() { 0, 3, 1, 0, 2, 3 };



    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        _meshFilter = this.GetComponent<MeshFilter>();
        _meshRenderer = this.GetComponent<MeshRenderer>();
        _mesh = new Mesh();
        _meshFilter.mesh = _mesh;
    }

    public void Show()
    {
        _meshRenderer.enabled = true;
    }

    public void Hide()
    {
        _meshRenderer.enabled = false;
    }

    public void LoadPoints(List<Vector3> points)
    {
        _points = points;
        if (_smoothAngles) _points = AddSmoothPoints(_points, _axis, _smoothDistance, _smoothPower);
        _sidePoints = GetSidePointsAll3D(_points, _axis, _widthCurve, _widthMultiplier);

        if (_worldPos) _sidePoints = ToWorldPosition(_sidePoints);
        int verticlesCount = (_sidePoints.Count - 2) * 3;
        int trianglesCount = verticlesCount;
        int uvsCount = verticlesCount;
        _vertices = new Vector3[verticlesCount];
        _triangles = new int[trianglesCount];
        _uvs = new Vector2[uvsCount];

        for (int i = 0; i < _sidePoints.Count - 2; i = i + 2)
        {
            int v = i * 3;
            _vertices[v] = _sidePoints[i + VERT_POSITIONS[0]];
            _vertices[v + 1] = _sidePoints[i + VERT_POSITIONS[1]];
            _vertices[v + 2] = _sidePoints[i + VERT_POSITIONS[2]];

            _vertices[v + 3] = _sidePoints[i + VERT_POSITIONS[3]];
            _vertices[v + 4] = _sidePoints[i + VERT_POSITIONS[4]];
            _vertices[v + 5] = _sidePoints[i + VERT_POSITIONS[5]];
        }

        for (int i = 0; i < _triangles.Length; i++)
        {
            _triangles[i] = i;
        }

        switch (_textureType)
        {
            case LineRendererTextureType.Stretch:
                _uvs = CalculateUVsStretch(_vertices);
                break;
            case LineRendererTextureType.ByPoints:
                _uvs = CalculateUVsByPoints(_vertices, _texturePeriod);
                break;
            case LineRendererTextureType.ByDistance:
                _uvs = CalculateUVsByDystance(_vertices, _texturePeriod, _points);
                break;
            default:
                _uvs = CalculateUVsStretch(_vertices);
                break;
        }

        _mesh.Clear();
        _mesh.vertices = _vertices;
        _mesh.triangles = _triangles;
        _mesh.uv = _uvs;
    }



    private List<Vector2> GetPoints2D(List<Vector3> points, Axis axis)
    {
        List<Vector2> result = new List<Vector2>();

        switch (axis)
        {
            case Axis.X:
                for (int i = 0; i < points.Count; i++)
                {
                    result.Add(new Vector2(points[i].z, points[i].y));
                }
                break;
            case Axis.Y:
                for (int i = 0; i < points.Count; i++)
                {
                    result.Add(new Vector2(points[i].x, points[i].z));
                }
                break;
            case Axis.Z:
                for (int i = 0; i < points.Count; i++)
                {
                    result.Add(new Vector2(points[i].x, points[i].y));
                }
                break;
            default:
                break;
        }

        return result;
    }

    (Vector2, Vector2) GetSidePointsPair2D(Vector2 position, Vector2 previousPosition, Vector2 nextPosition, float width, bool addPositionToResult)
    {
        Vector2 sidePoint1 = new Vector2();
        Vector2 sidePoint2 = new Vector2();

        Vector2 direction1 = position - previousPosition;
        Vector2 direction2 = nextPosition - position;
        direction1 = direction1.magnitude > 0.01f ? direction1.normalized : Vector2.zero;
        direction2 = direction2.magnitude > 0.01f ? direction2.normalized : Vector2.zero;
        Vector2 directionResult = (direction1 + direction2).normalized;
        sidePoint1 = new Vector2(-directionResult.y * width * 0.5f, directionResult.x * width * 0.5f);
        sidePoint2 = new Vector2(directionResult.y * width * 0.5f, -directionResult.x * width * 0.5f);

        if (addPositionToResult)
        {
            sidePoint1 += position;
            sidePoint2 += position;
        }

        return (sidePoint1, sidePoint2);
    }

    (Vector3, Vector3) GetSidePointsPair3D(Vector3 position, Vector3 previousPosition, Vector3 nextPosition, float width, Axis axis, bool addPositionToResult)
    {
        Vector2 positionV2 = GameMaths.Vector3ToVector2(position, axis);
        Vector2 previousPositionV2 = GameMaths.Vector3ToVector2(previousPosition, axis);
        Vector2 nextPositionV2 = GameMaths.Vector3ToVector2(nextPosition, axis);
        (Vector2, Vector2) resultV2 = GetSidePointsPair2D(positionV2, previousPositionV2, nextPositionV2, width, false);
        Vector3 sidePoint1 = GameMaths.Vector2ToVector3(resultV2.Item1, axis);
        Vector3 sidePoint2 = GameMaths.Vector2ToVector3(resultV2.Item2, axis);

        if (addPositionToResult)
        {
            sidePoint1 += position;
            sidePoint2 += position;
        }

        return (sidePoint1, sidePoint2);
    }


    List<Vector3> GetSidePointsAll3D(List<Vector3> points, Axis axis, AnimationCurve widthCurve, float widthMultiplier)
    {
        if (points.Count < 2) return new List<Vector3>();

        List<Vector3> sidePoints = new List<Vector3>();
        Vector3 sidePoint1;
        Vector3 sidePoint2;

        if (points.Count >= 2)
        {
            float width = widthCurve.Evaluate(0) * widthMultiplier;
            var sidePointsCurrent = GetSidePointsPair3D(points[0], points[0], points[1], width, axis, true);
            sidePoint1 = sidePointsCurrent.Item1;
            sidePoint2 = sidePointsCurrent.Item2;
            sidePoints.Add(sidePoint1);
            sidePoints.Add(sidePoint2);
        }


        for (int i = 1; i < points.Count - 1; i++)
        {
            float width = widthCurve.Evaluate((float)i / (float)points.Count) * widthMultiplier;
            var sidePointsCurrent = GetSidePointsPair3D(points[i], points[i - 1], points[i + 1], width, axis, true);
            sidePoint1 = sidePointsCurrent.Item1;
            sidePoint2 = sidePointsCurrent.Item2;
            sidePoints.Add(sidePoint1);
            sidePoints.Add(sidePoint2);
        }

        int lastIndex = points.Count - 1;
        if (lastIndex > 0)
        {
            float width = widthCurve.Evaluate(1) * widthMultiplier;
            var sidePointsCurrent = GetSidePointsPair3D(points[lastIndex], points[lastIndex - 1], points[lastIndex], width, axis, true);
            sidePoint1 = sidePointsCurrent.Item1;
            sidePoint2 = sidePointsCurrent.Item2;
            sidePoints.Add(sidePoint1);
            sidePoints.Add(sidePoint2);
        }

        return sidePoints;
    }

    List<Vector3> AddSmoothPoints(List<Vector3> points, Axis axis, float smoothDistance, float smoothPower)
    {
        List<Vector3> resultPoints = new List<Vector3>();

        if (points.Count > 0)
            resultPoints.Add(points[0]);

        for (int i = 1; i < points.Count - 1; i++)
        {
            Vector2 pointA = GameMaths.Vector3ToVector2(points[i - 1], axis);
            Vector2 pointB = GameMaths.Vector3ToVector2(points[i], axis);
            Vector2 pointC = GameMaths.Vector3ToVector2(points[i + 1], axis);

            List<Vector2> newPointsV2 = GameMaths.SmoothAngle(pointA, pointB, pointC, smoothDistance, smoothPower, true);
            List<Vector3> newPointsV3 = GameMaths.Vector2ToVector3(newPointsV2, axis);
            resultPoints.AddRange(newPointsV3);
        }

        if (points.Count > 1)
            resultPoints.Add(points[points.Count - 1]);

        return resultPoints;
    }

    List<Vector3> ToWorldPosition(List<Vector3> positions)
    {
        List<Vector3> result = new List<Vector3>();
        for (int i = 0; i < positions.Count; i++)
        {
            result.Add(ToWorldPosition(positions[i]));
        }
        return result;
    }

    Vector3 ToWorldPosition(Vector3 position)
    {
        return position - this.transform.position;
    }

    Vector2[] CalculateUVsStretch(Vector3[] positions)
    {
        Vector2[] uvs = new Vector2[positions.Length];

        List<Vector2> UV_Points = new List<Vector2>() { Vector2.zero, Vector2.zero, Vector2.zero, Vector2.zero };
        int rowsCount = positions.Length / VERT_NUMBER + 1;
        float a0, a1;
        for (int i = 0; i < rowsCount - 1; i++)
        {
            a0 = i * 1f / (rowsCount - 1);
            a1 = (i + 1) * 1f / (rowsCount - 1);

            if (_textureDirection == LineRendererTextureDirection.Horizontal)
            {
                UV_Points[0] = new Vector2(0, a0);
                UV_Points[1] = new Vector2(1f, a0);
                UV_Points[2] = new Vector2(0, a1);
                UV_Points[3] = new Vector2(1f, a1);
            }
            else
            {
                UV_Points[0] = new Vector2(a0, 1);
                UV_Points[1] = new Vector2(a0, 0);
                UV_Points[2] = new Vector2(a1, 1);
                UV_Points[3] = new Vector2(a1, 0);
            }


            int v = i * VERT_NUMBER;
            uvs[v] = GetVerticlePoint(0, UV_Points);
            uvs[v + 1] = GetVerticlePoint(1, UV_Points);
            uvs[v + 2] = GetVerticlePoint(2, UV_Points);

            uvs[v + 3] = GetVerticlePoint(3, UV_Points);
            uvs[v + 4] = GetVerticlePoint(4, UV_Points);
            uvs[v + 5] = GetVerticlePoint(5, UV_Points);
        }

        if (_textureInverted)
        {
            uvs = InverseUV(uvs);
        }


        return uvs;
    }

    Vector2[] CalculateUVsByPoints(Vector3[] positions, float period)
    {
        Vector2[] uvs = new Vector2[positions.Length];

        List<Vector2> UV_Points = new List<Vector2>() { new Vector2(), new Vector2(), new Vector2(), new Vector2() };
        int rowsCount = positions.Length / VERT_NUMBER + 1;
        float a0, a1;
        for (int i = 0; i < rowsCount - 1; i++)
        {
            a0 = i * 1f / period;
            a1 = (i + 1) / period;

            if (_textureDirection == LineRendererTextureDirection.Horizontal)
            {
                UV_Points[0] = new Vector2(0, a0);
                UV_Points[1] = new Vector2(1f, a0);
                UV_Points[2] = new Vector2(0, a1);
                UV_Points[3] = new Vector2(1f, a1);
            }
            else
            {
                UV_Points[0] = new Vector2(a0, 1);
                UV_Points[1] = new Vector2(a0, 0);
                UV_Points[2] = new Vector2(a1, 1);
                UV_Points[3] = new Vector2(a1, 0);
            }

            int v = i * VERT_NUMBER;
            uvs[v] = GetVerticlePoint(0, UV_Points);
            uvs[v + 1] = GetVerticlePoint(1, UV_Points);
            uvs[v + 2] = GetVerticlePoint(2, UV_Points);

            uvs[v + 3] = GetVerticlePoint(3, UV_Points);
            uvs[v + 4] = GetVerticlePoint(4, UV_Points);
            uvs[v + 5] = GetVerticlePoint(5, UV_Points);
        }

        if (_textureInverted)
        {
            uvs = InverseUV(uvs);
        }
        return uvs;
    }

    Vector2[] CalculateUVsByDystance(Vector3[] positions, float period, List<Vector3> points)
    {
        Vector2[] uvs = new Vector2[positions.Length];

        List<Vector2> UV_Points = new List<Vector2>() { new Vector2(), new Vector2(), new Vector2(), new Vector2() };
        int rowsCount = positions.Length / VERT_NUMBER + 1;
        float a0, a1;
        float dist = 0;
        for (int i = 0; i < rowsCount - 1; i++)
        {
            a0 = dist / period;
            dist += Vector3.Distance(points[i], points[i + 1]);
            a1 = dist / period;

            if (_textureDirection == LineRendererTextureDirection.Horizontal)
            {
                UV_Points[0] = new Vector2(0, a0);
                UV_Points[1] = new Vector2(1f, a0);
                UV_Points[2] = new Vector2(0, a1);
                UV_Points[3] = new Vector2(1f, a1);
            }
            else
            {
                UV_Points[0] = new Vector2(a0, 1);
                UV_Points[1] = new Vector2(a0, 0);
                UV_Points[2] = new Vector2(a1, 1);
                UV_Points[3] = new Vector2(a1, 0);
            }

            int v = i * VERT_NUMBER;
            uvs[v] = GetVerticlePoint(0, UV_Points);
            uvs[v + 1] = GetVerticlePoint(1, UV_Points);
            uvs[v + 2] = GetVerticlePoint(2, UV_Points);

            uvs[v + 3] = GetVerticlePoint(3, UV_Points);
            uvs[v + 4] = GetVerticlePoint(4, UV_Points);
            uvs[v + 5] = GetVerticlePoint(5, UV_Points);
        }

        if (_textureInverted)
        {
            uvs = InverseUV(uvs);
        }
        return uvs;
    }

    private Vector2 GetVerticlePoint(int index, List<Vector2> points)
    {
        int newIndex = VERT_POSITIONS[index];
        return points[newIndex];
    }

    Vector2[] InverseUV(Vector2[] uvs)
    {
        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] *= -1;
        }
        return uvs;
    }

    private void OnDestroy()
    {
        Destroy(_mesh);
    }
}
