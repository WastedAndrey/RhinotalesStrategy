
using UnityEngine;

[CreateAssetMenu(fileName = "FactoryBattlefield", menuName = "ScriptableObjects/Factories/FactoryBattlefield")]
public class EntityFactoryBattlefield : EntityFactoryBase
{
    protected override GameEntity CreateEntityInternal(GameObject gameObject, GameEntity entity)
    {
        Map map = gameObject.GetComponent<Map>();
        EntityLink link = gameObject.GetComponent<EntityLink>();
        entity.AddScriptLink(link);

        var cells = CreateCells(entity, map.Settings.CellsCount);
        entity.AddBattlefield(PlayerTeam.TeamRed, map.Settings, cells, GetCellMap(map.Settings));

        return entity;
    }

    private GameEntity[,] CreateCells(GameEntity battleField, Vector2Int cellsCount)
    {
        var contexts = Contexts.sharedInstance;

        GameEntity[,] result = new GameEntity[cellsCount.x, cellsCount.y];

        for (int i = 0; i < cellsCount.x; i++)
        {
            for (int j = 0; j < cellsCount.y; j++)
            {
                var entity = contexts.game.CreateEntity();
                entity.AddCell(null);
                entity.AddCellIndex(new Vector2Int(i, j));
                entity.AddBattlefieldLink(battleField);
                result[i, j] = entity;
            }
        }

        return result;
      
    }

    private bool[,] GetCellMap(MapSettings mapSettings)
    {
        bool[,] result = new bool[mapSettings.CellsCount.x, mapSettings.CellsCount.y];
        for (int i = 0; i < result.GetLength(0); i++)
        {
            for (int j = 0; j < result.GetLength(1); j++)
            {
                result[i, j] = true;
            }
        }
        return result;
    }
}