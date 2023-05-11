
using Entitas;

public class DestoyEntitySystem : IExecuteSystem
{
    readonly IGroup<GameEntity> _entities;

    public DestoyEntitySystem(Contexts contexts)
    {
        _entities = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Clicked, GameMatcher.UnitTurn));
    }
    public void Execute()
    {
        var entitiesArray = _entities.GetEntities();
        foreach (var item in entitiesArray)
        {
            item.Destroy();
        }
    }
}
