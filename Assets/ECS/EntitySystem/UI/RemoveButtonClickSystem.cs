
using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class RemoveButtonClickSystem : IExecuteSystem
{
    private Contexts _contexts;
    readonly IGroup<GameEntity> _entities;

    public RemoveButtonClickSystem(Contexts contexts)
    {
        _contexts = contexts;
        _entities = contexts.game.GetGroup(GameMatcher.ButtonClicked);
    }

    public void Execute()
    {
        GameEntity[] entitiesArray = _entities.GetEntities();
        foreach (var item in entitiesArray)
        {
            item.isButtonClicked = false;
        }
    }
}