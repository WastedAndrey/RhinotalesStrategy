//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly ButtonClickedComponent buttonClickedComponent = new ButtonClickedComponent();

    public bool isButtonClicked {
        get { return HasComponent(GameComponentsLookup.ButtonClicked); }
        set {
            if (value != isButtonClicked) {
                var index = GameComponentsLookup.ButtonClicked;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : buttonClickedComponent;

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

    static Entitas.IMatcher<GameEntity> _matcherButtonClicked;

    public static Entitas.IMatcher<GameEntity> ButtonClicked {
        get {
            if (_matcherButtonClicked == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.ButtonClicked);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherButtonClicked = matcher;
            }

            return _matcherButtonClicked;
        }
    }
}
