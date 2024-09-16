using UnityEngine;

public class VoidCell : Cell
{
    public override void Init()
    {
        Color = Color.black;
    }
    public override void OnTick(CellNeighbors neighbors)
    {
        return;
    }
}
