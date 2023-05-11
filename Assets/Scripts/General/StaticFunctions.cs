
using PathFindLib.PathFindA;
using System;
using UnityEngine;

public class StaticFunctions
{
    public static T GetNextEnumValue<T>(T current) where T : Enum
    {
        // Получаем список всех значений перечисления
        var values = Enum.GetValues(typeof(T));

        // Получаем индекс текущего значения
        int index = Array.IndexOf(values, current);

        // Вычисляем индекс следующего значения
        int nextIndex = (index + 1) % values.Length;

        // Возвращаем следующее значение
        return (T)values.GetValue(nextIndex);
    }
}