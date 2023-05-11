
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class PathfindVizualizer : MonoBehaviour
{
    [SerializeField]
    private EntityLink _entityLink;
    [SerializeField]
    private LineRenderer _lineRenderer;
    [SerializeField]
    private Transform _pathUnavailableIcon;

    private void Awake()
    {
        Hide();
        _entityLink.Init();
    }

    public void SetPath(List<Vector3> positions)
    {
        _lineRenderer.positionCount = positions.Count;
        _lineRenderer.SetPositions(positions.ToArray());
        _lineRenderer.enabled = true;
        _pathUnavailableIcon.gameObject.SetActive(false);
    }

    public void SetPathUnavailable(Vector3 point)
    {
        _pathUnavailableIcon.transform.position = point;
        _lineRenderer.enabled = false;
        _pathUnavailableIcon.gameObject.SetActive(true);

    }

    public void Hide()
    {
        _pathUnavailableIcon.gameObject.SetActive(false);
        _lineRenderer.enabled = false;
    }

    [Button]
    private void Test1()
    {
        SetPath(new List<Vector3>()
        {
            new Vector3(1,0,0),
             new Vector3(2,0,0),
              new Vector3(2,0,1),
               new Vector3(2,0,2),
        });
    }

    [Button]
    private void Test2()
    {
        SetPathUnavailable(new Vector3(2, 0, 1));
    }

    [Button]
    private void Test3()
    {
        Hide();
    }
}