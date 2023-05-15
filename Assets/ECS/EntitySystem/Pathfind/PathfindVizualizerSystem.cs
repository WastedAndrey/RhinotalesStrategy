

using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class PathfindVizualizerSystem : IExecuteSystem
{
    readonly Contexts _contexts;
    readonly IGroup<GameEntity> _entitiesPathfind;
    readonly IGroup<GameEntity> _entitiesVizualizer;
    readonly IGroup<GameEntity> _entitiesLockInput;
    public PathfindVizualizerSystem(Contexts contexts)
    {
        _contexts = contexts;
        _entitiesPathfind = contexts.game.GetGroup(GameMatcher.PathfindResult);
        _entitiesVizualizer = contexts.game.GetGroup(GameMatcher.PathfindVizualizer);
        _entitiesLockInput = contexts.game.GetGroup(GameMatcher.LockInput);
    }

    public void Execute()
    {
        var entityPathfind = _entitiesPathfind.GetSingleEntity();

        foreach (var item in _entitiesVizualizer)
        {
            var vizualizer = item.scriptLink.Script.GetComponent<PathfindVizualizer>(); // GetComponent should be removed from update of cause
            // i just need learn or invent some good ECS pattern for update components     
            if (entityPathfind == null || _entitiesLockInput.count > 0 || entityPathfind.pathfindResult.StartPosition == entityPathfind.pathfindResult.TargetPosition)
            {
                vizualizer.Hide();
                continue;
            }

            if (entityPathfind.pathfindResult.Path == null)
            {
                vizualizer.SetPathUnavailable(entityPathfind.pathfindResult.TargetPosition);
                continue;
            }

            vizualizer.SetPath(entityPathfind.pathfindResult.Path);
        }
    }
}