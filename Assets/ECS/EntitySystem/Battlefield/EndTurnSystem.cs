using Entitas;
using UnityEngine;

public class EndTurnSystem : IExecuteSystem
{
    private Contexts _contexts;
    readonly IGroup<GameEntity> _entitiesEndTurn;
    readonly IGroup<GameEntity> _entitiesBattlefield;
    readonly IGroup<GameEntity> _entitiesUnits;
    public EndTurnSystem(Contexts contexts)
    {
        _contexts = contexts;
        _entitiesEndTurn = contexts.game.GetGroup(GameMatcher.EndTurn);
        _entitiesBattlefield = contexts.game.GetGroup(GameMatcher.Battlefield);
        _entitiesUnits = contexts.game.GetGroup(GameMatcher.PlayerTeam);
    }

    public void Execute()
    {
        var entitiesEndTurnArray = _entitiesEndTurn.GetEntities();
        foreach (var item in entitiesEndTurnArray)
        {
            EndTurn();
            return;
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            EndTurn();
        }
    }

    private void EndTurn()
    {
        foreach (var item in _entitiesBattlefield)
        {
            item.battlefield.CurrentTurn = StaticFunctions.GetNextEnumValue<PlayerTeam>(item.battlefield.CurrentTurn);
            if (item.battlefield.CurrentTurn == PlayerTeam.Neutral)
            {
                item.battlefield.CurrentTurn++;
            }
        }

        foreach (var item in _entitiesUnits)
        {
            if (item.playerTeam.Team == item.battlefieldLink.BattlefieldEntity.battlefield.CurrentTurn)
            {
                item.isUnitTurn = true;
                item.scriptLink.Script.GetComponent<UnitBase>().IsUnitTurn = true;
            }
            else
            {
                item.isUnitTurn = false;
                item.scriptLink.Script.GetComponent<UnitBase>().IsUnitTurn = false;
                item.isSelected = false;
                item.scriptLink.Script.GetComponent<UnitBase>().IsSelected = false;
            }
        }

        var entitiesEndTurnArray = _entitiesEndTurn.GetEntities();
        foreach (var item in entitiesEndTurnArray)
        {
            item.isEndTurn = false;
        }

    }
}