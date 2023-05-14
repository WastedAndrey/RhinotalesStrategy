
using UnityEngine;

public class DebugFunctions
{
    public static void DrawPoint(Vector2 position, float size, Color color, float duration = 0f, bool depthTest = true)
    {
        float halfSize = size / 2f;

        Vector2 pointA = new Vector2(position.x - halfSize, position.y - halfSize);
        Vector2 pointB = new Vector2(position.x - halfSize, position.y + halfSize);
        Vector2 pointC = new Vector2(position.x + halfSize, position.y + halfSize);
        Vector2 pointD = new Vector2(position.x + halfSize, position.y - halfSize);

        Debug.DrawLine(pointA, pointB, color, duration, depthTest);
        Debug.DrawLine(pointB, pointC, color, duration, depthTest);
        Debug.DrawLine(pointC, pointD, color, duration, depthTest);
        Debug.DrawLine(pointD, pointA, color, duration, depthTest);
    }
}