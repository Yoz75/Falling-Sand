
using JetBrains.Annotations;
using UnityEngine;


public abstract class Cell
{
    public Color Color
    {
        get;
        set;
    }

    public Color DecorColor;

    public Vector2Int Position;

    public MoveDirection MoveDirection;

    public bool IsRequireNeighborsOnTick = false;

    public abstract void Init();

    public abstract void OnTick(CellNeighbors neighbors);

    public static void SwapPositions(Cell self, Cell other)
    {
        Vector2Int tempPosition = self.Position;
        self.Position = other.Position;
        other.Position = tempPosition;

        Simulation.NextFrame[other.Position] = other;
        Simulation.NextFrame[self.Position] = self;
    }
}
