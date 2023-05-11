
using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class RemoveClickSystem : IExecuteSystem
{
    private Contexts _contexts;
    readonly IGroup<GameEntity> _entities;

    public RemoveClickSystem(Contexts contexts)
    {
        _contexts = contexts;
        _entities = contexts.game.GetGroup(GameMatcher.Clicked);
    }

    public void Execute()
    {
        GameEntity[] entitiesArray = _entities.GetEntities();
        foreach (var item in entitiesArray)
        {
            item.RemoveClicked();
        }
    }
}