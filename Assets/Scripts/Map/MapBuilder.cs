using Sirenix.OdinInspector;
using UnityEngine;


[ExecuteInEditMode]
public class MapBuilder : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField]
    private UnitBase _prefabUnitWeak;
    [SerializeField]
    private UnitBase _prefabUnitStrong;
    [SerializeField]
    private UnitBase _prefabWall;
    [Header("Components")]
    [SerializeField]
    private Map _map;
    [SerializeField]
    private MapCollider _mapCollider;
    [Header("Fields")]
    [SerializeField]
    private bool _isEditing = false;
    [SerializeField]
    private UnitType _unitType;
    [SerializeField]
    private PlayerTeam _playerTeam;

    public bool IsEditing { get => _isEditing; set => _isEditing = value; }
    public UnitType UnitType { get => _unitType; set => _unitType = value; }
    public PlayerTeam PlayerTeam { get => _playerTeam; set => _playerTeam = value; }
    public MapCollider MapCollider { get => _mapCollider; }

    private void OnEnable()
    {
        if (MapCollider != null)
            MapCollider.WasClicked += OnClick;
    }

    private void OnDisable()
    {
        if (MapCollider != null)
            MapCollider.WasClicked -= OnClick;
    }

    private void OnClick(RaycastHit hit)
    {
        Vector2Int cellIndex = _map.Settings.GetCellIndex(hit.point);
        if (_map.IsCellEmpty(cellIndex))
            _map.CreateUnit(GetPrefab(UnitType), cellIndex, PlayerTeam);
        else
            _map.DestroyUnit(cellIndex);
    }

    private UnitBase GetPrefab(UnitType unitType)
    {
        switch (unitType)
        {
            case UnitType.UnitWeak:
                return _prefabUnitWeak;
            case UnitType.UnitStrong:
                return _prefabUnitStrong;
            case UnitType.Wall:
                return _prefabWall;
            default:
                return null;
        }
    }
}
