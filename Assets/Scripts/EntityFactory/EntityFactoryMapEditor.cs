
using UnityEngine;

[CreateAssetMenu(fileName = "FactoryButtonMapEditor", menuName = "ScriptableObjects/Factories/FactoryButtonMapEditor")]
public class EntityFactoryMapEditor : EntityFactoryBase
{
    protected override GameEntity CreateEntityInternal(GameObject gameObject, GameEntity entity)
    {
        EntityLink link = gameObject.GetComponent<EntityLink>();
        entity.AddScriptLink(link);
        return entity;
    }
}