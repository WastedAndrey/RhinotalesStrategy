using UnityEngine;

public enum UnitType
{ 
    UnitWeak,
    UnitStrong,
    Wall
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
    protected PlayerTeam _team;
    [SerializeField]
    protected Vector2Int _cellIndex;
    [SerializeField]
    protected int _movementSpeed;
    [SerializeField]
    protected bool _isSelected;
    [SerializeField]
    protected bool _isUnitTurn;

    public virtual UnitType UnitType { get => _unitType; set => _unitType = value; }
    public virtual PlayerTeam Team { get => _team; set => _team = value; }
    public virtual Vector2Int CellIndex { get => _cellIndex; set => _cellIndex = value; }
    public virtual int MovementSpeed { get => _movementSpeed; set => _movementSpeed = value; }
    public virtual bool IsSelected { get => _isSelected; set => _isSelected = value; }
    public virtual bool IsUnitTurn { get => _isUnitTurn; set => _isUnitTurn = value; }
    public Map Map { get; private set; }


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
    }
    protected virtual void OnDestroyInternal() { }

    public void Remove()
    {
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
}