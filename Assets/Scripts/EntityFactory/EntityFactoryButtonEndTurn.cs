
using UnityEngine;

[CreateAssetMenu(fileName = "FactoryButtonEndTurn", menuName = "ScriptableObjects/Factories/FactoryButtonEndTurn")]
public class EntityFactoryButtonEndTurn : EntityFactoryBase
{
    protected override GameEntity CreateEntityInternal(GameObject gameObject, GameEntity entity)
    {
        EntityLink link = gameObject.GetComponent<EntityLink>();
        entity.AddScriptLink(link);
        entity.isButtonRequestEndTurn = true;
        return entity;
    }
}