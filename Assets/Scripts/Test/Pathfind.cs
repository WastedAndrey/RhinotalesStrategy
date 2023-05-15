
using System.Collections.Generic;
using UnityEngine;

public class Pathfind
{
    public static List<Vector2Int> GetPath(Vector2Int startPosition, Vector2Int finishPosition)
    {
        List<Vector2Int> path = new List<Vector2Int>();
        Vector2Int currentPosition = startPosition;
        Vector2Int targetPosition = finishPosition;
        path.Add(currentPosition);
        while (currentPosition.x != targetPosition.x || currentPosition.y != targetPosition.y)
        {
            if (currentPosition.x != targetPosition.x)
            {
                currentPosition.x += (int)Mathf.Sign(targetPosition.x - currentPosition.x);
                path.Add(currentPosition);
                continue;
            }

            if (currentPosition.y != targetPosition.y)
            {
                currentPosition.y += (int)Mathf.Sign(targetPosition.y - currentPosition.y);
                path.Add(currentPosition);
                continue;
            }
        }

        return path;
    }
}