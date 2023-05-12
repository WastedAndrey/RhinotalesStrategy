
using Entitas;

public class DestroyUnitSystem : IExecuteSystem
{
    readonly Contexts _contexts;
    readonly IGroup<GameEntity> _entitiesUnits;

    public DestroyUnitSystem(Contexts contexts)
    {
        _contexts = contexts;
        _entitiesUnits = contexts.game.GetGroup(GameMatcher.RequestDestroyUnitEntity);
    }

    public void Execute()
    {
        var array = _entitiesUnits.GetEntities();
        foreach (var item in array)
        {

            item.battlefieldLink.BattlefieldEntity.battlefield.Cells[item.cellIndex.Index.x, item.cellIndex.Index.y].cell.InnerEntity = null;
            item.battlefieldLink.BattlefieldEntity.battlefield.CellsPassMap[item.cellIndex.Index.x, item.cellIndex.Index.y] = true;
            item.Destroy();
        }
    }
}