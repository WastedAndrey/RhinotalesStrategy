
using System.Collections.Generic;
using UnityEngine;

public class GameMaths
{
    #region Segments, Rays & Lines intersection operations
    public static float CrossProduct(Vector2 directionA, Vector2 directionB)
    {
        return directionA.x * directionB.y - directionA.y * directionB.x;
    }

    public static bool TryGetLineIntersectionPoint(Vector2 offsetA, Vector2 directionA, Vector2 offsetB, Vector2 directionB, out Vector2 result)
    {
        float crossProduct = CrossProduct(directionA, directionB);

        if (Mathf.Approximately(crossProduct, 0f))
        {
            // Прямые параллельны
            result = default;
            return false;
        }

        float xMultiplierA = directionA.y / directionA.x;
        float xMultiplierB = directionB.y / directionB.x;
        float yOffsetA = offsetA.y - xMultiplierA * offsetA.x; // offset with x = 0
        float yOffsetB = offsetB.y - xMultiplierB * offsetB.x; // offset with x = 0
        float x = (yOffsetB - yOffsetA) / (xMultiplierA - xMultiplierB);
        float y = xMultiplierA * x + yOffsetA;

        /*
          m1 * x + c1 = m2 * x + c2
          m1 * x - m2 * x = c2 - c1
          (m1 - m2) * x = c2 - c1
          x =  (c2 - c1) / (m1 - m2)
          y = m1 * x + c1
        */

        result = new Vector2(x, y);
        return true;
    }

    public static bool TryGetLineIntersectionPoint(Vector2 directionA, Vector2 directionB, out Vector2 result)
    {
        float crossProduct = CrossProduct(directionA, directionB);
        if (crossProduct == 0)
        {
            result = default;
            return false;
        }
        float intersectionPointX = (directionA.y - directionB.y) / crossProduct;
        float intersectionPointY = (directionA.x - directionB.x) / crossProduct;
        result = new Vector2(intersectionPointX, intersectionPointY);
        return true;
    }

    public static bool TryGetSegmentIntersectionPoint(Vector2 segmentStartA, Vector2 segmentEndA, Vector2 segmentStartB, Vector2 segmentEndB, out Vector2 result)
    {
        Vector2 directionA = segmentEndA - segmentStartA;
        Vector2 directionB = segmentEndB - segmentStartB;
        float crossProduct = CrossProduct(directionA, directionB);
        if (crossProduct == 0)
        {
            result = default;
            return false;
        }
        float intersectionPositionOnSegmentA = ((segmentStartB.x - segmentStartA.x) * (segmentStartB.y - segmentEndB.y) - (segmentEndB.y - segmentStartA.y) * (segmentStartB.x - segmentEndB.x)) / crossProduct;
        float intersectionPositionOnSegmentB = ((segmentStartA.x - segmentEndA.x) * (segmentEndB.y - segmentStartA.y) - (segmentStartA.y - segmentEndA.y) * (segmentStartB.x - segmentStartA.x)) / crossProduct;
        if (intersectionPositionOnSegmentA < 0 || intersectionPositionOnSegmentA > 1 ||
            intersectionPositionOnSegmentB < 0 || intersectionPositionOnSegmentB > 1)
        {
            result = default;
            return false;
        }
        float intersectionPointX = segmentStartA.x + intersectionPositionOnSegmentA * (segmentEndA.x - segmentStartA.x);
        float intersectionPointY = segmentStartA.y + intersectionPositionOnSegmentA * (segmentEndA.y - segmentStartA.y);
        result = new Vector2(intersectionPointX, intersectionPointY);
        return true;
    }

    public static bool TryGetRayIntersectionPoint(Vector2 rayStartA, Vector2 rayDirectionA, Vector2 rayStartB, Vector2 rayDirectionB, out Vector2 result)
    {
        Vector2 directionA = rayDirectionA - rayStartA;
        Vector2 directionB = rayDirectionB - rayStartB;
        float crossProduct = CrossProduct(directionA, directionB);
        if (crossProduct == 0)
        {
            result = default;
            return false;
        }
        float intersectionPositionOnRayA = ((rayStartB.x - rayStartA.x) * (rayStartB.y - rayDirectionB.y) - (rayDirectionB.y - rayStartA.y) * (rayStartB.x - rayDirectionB.x)) / crossProduct;
        float intersectionPositionOnRayB = ((rayStartA.x - rayDirectionA.x) * (rayDirectionB.y - rayStartA.y) - (rayStartA.y - rayDirectionA.y) * (rayStartB.x - rayStartA.x)) / crossProduct;
        if (intersectionPositionOnRayA < 0 ||
            intersectionPositionOnRayB < 0)
        {
            result = default;
            return false;
        }
        float intersectionPointX = rayStartA.x + intersectionPositionOnRayA * (rayDirectionA.x - rayStartA.x);
        float intersectionPointY = rayStartA.y + intersectionPositionOnRayA * (rayDirectionA.y - rayStartA.y);
        result = new Vector2(intersectionPointX, intersectionPointY);
        return true;
    }
    #endregion

    #region Vector3 <-> Vector2 axis dependant convertations

    public static List<Vector2> Vector3ToVector2(List<Vector3> value, Axis axis)
    {
        List<Vector2> resultPoints = new List<Vector2>();
        switch (axis)
        {
            case Axis.X:
                for (int i = 0; i < value.Count; i++)
                    resultPoints.Add(Vector3ToVector2X(value[i]));
                return resultPoints;
            case Axis.Y:
                for (int i = 0; i < value.Count; i++)
                    resultPoints.Add(Vector3ToVector2Y(value[i]));
                return resultPoints;
            case Axis.Z:
                for (int i = 0; i < value.Count; i++)
                    resultPoints.Add(Vector3ToVector2Z(value[i]));
                return resultPoints;
            default:
                return resultPoints;
        }
    }
    public static Vector2 Vector3ToVector2(Vector3 value, Axis axis)
    {
        switch (axis)
        {
            case Axis.X:
                return Vector3ToVector2X(value);
            case Axis.Y:
                return Vector3ToVector2Y(value);
            case Axis.Z:
                return Vector3ToVector2Z(value);
            default:
                return default;
        }
    }
    private static Vector2 Vector3ToVector2X(Vector3 value)
    {
        return new Vector2(value.z, value.y);
    }
    private static Vector2 Vector3ToVector2Y(Vector3 value)
    {
        return new Vector2(value.x, value.z);
    }
    private static Vector2 Vector3ToVector2Z(Vector3 value)
    {
        return new Vector2(value.x, value.y);
    }
    public static List<Vector3> Vector2ToVector3(List<Vector2> value, Axis axis)
    {
        List<Vector3> resultPoints = new List<Vector3>();
        switch (axis)
        {
            case Axis.X:
                for (int i = 0; i < value.Count; i++)
                    resultPoints.Add(Vector2ToVector3X(value[i]));
                return resultPoints;
            case Axis.Y:
                for (int i = 0; i < value.Count; i++)
                    resultPoints.Add(Vector2ToVector3Y(value[i]));
                return resultPoints;
            case Axis.Z:
                for (int i = 0; i < value.Count; i++)
                    resultPoints.Add(Vector2ToVector3Z(value[i]));
                return resultPoints;
            default:
                return resultPoints;
        }
    }
    public static Vector3 Vector2ToVector3(Vector2 value, Axis axis)
    {
        switch (axis)
        {
            case Axis.X:
                return Vector2ToVector3X(value);
            case Axis.Y:
                return Vector2ToVector3Y(value);
            case Axis.Z:
                return Vector2ToVector3Z(value);
            default:
                return default;
        }
    }
    private static Vector3 Vector2ToVector3X(Vector2 value)
    {
        return new Vector3(0, value.y, value.x);
    }
    private static Vector3 Vector2ToVector3Y(Vector2 value)
    {
        return new Vector3(value.x, 0, value.y);
    }
    private static Vector3 Vector2ToVector3Z(Vector2 value)
    {
        return new Vector3(value.x, value.y, 0);
    }
    #endregion


    public static Vector2 GetNormal(Vector2 value)
    {
        return new Vector2(-value.y, value.x);
    }

    public static List<Vector2> SmoothAngle(Vector2 pointA, Vector2 pointB, Vector2 pointC, float smoothDistance, float smoothPower, bool addPositionToResult)
    {
        List<Vector2> result = new List<Vector2>();
        Vector2 directionAB = pointB - pointA;
        Vector2 directionBC = pointC - pointB;
        Vector2 directionABNormalized = directionAB.normalized;
        Vector2 directionBCNormalized = directionBC.normalized;
        float directionsDistance = Vector2.Distance(directionABNormalized, directionBCNormalized);
        Vector2 pointANormalized = -directionABNormalized * smoothDistance;
        Vector2 pointBNormalized = directionBCNormalized * smoothDistance;
        Vector2 normalToAB = GetNormal(directionABNormalized);
        Vector2 normalToBC = GetNormal(directionBCNormalized);

        Vector2 intersectionPoint;
        TryGetLineIntersectionPoint(pointANormalized, pointANormalized + normalToAB, pointBNormalized, pointBNormalized + normalToBC, out intersectionPoint);

       
        int pointsCount = (int)(directionsDistance * smoothPower);
        if (pointsCount >= 2)
        {
            result.Add(pointANormalized);

            if (pointsCount >= 3)
            {
                Vector2[] additionalPoints = GetIntermediatePoints(intersectionPoint, pointANormalized, pointBNormalized, pointsCount - 2);
                result.AddRange(additionalPoints);
                DebugFunctions.DrawPoint(pointB + intersectionPoint, 0.05f, Color.red, 2);
            }
            result.Add(pointBNormalized);
        }
        else
        {
            result.Add(Vector2.zero);
        }


        if (addPositionToResult)
        {
            for (int i = 0; i < result.Count; i++)
            {
                result[i] += pointB;
            }
        }

        return result;
    }

    public static Vector2[] GetIntermediatePoints(Vector2 center, Vector2 pointA, Vector2 pointB, int count)
    {
        Vector2[] intermediatePoints = new Vector2[count];

        // Вычисляем радиус и угол между точками A и B
        float radius = Vector2.Distance(center, pointA);
        float angle = Mathf.Atan2(pointA.y - center.y, pointA.x - center.x);

        // Вычисляем длину дуги между точками A и B
        float arcLength = GetArcLength(center, pointA, pointB, radius);

        // Вычисляем расстояние между промежуточными точками
        float stepSize = arcLength / (count + 1);

        // Вычисляем координаты промежуточных точек
        for (int i = 0; i < count; i++)
        {
            angle += stepSize / radius;
            float x = center.x + radius * Mathf.Cos(angle);
            float y = center.y + radius * Mathf.Sin(angle);
            intermediatePoints[i] = new Vector2(x, y);
        }

        return intermediatePoints;
    }

    private static float GetArcLength(Vector2 center, Vector2 pointA, Vector2 pointB, float radius)
    {
        float angleA = Mathf.Atan2(pointA.y - center.y, pointA.x - center.x);
        float angleB = Mathf.Atan2(pointB.y - center.y, pointB.x - center.x);

        float multiplier = 1;
        // Обрабатываем случай, когда точки A и B находятся по разные стороны окружности
        if (Mathf.Abs(angleB - angleA) > Mathf.PI)
        {
            angleA += Mathf.Sign(angleB - angleA) * 2 * Mathf.PI;
        }
        multiplier = Mathf.Sign(angleB - angleA);

        return Mathf.Abs(angleB - angleA) * radius * multiplier;
    }
}