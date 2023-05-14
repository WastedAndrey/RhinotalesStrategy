using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EditPanel : MonoBehaviour
{
    [SerializeField]
    private Toggle _toggleEditMode;
    [SerializeField]
    private TMP_Dropdown _dropdownPlayerTeam;
    [SerializeField]
    private TMP_Dropdown _dropdownUnitType;
    [SerializeField]
    private MapBuilder _mapBuilder;
    [SerializeField]
    private EntityLink _entityLink;

    LayerMask layerMask = LayersManager.LayerMaskMap;

    private void Awake()
    {
        _entityLink.Init();
    }

    private void OnEnable()
    {
        _toggleEditMode.isOn = _mapBuilder.IsEditing;
        _dropdownPlayerTeam.value = (int)_mapBuilder.PlayerTeam;
        _dropdownUnitType.value = (int)_mapBuilder.UnitType;
        _toggleEditMode.onValueChanged.AddListener(OnToggleEditModeChanged);
        _dropdownPlayerTeam.onValueChanged.AddListener(OnDropdownPlayerTeam);
        _dropdownUnitType.onValueChanged.AddListener(OnDropdownUnitType);
    }

    private void OnDisable()
    {
        _toggleEditMode.onValueChanged.RemoveListener(OnToggleEditModeChanged);
        _dropdownPlayerTeam.onValueChanged.RemoveListener(OnDropdownPlayerTeam);
        _dropdownUnitType.onValueChanged.RemoveListener(OnDropdownUnitType);
    }

    private void OnToggleEditModeChanged(bool value)
    {
        _mapBuilder.IsEditing = value;
        _entityLink.Entity.isLockInput = value;
        _entityLink.Entity.isLockUI = value;
    }
    private void OnDropdownPlayerTeam(int value)
    {
        _mapBuilder.PlayerTeam = (PlayerTeam)value;
    }
    private void OnDropdownUnitType(int value)
    {
        _mapBuilder.UnitType = (UnitType)value;
    }

    private void Update()
    {
        if (_toggleEditMode.isOn == false)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var hits = Physics.RaycastAll(ray, Mathf.Infinity, layerMask);

            for (int i = 0; i < hits.Length; i++)
            {
                MapCollider mapCollider = hits[i].collider.GetComponent<MapCollider>();
                if (mapCollider != null)
                {
                    mapCollider.RegisterHit(hits[i]);
                }
            }
        }
    }
}
