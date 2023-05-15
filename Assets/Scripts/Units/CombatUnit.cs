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
    [SerializeField]
    private GameObject _mainModel;
    [SerializeField]
    private ShatterAnimations _shatterAnimations;

    public override PlayerTeam Team
    {
        get => _team;

        set
        {
            SetMaterial(_teamSettings.GetTeamColor(value));
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

    private SimpleTimer _showTimer;

    private void SetMaterial(Material material)
    {
        for (int i = 0; i < _renderers.Count; i++)
        {
            _renderers[i].sharedMaterial = material;
        }
        _shatterAnimations.SetMaterial(material);
    }

    protected override void AwakeInternal()
    {
        this.gameObject.SetActive(false);
        _mainModel.gameObject.SetActive(false);
        if (_showPause > 0)
        {
            _showTimer = new SimpleTimer();
            _showTimer.Elapsed += RunStartAnimations;
            _showTimer.Start(_showPause);
        }
        else
            RunStartAnimations();

        base.AwakeInternal();
    }

    private void RunStartAnimations()
    {
        this.gameObject.SetActive(true);
        _shatterAnimations.Init();
        _shatterAnimations.CreationAnimationFinished += OnCreationAnimationFinished;
        _shatterAnimations.StartCreationAnimation();
    }

    protected override void OnDestroyInternal()
    {
        _shatterAnimations.CreationAnimationFinished -= OnCreationAnimationFinished;
        base.OnDestroyInternal();
    }

    private void OnCreationAnimationFinished()
    {
        _mainModel.gameObject.SetActive(true);
    }
}
