
using System;
using UnityEngine;

public interface IClickableObject
{
    /// <summary>
    /// Register click for some workaround after.
    /// </summary>
    /// <param name="hit"></param>
    /// <returns>True = click was successfull. So clicks on objects behind are blocked.</returns>
    bool RegisterHit(RaycastHit hit);

    Action<RaycastHit> WasClicked { get; set; }
}