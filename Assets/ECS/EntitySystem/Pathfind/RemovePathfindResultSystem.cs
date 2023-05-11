
using Entitas;

public class RemovePathfindResultSystem : IExecuteSystem
{
    private Contexts _contexts;
    readonly IGroup<GameEntity> _entities;

    public RemovePathfindResultSystem(Contexts contexts)
    {
        _contexts = contexts;
        _entities = contexts.game.GetGroup(GameMatcher.PathfindResult);
    }

    public void Execute()
    {
        GameEntity[] entitiesArray = _entities.GetEntities();
        foreach (var item in entitiesArray)
        {
            item.RemovePathfindResult();
        }
    }
}