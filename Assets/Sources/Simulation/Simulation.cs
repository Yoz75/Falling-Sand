using System.Collections.Generic;
using UnityEngine;


public class Simulation : MonoBehaviour
{
    public class Brush
    {
        private Simulation Simulation;

        public Brush(Simulation simulation)
        {
            Simulation = simulation;
        }

        public void SpawnCell<T>(Vector2Int position) where T : Cell, new()
        {
            Simulation.Cells[position] = new T();
            Simulation.Cells[position].Init();
        }

        public void EraseCell(Vector2Int position)
        {
            SpawnCell<VoidCell>(position);
        }
    }

    public const string SimulationTag = "Simulation";

    public static Dictionary<Vector2Int, Cell> Cells;

    [SerializeField] private ScreenRenderer Renderer;

    private Brush PaintBrush;

    private int Width, Height;

    private void Start()
    {
        PaintBrush = new Brush(this);

        Width = SimulationSettings.Resolution.x;
        Height = SimulationSettings.Resolution.y;    

        Cells = new Dictionary<Vector2Int, Cell>(Width * Height);

        Vector2Int cellPosition = new Vector2Int();
        for(int x = 0; x < Width; x++)
        {
            for(int y = 0; y < Height; y++)
            {
                cellPosition.x = x;
                cellPosition.y = y;

                if(x == 0 || y == 0 || x == Width-1 || y == Height-1)
                {
                    Cells[cellPosition] = new Wall();
                }
                else
                {
                    Cells[cellPosition] = new VoidCell();
                }

                if(UnityEngine.Random.value < 0.01 && Cells[cellPosition] is not Wall)
                {
                    Cells[cellPosition] = new Powder();
                }

                Cells[cellPosition].Position = new Vector2Int(x, y);
                Cells[cellPosition].Init();
            }
        }
    }

    private void Update()
    {
        UpdateCells();
        RenderCells();
    }

    private void RenderCells()
    {
        Vector2Int cellPosition = new();
        for(int y = 0; y < Height; y++)
        {
            for(int x = 0; x < Width; x++)
            {
                cellPosition.x = x;
                cellPosition.y = y;

                Renderer.AddColorToRenderQueue(Cells[cellPosition].Color);
            }
        }
        Renderer.RenderScreen();
    }

    private void UpdateCells()
    {
        Vector2Int cellPosition = new();

        for(int x = 0; x < Width; x++)
        {
            for(int y = 0; y < Height; y++)
        {

                cellPosition.x = x;
                cellPosition.y = y;

                if(Cells[cellPosition].IsRequireNeighborsOnTick)
                {
                    Cells[cellPosition].OnTick(GetNeighborsAt(cellPosition));
                }
                else
                {
                    Cells[cellPosition].OnTick(default);
                }
            }
        }
    }

    private CellNeighbors GetNeighborsAt(Vector2Int position)
    {
        CellNeighbors neighbors = new CellNeighbors();

        neighbors.Up = Cells[new Vector2Int(position.x, position.y + 1)];
        neighbors.RightUp = Cells[new Vector2Int(position.x + 1, position.y + 1)];
        neighbors.Right = Cells[new Vector2Int(position.x + 1, position.y)];
        neighbors.DownRight = Cells[new Vector2Int(position.x + 1, position.y - 1)];
        neighbors.Down = Cells[new Vector2Int(position.x, position.y - 1)];
        neighbors.DownLeft = Cells[new Vector2Int(position.x - 1, position.y - 1)];
        neighbors.Left = Cells[new Vector2Int(position.x - 1, position.y)];
        neighbors.LeftUp = Cells[new Vector2Int(position.x - 1, position.y + 1)];

        return neighbors;
    }
}
