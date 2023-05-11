using Entitas;

public class BattlefieldComponent : IComponent
{
    public PlayerTeam CurrentTurn;
    public MapSettings MapSettings;
    public GameEntity[,] Cells;
    public bool[,] CellsPassMap; // true = empty, false = filled and can not be passed
}