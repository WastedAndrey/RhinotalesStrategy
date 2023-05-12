using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class GridVizualizer : MonoBehaviour
{
    [SerializeField] 
    private GameObject _prefabCell;
    [SerializeField]
    private Transform _effectsParent;
    [SerializeField]
    private Transform _cellsParent;

    [SerializeField]
    private GameObject[,] _cells = new GameObject[0, 0];
    


    public void DrawGrid(MapSettings settings)
    {
        ClearCells();
        DrawCells(settings);
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
