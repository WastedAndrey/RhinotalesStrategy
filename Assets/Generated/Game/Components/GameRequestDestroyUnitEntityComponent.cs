//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly RequestDestroyUnitEntity requestDestroyUnitEntityComponent = new RequestDestroyUnitEntity();

    public bool isRequestDestroyUnitEntity {
        get { return HasComponent(GameComponentsLookup.RequestDestroyUnitEntity); }
        set {
            if (value != isRequestDestroyUnitEntity) {
                var index = GameComponentsLookup.RequestDestroyUnitEntity;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : requestDestroyUnitEntityComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
                }
            }
        }
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

    static Entitas.IMatcher<GameEntity> _matcherRequestDestroyUnitEntity;

    public static Entitas.IMatcher<GameEntity> RequestDestroyUnitEntity {
        get {
            if (_matcherRequestDestroyUnitEntity == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.RequestDestroyUnitEntity);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherRequestDestroyUnitEntity = matcher;
            }

            return _matcherRequestDestroyUnitEntity;
        }
    }
}
