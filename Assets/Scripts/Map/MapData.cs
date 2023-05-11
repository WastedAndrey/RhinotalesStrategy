
using UnityEngine;



[System.Serializable]
public class MapData
{
    [System.Serializable]
    private class CellInfoArray
    {
        [SerializeField]
        public CellInfo[] Array;

        public CellInfoArray(int count)
        {
            Array = new CellInfo[count];

            for (int i = 0; i < count; i++)
            {
                Array[i] = new CellInfo();
            }
        }
    }

    [SerializeField]
    private CellInfoArray[] cells;


    public void CreateCells(Vector2Int count)
    {
        cells = new CellInfoArray[count.y];
        for (int i = 0; i < count.y; i++)
        {
            cells[i] = new CellInfoArray(count.x);

            for (int j = 0; j < cells[i].Array.Length; j++)
            {
                cells[i].Array[j].Index = new Vector2Int(j, i);
            }
        }
    }

    public CellInfo GetCell(Vector2Int index)
    {
        return cells[index.y].Array[index.x];
    }
}