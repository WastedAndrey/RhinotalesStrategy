using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapSettings
{
    [SerializeField]
    [Min(1)]
    public Vector2Int CellsCount = new Vector2Int(5, 5);
    [SerializeField]
    [Min(0.1f)]
    public float CellSize = 1;

    public Vector2Int GetCellIndex(Vector3 position)
    {
        Vector2 positionV2 = new Vector2(position.x, position.z);
        positionV2 /= CellSize;
        positionV2 += (Vector2)CellsCount * 0.5f;
       
        positionV2.x = Mathf.Clamp((int)positionV2.x, 0, CellsCount.x - 1);
        positionV2.y = Mathf.Clamp((int)positionV2.y, 0, CellsCount.y - 1);
        return Vector2Int.RoundToInt(positionV2);
    }

    public List<Vector3> GetCellPositions(List<Vector2Int> cellIndex)
    {
        List<Vector3> result = new List<Vector3>();
        for (int i = 0; i < cellIndex.Count; i++)
        {
            result.Add(GetCellPosition(cellIndex[i]));
        }
        return result;
    }

    public List<Vector3> GetCellPositions(List<Vector2> cellIndex)
    {
        List<Vector3> result = new List<Vector3>();
        for (int i = 0; i < cellIndex.Count; i++)
        {
            result.Add(GetCellPosition(cellIndex[i]));
        }
        return result;
    }

    public Vector3 GetCellPosition(Vector2 cellIndex)
    {
        Vector2 position = cellIndex * CellSize - (Vector2)CellsCount * CellSize * 0.5f + Vector2.one * CellSize * 0.5f;

        return new Vector3(position.x, 0, position.y);
    }

    public Vector2Int GetRandomCell()
    {
        return new Vector2Int(Random.Range(0, CellsCount.x), Random.Range(0, CellsCount.y));
    }
}