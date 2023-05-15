using System;
using UnityEngine;

public enum UnitType
{
    Wall,
    UnitWeak,
    UnitStrong,
}

public enum UnitCombatType
{
    NonCombat,
    Combat
}

public class UnitBase : MonoBehaviour
{
    [Header("Component links")]
    [SerializeField]
    private UnitCollider _collider;
    [SerializeField]
    private EntityLink _link;
    [Header("Data")]
    [SerializeField]
    protected UnitType _unitType;
    [SerializeField]
    protected UnitCombatType _unitCombatType;
    [SerializeField]
    protected PlayerTeam _team;
    [SerializeField]
    protected Vector2Int _cellIndex;
    [SerializeField]
    protected int _movementSpeed;
    [SerializeField]
    protected bool _isSelected;
    [SerializeField]
    protected bool _isUnitTurn;

    [SerializeField]
    protected float _showPause = 0; // pause after creation of unit before it is shown to player

    public virtual UnitType UnitType { get => _unitType; set => _unitType = value; }
    public virtual UnitCombatType UnitCombatType { get => _unitCombatType; set => _unitCombatType = value; }
    public virtual PlayerTeam Team { get => _team; set => _team = value; }
    public virtual Vector2Int CellIndex 
    { 
        get => _cellIndex; 
        set
        {
            if (_cellIndex == value)
                return;

            var previousIndex = _cellIndex;
            _cellIndex = value;
            CellChanged?.Invoke(this, previousIndex, _cellIndex); 
        } 
    }
    public virtual int MovementSpeed { get => _movementSpeed; set => _movementSpeed = value; }
    public virtual bool IsSelected { get => _isSelected; set => _isSelected = value; }
    public virtual bool IsUnitTurn { get => _isUnitTurn; set => _isUnitTurn = value; }
    public virtual float ShowPause { get => _showPause; set => _showPause = value; }
    public Map Map { get; private set; }
  

    public Action<UnitBase, Vector2Int, Vector2Int> CellChanged;
    public Action<UnitBase> Destroyed;

    public bool IsTurnAvailable = false;

    public void Init(Map map)
    {
        Map = map;
        _link.Init();

        InitInternal();
    }

    protected virtual void InitInternal() { }


    public void SetBaseParams(Vector2Int cellIndex, PlayerTeam team)
    {
        CellIndex = cellIndex;
        Team = team;
        SetBaseParamsInternal(cellIndex, team);
    }
    protected virtual void SetBaseParamsInternal(Vector2Int cellIndex, PlayerTeam team) { }
    private void Awake() 
    {
        AwakeInternal();
    }

    protected virtual void AwakeInternal() { }


    private void OnEnable()
    {
        if (_collider != null)
            _collider.WasClicked += OnClick;
        OnEnableInternal();
    }

    protected virtual void OnEnableInternal() { }


    private void OnDisable()
    {
        if (_collider != null)
            _collider.WasClicked -= OnClick;
        OnDisableInternal();
    }
    protected virtual void OnDisableInternal() { }

    private void OnDestroy()
    {
        OnDestroyInternal();
        Destroyed?.Invoke(this);
    }
    protected virtual void OnDestroyInternal() { }

    public void Remove()
    {
        if (_link.Entity != null)
            _link.Entity.isRequestDestroyUnitEntity = true;

        RemoveInternal();

        if (Application.isPlaying)
            Destroy(this.gameObject);
        else
            DestroyImmediate(this.gameObject);
    }

    protected virtual void RemoveInternal() { }

    public void OnClick(RaycastHit hit)
    {
        OnClickInternal(hit);
    }

    protected virtual void OnClickInternal(RaycastHit hit) { }

    private void Update()
    {
        UpdateInternal();
    }
    protected virtual void UpdateInternal() { }
}