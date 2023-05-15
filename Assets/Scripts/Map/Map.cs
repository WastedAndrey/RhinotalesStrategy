using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;



public class Map : MonoBehaviour
{
    [Header("Component links")]
    [SerializeField]
    private GridVizualizer _vizualizer;
    [SerializeField]
    private EntityLink _link;
    [SerializeField]
    private Transform _unitsParent;
    [Header("Data")]
    [SerializeField]
    private MapSettings _settings;
    [SerializeField]
    private MapData _data;
    [SerializeField]
    private List<UnitBase> _units = new List<UnitBase>();

    public MapSettings Settings { get => _settings; }
    public GameEntity Entity { get => _link.Entity; }

    private void Start()
    {
        _link.Init();
        for (int i = 0; i < _units.Count; i++)
        {
            _units[i].Init(this);
            SubscribeToUnitEvents(_units[i]);
        }
    }


    [Button]
    public void CreateMap()
    {
        DestroyAllUnits();

        _data.CreateCells(_settings.CellsCount);
        _vizualizer.DrawGrid(_settings);
    }


    public bool CreateUnit(UnitBase prefab, Vector2Int cellIndex, PlayerTeam team)
    {
        if (IsCellEmpty(cellIndex) == false)
            return false;

        Vector3 position = Settings.GetCellPosition(cellIndex);
        UnitBase newUnit = null;
        if (Application.isPlaying)
            newUnit = Instantiate(prefab, position, Quaternion.identity);
        else
        {
#if UNITY_EDITOR
            newUnit = (UnitBase)UnityEditor.PrefabUtility.InstantiatePrefab(prefab);
            newUnit.transform.position = position;
            newUnit.ShowPause = (float)cellIndex.y / (float)_settings.CellsCount.y * 2f + 1;
#endif
        }

        newUnit.SetBaseParams(cellIndex, team);
        newUnit.transform.SetParent(_unitsParent);
        _units.Add(newUnit);
        _data.GetCell(cellIndex).Unit = newUnit;

        if (Application.isPlaying)
        {
            newUnit.Init(this);
            SubscribeToUnitEvents(newUnit);
        }

        return true;
    }
    public bool DestroyUnit(Vector2Int cellIndex)
    {
        if (IsCellEmpty(cellIndex))
            return false;

        if (_units.Contains(_data.GetCell(cellIndex).Unit))
            _units.Remove(_data.GetCell(cellIndex).Unit);

        _data.GetCell(cellIndex).Unit.Remove();

        return true;
    }

    public void DestroyAllUnits()
    {
        List<UnitBase> unitsList = new List<UnitBase>(_units);
        for (int i = 0; i < unitsList.Count; i++)
        {
            if (unitsList[i] != null)
                DestroyUnit(unitsList[i].CellIndex);
        }
    }

    public bool IsCellEmpty(Vector2Int cellIndex)
    {
        return _data.GetCell(cellIndex).Unit == null;
    }

    private void SubscribeToUnitEvents(UnitBase unit)
    {
        unit.CellChanged += OnUnitCellChanged;
        unit.Destroyed += OnUnitDestroyed;
    }
    private void UnsubscribeFromUnitEvents(UnitBase unit)
    {
        unit.CellChanged -= OnUnitCellChanged;
        unit.Destroyed -= OnUnitDestroyed;
    }
    private void OnUnitCellChanged(UnitBase unit, Vector2Int previousCellIndex, Vector2Int newCellIndex)
    {
        _data.GetCell(previousCellIndex).Unit = null;
        _data.GetCell(newCellIndex).Unit = unit;
    }

    private void OnUnitDestroyed(UnitBase unit)
    {
        UnsubscribeFromUnitEvents(unit);
    }
}
