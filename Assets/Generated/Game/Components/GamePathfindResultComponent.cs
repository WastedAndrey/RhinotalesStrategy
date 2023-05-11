//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public PathfindResultComponent pathfindResult { get { return (PathfindResultComponent)GetComponent(GameComponentsLookup.PathfindResult); } }
    public bool hasPathfindResult { get { return HasComponent(GameComponentsLookup.PathfindResult); } }

    public void AddPathfindResult(UnityEngine.Vector3 newStartPosition, UnityEngine.Vector3 newTargetPosition, System.Collections.Generic.List<UnityEngine.Vector3> newPath) {
        var index = GameComponentsLookup.PathfindResult;
        var component = (PathfindResultComponent)CreateComponent(index, typeof(PathfindResultComponent));
        component.StartPosition = newStartPosition;
        component.TargetPosition = newTargetPosition;
        component.Path = newPath;
        AddComponent(index, component);
    }

    public void ReplacePathfindResult(UnityEngine.Vector3 newStartPosition, UnityEngine.Vector3 newTargetPosition, System.Collections.Generic.List<UnityEngine.Vector3> newPath) {
        var index = GameComponentsLookup.PathfindResult;
        var component = (PathfindResultComponent)CreateComponent(index, typeof(PathfindResultComponent));
        component.StartPosition = newStartPosition;
        component.TargetPosition = newTargetPosition;
        component.Path = newPath;
        ReplaceComponent(index, component);
    }

    public void RemovePathfindResult() {
        RemoveComponent(GameComponentsLookup.PathfindResult);
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

    static Entitas.IMatcher<GameEntity> _matcherPathfindResult;

    public static Entitas.IMatcher<GameEntity> PathfindResult {
        get {
            if (_matcherPathfindResult == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.PathfindResult);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherPathfindResult = matcher;
            }

            return _matcherPathfindResult;
        }
    }
}
