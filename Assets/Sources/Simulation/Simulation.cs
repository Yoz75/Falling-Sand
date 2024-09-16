using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;


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

    public static Dictionary<Vector2Int, Cell> CellMatrix;

    [SerializeField] private ScreenRenderer Renderer;

    private Dictionary<Vector2Int, Cell> Cells;

    Brush PaintBrush;

    private IEnumerator RenderEnumerator;

    private int X, Y;

    private int Width, Height;

    private void Start()
    {
        PaintBrush = new Brush(this);

        Width = Screen.width / SimulationSettings.RenderScale;
        Height = Screen.height / SimulationSettings.RenderScale;    

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

                if(Random.value < 0.01 && Cells[cellPosition] is not Wall)
                {
                    Cells[cellPosition] = new Powder();
                }

                Cells[cellPosition].Position = new Vector2Int(x, y);
                Cells[cellPosition].Init();
            }
        }

        CellMatrix = Cells;

        StartCoroutine(UpdateSimulation());
    }

    private IEnumerator UpdateSimulation()
    {
        int pixelsToRenderCount;
        int pixelsPerFrame;
        int otherPixels;
        int renderedPixels = 0;
        while(true)
        {
            UpdateCells();
            pixelsToRenderCount = Width * Height;
            pixelsPerFrame = pixelsToRenderCount / SimulationSettings.UpdatesPerSecond;
            otherPixels = pixelsToRenderCount - pixelsPerFrame * SimulationSettings.UpdatesPerSecond;

            while(renderedPixels < pixelsToRenderCount - otherPixels)
            {
                RenderCells(pixelsPerFrame);
                renderedPixels += pixelsPerFrame;
                yield return null;
            }


            RenderCells(otherPixels);

            renderedPixels = 0;
            X = 0;
            Y = 0;

            ApplyRender();
            yield return new WaitForSeconds(1 / SimulationSettings.UpdatesPerSecond);
        }
    }

    private void RenderCells(int cellsCount)
    {
        int renderedCells = 0;

        Vector2Int cellPosition = new Vector2Int();
        while(X < Width)
        {
            Y = 0;
            if(renderedCells >= cellsCount)
            {
                break;
            }

            while(Y < Height)
            {
                cellPosition.x = X;
                cellPosition.y = Y;
                if(renderedCells >= cellsCount)
                {
                    break;
                }

                Render(Cells[cellPosition]);
                renderedCells++;
                Y++;
            }
            X++;
        }

        void Render(Cell cell)
        {
            Renderer.RenderCellAtTexture(cell);
        }
    }

    private void ApplyRender()
    {
        Renderer.UpdateScreen();
    }

    private void UpdateCells()
    {

        Vector2Int cellPosition = new Vector2Int();
        for(int x = 0; x < Width; x++)
        {
            for(int y = 0; y < Height; y++)
            {
                cellPosition.x = x;
                cellPosition.y = y;

                if(Cells[cellPosition].Position == -Vector2Int.one)
                {
                    Cells[cellPosition] = new VoidCell();
                    Cells[cellPosition].Position = new Vector2Int(x, y);
                    Cells[cellPosition].Init();
                }

                if(Cells[cellPosition].IsRequireNeighborsOnTick)
                {
                    CellNeighbors neighbors = new CellNeighbors();
                    neighbors.Up = Cells[new Vector2Int(x, y +- 1)];
                    neighbors.UpRight = Cells[new Vector2Int(x + 1, y - 1)];
                    neighbors.Right = Cells[new Vector2Int(x + 1, y)];
                    neighbors.RightDown = Cells[new Vector2Int(x + 1, y + 1)];
                    neighbors.Down = Cells[new Vector2Int(x, y + 1)];
                    neighbors.DownLeft = Cells[new Vector2Int(x - 1, y + 1)];
                    neighbors.Left = Cells[new Vector2Int(x - 1, y)];
                    neighbors.LeftUp = Cells[new Vector2Int(x - 1, y - 1)];
                    Cells[cellPosition].OnTick(neighbors);
                }
                else
                {
                    Cells[cellPosition].OnTick(null);
                }
            }
        }
    }
}
