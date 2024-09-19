using UnityEngine;

public class Brush : IBrush
{
    private Simulation Simulation;

    public Brush(Simulation simulation)
    {
        Simulation = simulation;
    }

    public void SpawnCell<T>(Vector2Int position) where T : Cell, new()
    {
        Simulation.Cells[position] = null;
        Simulation.Cells[position] = new T();
        Simulation.Cells[position].Position = position;
        Simulation.Cells[position].Init();
    }

    public void EraseCell(Vector2Int position)
    {
        SpawnCell<VoidCell>(position);
    }
}