using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCollider : MonoBehaviour, IClickableObject
{
    public Action<RaycastHit> WasClicked { get; set; }

    public bool RegisterHit(RaycastHit hit)
    {
        WasClicked?.Invoke(hit);
        return true;
    }
}
