
using System;
using UnityEngine;

public class Cell
{
    public Color Color
    {
        get;
        set;
    }

    public bool CanBeDeleted = true;

    public Color DecorColor;

    public Vector2Int Position;

    public MoveDirection MoveDirection;

    public bool IsRequireNeighborsOnTick = false;

    public virtual void Init()
    {
        throw new NotImplementedException("YOU ARE NOT OVERRIDED INIT METHOD, DUMBASS");
    }

    public virtual void OnTick(CellNeighbors neighbors)
    {
        throw new NotImplementedException("YOU ARE NOT OVERRIDED ONTICK METHOD, DUMBASS");
    }

    public static void SwapPositions(Cell self, Cell other)
    {
        Vector2Int tempPosition = self.Position;
        self.Position = other.Position;
        other.Position = tempPosition;

        Simulation.Cells[other.Position] = other;
        Simulation.Cells[self.Position] = self;
    }
}
