
using Entitas;
using UnityEngine;

public class UnitSelectionSystem : IExecuteSystem
{
    private Contexts _contexts;
    readonly IGroup<GameEntity> _entitiesClicked;
    readonly IGroup<GameEntity> _entitiesSelected;

    public UnitSelectionSystem(Contexts contexts)
    {
        _contexts = contexts;
        _entitiesClicked = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Clicked, GameMatcher.UnitTurn));
        _entitiesSelected = contexts.game.GetGroup(GameMatcher.Selected);
    }

    public void Execute()
    {
        foreach (var item in _entitiesClicked)
        {
            ClearSelected();

            item.isSelected = true;
            item.scriptLink.Script.GetComponent<UnitBase>().IsSelected = true;
            break;
        }
    }

    private void ClearSelected()
    {
        var entitiesArray = _entitiesSelected.GetEntities();
        for (int i = 0; i < entitiesArray.Length; i++)
        {
            entitiesArray[i].isSelected = false;
            entitiesArray[i].scriptLink.Script.GetComponent<UnitBase>().IsSelected = false;
        }
    }
}