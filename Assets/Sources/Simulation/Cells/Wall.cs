using UnityEngine;

public class Wall : Cell
{
    public override void Init()
    {
        Color = Color.gray;
    }

    public override void OnTick(CellNeighbors neighbors)
    {
        return;
    }
}
