
using UnityEngine;

public enum StrategyObjectType
{
    PlayerOne,
    PlayerTwo,
    Wall
}

[System.Serializable]
public abstract class StrategyObject
{

}

[System.Serializable]
public class CellInfo
{
    public Vector2Int Index;
    public UnitBase Unit;
}