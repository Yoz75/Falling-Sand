
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

    public void SwapPositions(Cell other)
    {
        Vector2Int tempPosition = other.Position;
        other.Position = Position;
        Position = tempPosition;

        Simulation.CellMatrix[other.Position] = other;
        Simulation.CellMatrix[Position] = this;

        Position = -Vector2Int.one;
    }
}
