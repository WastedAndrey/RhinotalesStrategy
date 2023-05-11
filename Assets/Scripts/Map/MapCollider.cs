using System;
using UnityEngine;

public class MapCollider : MonoBehaviour, IClickableObject
{
    [SerializeField]
    private BoxCollider _boxCollider;

    public Action<RaycastHit> WasClicked { get; set; }

    public Vector3 Scale
    {
        get
        {
            Vector3 scale = transform.lossyScale;
            if (_boxCollider != null)
                scale.Scale(_boxCollider.size);
            return scale;
        }
    }

    public Vector3 Position
    {
        get
        {
            Vector3 position = Vector3.zero;
            if (_boxCollider != null)
            {
                position = _boxCollider.center;
                position.Scale(transform.lossyScale);
            }
            return position;
        }
    }

    public bool RegisterHit(RaycastHit hit)
    {
        WasClicked?.Invoke(hit);
        return true;
    }
}
