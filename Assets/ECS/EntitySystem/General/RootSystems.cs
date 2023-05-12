
public class RootSystems : Feature
{
    public RootSystems(Contexts contexts)
    {
        // Init
        Add(new InitGameSystem(contexts));
        Add(new UnitInitSystem(contexts));

        // UI
        Add(new ButtonRequestEndTurnSystem(contexts));

        // Game
        Add(new RegisterClickSystem(contexts));
        Add(new UnitSelectionSystem(contexts));
        Add(new MovementOrderSystem(contexts));
        Add(new EndTurnSystem(contexts));

        
        // Vizualization
        Add(new PathfindSystem(contexts));
        Add(new PathfindVizualizerSystem(contexts));

        // Animation
        Add(new MovementAnimationSystem(contexts));

        // Unity Scripts Update
        Add(new UpdateUnitViewSystem(contexts));
        Add(new DestroyUnitSystem(contexts));
        // Clearing
        Add(new RemoveClickSystem(contexts));
        Add(new RemoveButtonClickSystem(contexts));
        Add(new RemovePathfindResultSystem(contexts));
    }
}