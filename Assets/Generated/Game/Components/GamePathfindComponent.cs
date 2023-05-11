//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public PathfindComponent pathfind { get { return (PathfindComponent)GetComponent(GameComponentsLookup.Pathfind); } }
    public bool hasPathfind { get { return HasComponent(GameComponentsLookup.Pathfind); } }

    public void AddPathfind(UnityEngine.Vector2Int newStartPosition, UnityEngine.Vector2Int newTargetPosition) {
        var index = GameComponentsLookup.Pathfind;
        var component = (PathfindComponent)CreateComponent(index, typeof(PathfindComponent));
        component.StartPosition = newStartPosition;
        component.TargetPosition = newTargetPosition;
        AddComponent(index, component);
    }

    public void ReplacePathfind(UnityEngine.Vector2Int newStartPosition, UnityEngine.Vector2Int newTargetPosition) {
        var index = GameComponentsLookup.Pathfind;
        var component = (PathfindComponent)CreateComponent(index, typeof(PathfindComponent));
        component.StartPosition = newStartPosition;
        component.TargetPosition = newTargetPosition;
        ReplaceComponent(index, component);
    }

    public void RemovePathfind() {
        RemoveComponent(GameComponentsLookup.Pathfind);
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

    static Entitas.IMatcher<GameEntity> _matcherPathfind;

    public static Entitas.IMatcher<GameEntity> Pathfind {
        get {
            if (_matcherPathfind == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Pathfind);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherPathfind = matcher;
            }

            return _matcherPathfind;
        }
    }
}