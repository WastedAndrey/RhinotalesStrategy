
using UnityEngine;

[CreateAssetMenu(fileName = "FactoryPathfindVizualizer", menuName = "ScriptableObjects/Factories/FactoryPathfindVizualizer")]
public class EntityFactoryPathfindVizualizer : EntityFactoryBase
{
    protected override GameEntity CreateEntityInternal(GameObject gameObject, GameEntity entity)
    {
        EntityLink link = gameObject.GetComponent<EntityLink>();
        entity.AddScriptLink(link);
        entity.isPathfindVizualizer = true;
        return entity;
    }
}