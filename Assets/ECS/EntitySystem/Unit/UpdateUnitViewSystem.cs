
using Entitas;

public class UpdateUnitViewSystem : IExecuteSystem
{
    readonly Contexts _contexts;
    readonly IGroup<GameEntity> _entitiesUnits;

    public UpdateUnitViewSystem(Contexts contexts)
    {
        _contexts = contexts;
        _entitiesUnits = contexts.game.GetGroup(GameMatcher.UnitType);
    }

    public void Execute()
    { }
}