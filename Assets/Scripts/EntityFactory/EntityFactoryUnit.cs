
using UnityEngine;

[CreateAssetMenu(fileName = "FactoryUnit", menuName = "ScriptableObjects/Factories/FactoryUnit")]
public class EntityFactoryUnit : EntityFactoryBase
{
    protected override GameEntity CreateEntityInternal(GameObject gameObject, GameEntity entity)
    {
        UnitBase unit = gameObject.GetComponent<UnitBase>();
        EntityLink link = gameObject.GetComponent<EntityLink>();
        entity.AddScriptLink(link);
        entity.AddCellIndex(unit.CellIndex);
        entity.AddUnitCombatType(unit.UnitCombatType);
        entity.AddPlayerTeam(unit.Team);
        entity.AddBattlefieldLink(unit.Map.Entity);
        entity.isUnitRequestInit = true;
        return entity;
    }
}