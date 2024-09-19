using UnityEngine;

public class TestFlashingCell : Cell
{
    public override void Init()
    {
        Name = "Test Flashing Cell";
        Description = "First cell type, created just for debug";

        Color = Random.ColorHSV();
    }

    public override void OnTick(CellNeighbors neighbors)
    {
        Color = Random.ColorHSV();
    }
}
