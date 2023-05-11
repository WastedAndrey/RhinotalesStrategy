using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class GridVizualizer : MonoBehaviour
{
    [SerializeField]
    private GameObject _prefabCellHighlight;
    [SerializeField] 
    private GameObject _prefabCell;
    [SerializeField]
    private Transform _effectsParent;
    [SerializeField]
    private Transform _cellsParent;

    [SerializeField]
    private List<GameObject> _highlightedCells = new List<GameObject>();
    [SerializeField]
    private List<GameObject> _highlightedCellsDisabled = new List<GameObject>();
    [SerializeField]
    private GameObject[,] _cells = new GameObject[0, 0];
    


    public void DrawGrid(MapSettings settings)
    {
        ClearCells();
        DrawCells(settings);
    }

    public void EnableHighlight(MapSettings settings, List<Vector2Int> cells, bool clearPrevious = true)
    {
        if (clearPrevious)
            DisableHighlights();

        for (int i = 0; i < cells.Count; i++)
        {
            GameObject highlightedCell;
            if (_highlightedCellsDisabled.Count > 0)
            {
                highlightedCell = _highlightedCellsDisabled[0];
                _highlightedCells.Add(highlightedCell);
                _highlightedCellsDisabled.RemoveAt(0);
            }
            else
            {
                highlightedCell = CreateCellHighlightObject();
            }

            Vector3 position = settings.GetCellPosition(cells[i]);
            highlightedCell.transform.position = position;
            highlightedCell.transform.localScale = Vector3.one * settings.CellSize;
            highlightedCell.SetActive(true);
        }
    }

    public void DisableHighlights()
    {
        for (int i = 0; i < _highlightedCells.Count; i++)
        {
            _highlightedCells[i].gameObject.SetActive(false);
            _highlightedCellsDisabled.Add(_highlightedCells[i]);
        }
        _highlightedCells.Clear();
    }

    private GameObject CreateCellHighlightObject()
    {
        GameObject newHighlightedCell = Instantiate(_prefabCellHighlight);
        newHighlightedCell.transform.SetParent(_effectsParent);
        _highlightedCells.Add(newHighlightedCell);
        return newHighlightedCell;
    }

    private void ClearCells()
    {
        for (int i = 0; i < _cells.GetLength(0); i++)
        {
            for (int j = 0; j < _cells.GetLength(1); j++)
            {
                if (Application.isPlaying)
                {
                    if (_cells[i, j] != null)
                        Destroy(_cells[i, j].gameObject);
                }
                else
                {
                    if (_cells[i, j] != null)
                        DestroyImmediate(_cells[i, j].gameObject);
                }
            }
        }
    }

    private void DrawCells(MapSettings settings)
    {
        _cells = new GameObject[settings.CellsCount.x, settings.CellsCount.y];

        for (int i = 0; i < _cells.GetLength(0); i++)
        {
            for (int j = 0; j < _cells.GetLength(1); j++)
            {
                Vector3 position = settings.GetCellPosition(new Vector2(i, j));

                _cells[i, j] = Instantiate(_prefabCell, position, Quaternion.identity);
                _cells[i, j].transform.SetParent(_cellsParent, true);
                _cells[i, j].transform.localScale = Vector3.one * settings.CellSize;
            }
        }
    }
}
