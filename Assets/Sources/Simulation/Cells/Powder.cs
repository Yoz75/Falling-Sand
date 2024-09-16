using UnityEngine;

public class Powder : Cell
{
    public override void Init()
    {
        Color = Color.blue;
        IsRequireNeighborsOnTick = true;
    }

    public override void OnTick(CellNeighbors neighbors)
    {
        if(neighbors.Down is VoidCell)
        {
            neighbors.Down.SwapPositions(neighbors.Down);

        }
            neighbors.Down.Color = Color.red;
    }
}
