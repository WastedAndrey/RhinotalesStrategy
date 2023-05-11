using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class PathfindResultComponent : IComponent
{
    public Vector3 StartPosition;
    public Vector3 TargetPosition;
    public List<Vector3> Path;
}