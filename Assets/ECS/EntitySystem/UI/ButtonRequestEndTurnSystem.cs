
using Entitas;
using UnityEngine;

public class ButtonRequestEndTurnSystem : IExecuteSystem
{
    private Contexts _contexts;
    readonly IGroup<GameEntity> _entities;
    readonly IGroup<GameEntity> _entitiesLockUI;

    public ButtonRequestEndTurnSystem(Contexts contexts)
    {
        _contexts = contexts;
        _entities = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.ButtonClicked, GameMatcher.ButtonRequestEndTurn));
        _entitiesLockUI = contexts.game.GetGroup(GameMatcher.LockUI);
    }

    public void Execute() 
    {
        if (_entitiesLockUI.count > 0)
            return;

        foreach (var item in _entities)
        {
            item.isEndTurn = true;
        }
    }
}
