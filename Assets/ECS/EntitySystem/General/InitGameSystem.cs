using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class InitData
{
    public List<UnitBase> units;
}

public class InitGameSystem : IInitializeSystem
{
    private readonly Contexts _contexts;

    public InitGameSystem(Contexts contexts)
    {
        _contexts = contexts;
    }

    public void Initialize()
    {
      
    }
}