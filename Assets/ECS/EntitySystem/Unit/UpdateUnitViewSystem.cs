
using Entitas;

public class UpdateUnitViewSystem : IExecuteSystem
{
    readonly Contexts _contexts;
    readonly IGroup<GameEntity> _entitiesUnits;

    public UpdateUnitViewSystem(Contexts contexts)
    {
        _contexts = contexts;
        _entitiesUnits = contexts.game.GetGroup(GameMatcher.RequestUpdateUnitView);
    }

    public void Execute()
    {
        var array = _entitiesUnits.GetEntities();
        foreach (var item in array)
        {
            var script = item.scriptLink.Script.GetComponent<UnitBase>();
            script.IsUnitTurn = item.isUnitTurn;
            script.IsSelected = item.isSelected;
            script.CellIndex = item.cellIndex.Index;
            item.isRequestUpdateUnitView = false;
        }
    }
}