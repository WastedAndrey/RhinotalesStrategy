
using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class UnitInitSystem : IExecuteSystem
{
    readonly Contexts _contexts;
    readonly IGroup<GameEntity> _entitiesUnits;

    public UnitInitSystem(Contexts contexts)
    {
        _contexts = contexts;
        _entitiesUnits = contexts.game.GetGroup(GameMatcher.UnitRequestInit);
    }

    public void Execute()
    {
        var unitsArray = _entitiesUnits.GetEntities();
        foreach (var item in unitsArray)
        {
            Vector2Int cellIndex = item.cellIndex.Index;

            item.battlefieldLink.BattlefieldEntity.battlefield.Cells[cellIndex.x, cellIndex.y].cell.InnerEntity = item;
            item.battlefieldLink.BattlefieldEntity.battlefield.CellsPassMap[cellIndex.x, cellIndex.y] = false;
            if (item.playerTeam.Team == item.battlefieldLink.BattlefieldEntity.battlefield.CurrentTurn)
            {
                item.isUnitTurn = true;
                item.scriptLink.Script.GetComponent<UnitBase>().IsUnitTurn = true;
            }

            item.isUnitRequestInit = false;
        }
    }
}