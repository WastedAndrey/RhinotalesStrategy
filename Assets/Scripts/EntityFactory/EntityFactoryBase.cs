
using UnityEngine;

public abstract class EntityFactoryBase : ScriptableObject
{
    public GameEntity CreateEntity(GameObject gameObject)
    {
        var contexts = Contexts.sharedInstance;
        var entity = contexts.game.CreateEntity();
        return CreateEntityInternal(gameObject, entity);
    }

    protected abstract GameEntity CreateEntityInternal(GameObject gameObject, GameEntity entity);
}