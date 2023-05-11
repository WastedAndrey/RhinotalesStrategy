//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public CellIndexComponent cellIndex { get { return (CellIndexComponent)GetComponent(GameComponentsLookup.CellIndex); } }
    public bool hasCellIndex { get { return HasComponent(GameComponentsLookup.CellIndex); } }

    public void AddCellIndex(UnityEngine.Vector2Int newIndex) {
        var index = GameComponentsLookup.CellIndex;
        var component = (CellIndexComponent)CreateComponent(index, typeof(CellIndexComponent));
        component.Index = newIndex;
        AddComponent(index, component);
    }

    public void ReplaceCellIndex(UnityEngine.Vector2Int newIndex) {
        var index = GameComponentsLookup.CellIndex;
        var component = (CellIndexComponent)CreateComponent(index, typeof(CellIndexComponent));
        component.Index = newIndex;
        ReplaceComponent(index, component);
    }

    public void RemoveCellIndex() {
        RemoveComponent(GameComponentsLookup.CellIndex);
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

    static Entitas.IMatcher<GameEntity> _matcherCellIndex;

    public static Entitas.IMatcher<GameEntity> CellIndex {
        get {
            if (_matcherCellIndex == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.CellIndex);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherCellIndex = matcher;
            }

            return _matcherCellIndex;
        }
    }
}
