
using Entitas;
using UnityEngine;

public class MovementAnimationSystem : IExecuteSystem
{
    readonly Contexts _contexts;
    readonly IGroup<GameEntity> _entities;

    public MovementAnimationSystem(Contexts contexts)
    {
        _contexts = contexts;
        _entities = contexts.game.GetGroup(GameMatcher.MovementAnimation);
    }

    public void Execute()
    {
        var entitiesArray = _entities.GetEntities();
        foreach (var item in entitiesArray)
        {
            item.movementAnimation.Progress += Time.deltaTime * 8;
            int previousPositionIndex = (int)item.movementAnimation.Progress;
            int nextPositionIndex = previousPositionIndex + 1;
            float progressBetweenPoints = item.movementAnimation.Progress % 1;
            previousPositionIndex = Mathf.Clamp(previousPositionIndex, 0, item.movementAnimation.Path.Count - 1);
            nextPositionIndex = Mathf.Clamp(nextPositionIndex, 0, item.movementAnimation.Path.Count - 1);
            
            Vector2Int previousPosition = item.movementAnimation.Path[previousPositionIndex];
            Vector2Int nextPosition = item.movementAnimation.Path[nextPositionIndex];
            Vector2 currentPosition = Vector2.Lerp(previousPosition, nextPosition, progressBetweenPoints);

            item.scriptLink.Script.gameObject.transform.position = item.movementAnimation.MapSettings.GetCellPosition(currentPosition);

            if (item.movementAnimation.Progress >= item.movementAnimation.Path.Count - 1)
            {
                item.cellIndex.Index = item.movementAnimation.Path[nextPositionIndex];
                item.isLockInput = false;
                item.isLockUI = false;
                item.RemoveMovementAnimation();
            }
        }
    }
}