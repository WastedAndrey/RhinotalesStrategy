

using Entitas;
using PathFindLib.PathFindA;
using System.Collections.Generic;
using UnityEngine;

public class MovementOrderSystem : IExecuteSystem
{
    readonly Contexts _contexts;
    readonly IGroup<GameEntity> _entitiesUnits;
    readonly IGroup<GameEntity> _entitiesCells;
    readonly IGroup<GameEntity> _entitiesLockInput;

    public MovementOrderSystem(Contexts contexts)
    {
        _contexts = contexts;
        _entitiesUnits = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.UnitTurn, GameMatcher.Selected));
        _entitiesCells = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Cell, GameMatcher.Clicked));
        _entitiesLockInput = contexts.game.GetGroup(GameMatcher.LockUI);
    }

    public void Execute()
    {
        if (_entitiesLockInput.count > 0)
            return;

        var entitiesUnitsArray = _entitiesUnits.GetEntities();
        var cellEntity = _entitiesCells.GetSingleEntity();
        if (cellEntity == null)
            return;

        foreach (var unitEntity in entitiesUnitsArray)
        {
            BattlefieldComponent battlefield = unitEntity.battlefieldLink.BattlefieldEntity.battlefield;
            battlefield.Cells[unitEntity.cellIndex.Index.x, unitEntity.cellIndex.Index.y].cell.InnerEntity = null;
            battlefield.CellsPassMap[unitEntity.cellIndex.Index.x, unitEntity.cellIndex.Index.y] = true;

            var path = PathFindA.PathFind(unitEntity.cellIndex.Index, cellEntity.cellIndex.Index, battlefield.CellsPassMap);
            path.Reverse();
            path.Add(cellEntity.cellIndex.Index);
            unitEntity.cellIndex.Index = cellEntity.cellIndex.Index;
            cellEntity.cell.InnerEntity = unitEntity;
            battlefield.CellsPassMap[unitEntity.cellIndex.Index.x, unitEntity.cellIndex.Index.y] = false;
            unitEntity.isUnitTurn = false;
            unitEntity.isSelected = false;
            unitEntity.isRequestUpdateUnitView = true;
            unitEntity.isLockUI = true;
            unitEntity.isLockInput = true;
            unitEntity.AddMovementAnimation(0, path, unitEntity.battlefieldLink.BattlefieldEntity.battlefield.MapSettings);
        }
    }
}