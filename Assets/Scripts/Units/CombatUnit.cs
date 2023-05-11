using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class CombatUnit : UnitBase
{
    [SerializeField]
    private PlayerTeamSettings _teamSettings;
    [SerializeField]
    private List<Renderer> _renderers = new List<Renderer>();
    [SerializeField]
    private GameObject _selectedEffect;
    [SerializeField]
    private GameObject _unitTurnEffect;

    public override PlayerTeam Team
    {
        get => _team;

        set
        {
            ChangeMaterial(_teamSettings.GetTeamColor(value));
            _team = value;
        }
    }

    public override bool IsSelected 
    { 
        get => base.IsSelected; 
        set 
        {
            _selectedEffect.SetActive(value);
            base.IsSelected = value; 
        } 
    }

    public override bool IsUnitTurn
    {
        get => base.IsUnitTurn;
        set
        {
            _unitTurnEffect.SetActive(value);
            base.IsUnitTurn = value;
        }
    }

    private void ChangeMaterial(Material material)
    {
        for (int i = 0; i < _renderers.Count; i++)
        {
            _renderers[i].sharedMaterial = material;
        }
    }
}
