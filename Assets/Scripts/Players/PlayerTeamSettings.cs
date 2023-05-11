
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "PlayerTeamSettings", menuName = "ScriptableObjects/PlayerTeamSettings")]
[System.Serializable]
public class PlayerTeamSettings : ScriptableObject
{
    [SerializeField] 
    private List<ValuePair<PlayerTeam, Material>> _colorSettings = new List<ValuePair<PlayerTeam, Material>>();

    public Material GetTeamColor(PlayerTeam team)
    {// Dictionary is not serialized well out of box, so for easiar use List with value pair is here. A workaround can be made, but too lazy make it in test task
        foreach (var item in _colorSettings)
        {
            if (item.Key == team)
                return item.Value;
        }
        return null;
    }
}