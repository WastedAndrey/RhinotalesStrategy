
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class PathfindVizualizer : MonoBehaviour
{
    [SerializeField]
    private EntityLink _entityLink;
    [SerializeField]
    private LineRendererNew _lineRendererNew;
    [SerializeField]
    private Transform _pathUnavailableIcon;

    private void Start()
    {
        Hide();
        _entityLink.Init();
    }

    public void SetPath(List<Vector3> positions)
    {
        _lineRendererNew.LoadPoints(positions);
        _lineRendererNew.Show();
        _pathUnavailableIcon.gameObject.SetActive(false);
    }

    public void SetPathUnavailable(Vector3 point)
    {
        _pathUnavailableIcon.transform.position = point;
        _lineRendererNew.Hide();
        _pathUnavailableIcon.gameObject.SetActive(true);

    }

    public void Hide()
    {
        _pathUnavailableIcon.gameObject.SetActive(false);
        _lineRendererNew.Hide();
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