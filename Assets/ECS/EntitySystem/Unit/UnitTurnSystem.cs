
using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class UnitTurnSystem : IExecuteSystem
{
    readonly Contexts _contexts;
    readonly IGroup<GameEntity> _entitiesUnits;

    public UnitTurnSystem(Contexts contexts)
    {
        _contexts = contexts;
        _entitiesUnits = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.UnitTurn, GameMatcher.Selected));
    }

    public void Execute()
    {
        foreach (var item in _entitiesUnits)
        {
            MapSettings mapSettings = item.battlefieldLink.BattlefieldEntity.battlefield.MapSettings;
            Vector3 clickPos = item.battlefieldLink.BattlefieldEntity.clicked.Position;
            Vector2Int currentCellIndex = item.cellIndex.Index;
            Vector2Int targetCellIndex = mapSettings.GetCellIndex(clickPos);
            List<Vector2Int> path = new List<Vector2Int>() { currentCellIndex , targetCellIndex };

            item.AddMovementAnimation(0, path, mapSettings);
            item.isUnitTurn = false;
            item.isRequestUpdateUnitView = true;
        }
    }
}