using UnityEngine;

public class TestFlashingCell : Cell
{
    public override void Init()
    {

        Color = Random.ColorHSV();
    }

    public override void OnTick(CellNeighbors neighbors)
    {
        Color = Random.ColorHSV();
    }
}
