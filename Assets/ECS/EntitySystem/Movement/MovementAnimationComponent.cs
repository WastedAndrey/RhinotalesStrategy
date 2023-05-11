
using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class MovementAnimationComponent : IComponent
{
    public float Progress = 0;
    public List<Vector2Int> Path = new List<Vector2Int>();
    public MapSettings MapSettings;
}