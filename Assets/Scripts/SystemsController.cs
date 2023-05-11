using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using Sirenix.OdinInspector;

public class SystemsController : MonoBehaviour
{
    private RootSystems _systems;

    private void Awake()
    {
        CreateEntities();
    }

    private void Update()
    {
        ExecuteSystems();
    }

    [Button]
    private void CreateEntities()
    {
        var contexts = Contexts.sharedInstance;

        _systems = new RootSystems(contexts);
        _systems.Initialize();
    }

    [Button]
    private void ExecuteSystems()
    {
        _systems.Execute();
    }

    [Button]
    private void InspectEntities()
    {
        var entities = Contexts.sharedInstance.game.GetEntities();

        foreach (var entity in entities)
        {
            Debug.Log(entity);
        }
        Debug.Log("Length: " + entities.Length);
    }

    [Button]
    private void DestroyEntities()
    {
        var entities = Contexts.sharedInstance.game.GetEntities();
        foreach (var entity in entities)
        {
            entity.Destroy();
        }
    }
}
