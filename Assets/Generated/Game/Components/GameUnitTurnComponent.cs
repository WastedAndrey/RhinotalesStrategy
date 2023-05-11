//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly UnitTurnComponent unitTurnComponent = new UnitTurnComponent();

    public bool isUnitTurn {
        get { return HasComponent(GameComponentsLookup.UnitTurn); }
        set {
            if (value != isUnitTurn) {
                var index = GameComponentsLookup.UnitTurn;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : unitTurnComponent;

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

    static Entitas.IMatcher<GameEntity> _matcherUnitTurn;

    public static Entitas.IMatcher<GameEntity> UnitTurn {
        get {
            if (_matcherUnitTurn == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.UnitTurn);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherUnitTurn = matcher;
            }

            return _matcherUnitTurn;
        }
    }
}