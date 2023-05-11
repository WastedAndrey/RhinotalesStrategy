//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public CellComponent cell { get { return (CellComponent)GetComponent(GameComponentsLookup.Cell); } }
    public bool hasCell { get { return HasComponent(GameComponentsLookup.Cell); } }

    public void AddCell(GameEntity newInnerEntity) {
        var index = GameComponentsLookup.Cell;
        var component = (CellComponent)CreateComponent(index, typeof(CellComponent));
        component.InnerEntity = newInnerEntity;
        AddComponent(index, component);
    }

    public void ReplaceCell(GameEntity newInnerEntity) {
        var index = GameComponentsLookup.Cell;
        var component = (CellComponent)CreateComponent(index, typeof(CellComponent));
        component.InnerEntity = newInnerEntity;
        ReplaceComponent(index, component);
    }

    public void RemoveCell() {
        RemoveComponent(GameComponentsLookup.Cell);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherCell;

    public static Entitas.IMatcher<GameEntity> Cell {
        get {
            if (_matcherCell == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Cell);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherCell = matcher;
            }

            return _matcherCell;
        }
    }
}