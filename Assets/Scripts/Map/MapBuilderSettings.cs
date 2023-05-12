using UnityEngine;

[CreateAssetMenu(fileName = "MapBuilderSettings", menuName = "ScriptableObjects/MapBuilderSettings")]
[System.Serializable]
public class MapBuilderSettings : ScriptableObject
{
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
}