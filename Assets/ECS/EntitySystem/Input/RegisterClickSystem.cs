
using Entitas;
using UnityEngine;

public class RegisterClickSystem : IExecuteSystem
{
    LayerMask layerMask = LayersManager.LayerMaskMap;

    public RegisterClickSystem(Contexts contexts) { }

    public void Execute()
    {
        if (Input.GetMouseButtonDown(0))
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

                    if (cellEntity.cell.InnerEntity != null)
                    {

                        cellEntity.cell.InnerEntity.AddClicked(hits[i].point);
                    }
                    else
                    {
                        cellEntity.AddClicked(hits[i].point);
                    }
                }
            }
        }
    }
}