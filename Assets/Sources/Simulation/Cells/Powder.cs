using UnityEngine;

public class Powder : Cell
{

    protected float Fluidness = 0.5f;

    public override void Init()
    {
        Color = Color.blue;
        IsRequireNeighborsOnTick = true;
    }

    public override void OnTick(CellNeighbors neighbors)
    {
        if(neighbors.Down is VoidCell)
        {
            SwapPositions(this, neighbors.Down);
            return;
        }

        System.Random random = new System.Random();
        if(neighbors.Down is Powder && neighbors.DownLeft is VoidCell)
        {
            var value = random.NextDouble();

            if(value <= Fluidness)
            {
                SwapPositions(this, neighbors.DownLeft);
            }
        }
        else if(neighbors.Down is Powder && neighbors.DownRight is VoidCell)
        {
            var value = random.NextDouble();

            if(value <= Fluidness)
            {
                SwapPositions(this, neighbors.DownRight);
            }
        }
    }
}
