using Entitas;
using PathFindLib.PathFindA;
using System.Collections.Generic;
using UnityEngine;

public class PathfindSystem : IExecuteSystem
{
    readonly Contexts _contexts;
    readonly IGroup<GameEntity> _entities;
    LayerMask layerMask = LayersManager.LayerMaskMap;

    public PathfindSystem(Contexts contexts)
    {
        _contexts = contexts;
        _entities = contexts.game.GetGroup(GameMatcher.Selected);
    }

    public void Execute()
    {
        GameEntity cellEntity = TryHitCell();
        if (cellEntity == null)
        {
            foreach (var item in _entities)
            {
                Vector3 startPosition = item.battlefieldLink.BattlefieldEntity.battlefield.MapSettings.GetCellPosition(item.cellIndex.Index);
                Vector3 targetPosition = item.battlefieldLink.BattlefieldEntity.battlefield.MapSettings.GetCellPosition(item.cellIndex.Index);
                item.ReplacePathfindResult(startPosition, targetPosition, null);
            }

            return;
        }
           

        foreach (var item in _entities)
        {
            BattlefieldComponent battlefield = item.battlefieldLink.BattlefieldEntity.battlefield;
            Vector3 startPosition = battlefield.MapSettings.GetCellPosition(item.cellIndex.Index);
            Vector3 targetPosition = battlefield.MapSettings.GetCellPosition(cellEntity.cellIndex.Index);
            List<Vector2Int> pathCellIndex = PathFindA.PathFind(item.cellIndex.Index, cellEntity.cellIndex.Index, battlefield.CellsPassMap);
            
            List<Vector3> pathWorldPosition = null;
            if (pathCellIndex != null && pathCellIndex.Count > 0)
            {
                pathCellIndex.Reverse();
                pathWorldPosition = item.battlefieldLink.BattlefieldEntity.battlefield.MapSettings.GetCellPositions(pathCellIndex);
                pathWorldPosition.Add(targetPosition);
            }
           
                
            
            item.ReplacePathfindResult(startPosition, targetPosition, pathWorldPosition);
        }
    }

    private GameEntity TryHitCell()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var hits = Physics.RaycastAll(ray, Mathf.Infinity, layerMask);

        for (int i = 0; i < hits.Length; i++)
        {
            var rigidBody = hits[i].collider.attachedRigidbody;
            if (rigidBody != null)
            {
                var entityLink = rigidBody.GetComponent<EntityLink>();

                Vector2Int cellIndex = entityLink.Entity.battlefield.MapSettings.GetCellIndex(hits[i].point);
                GameEntity cellEntity = entityLink.Entity.battlefield.Cells[cellIndex.x, cellIndex.y];
                return cellEntity;
            }
        }

        return null;
    }
}