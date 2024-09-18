using UnityEngine;


public class ScreenRenderer : MonoBehaviour
{
    private SpriteRenderer Renderer;

    private int Width;
    private int Height;

    private Texture2D Texture;
    private Sprite ImageSprite;

    Color32[] CellColors;

    private int Index;

    private void Awake()
    {
        Renderer = GetComponent<SpriteRenderer>();
        Width = SimulationSettings.Resolution.x;
        Height = SimulationSettings.Resolution.y;

        Texture = new Texture2D(Width, Height);

        Texture.filterMode = FilterMode.Point;

        ImageSprite = Sprite.Create(Texture, new Rect(0, 0, Width, Height), Vector2.zero);
        Renderer.sprite = ImageSprite;

        CellColors = new Color32[Width * Height];
    }

    public void AddColorToRenderQueue(Color32 color)
    {
        CellColors[Index] = color;
        Index++;
    }

    public void RenderScreen()
    {
        Texture.SetPixels32(CellColors);
        Texture.Apply();
        ImageSprite = Sprite.Create(Texture, new Rect(0, 0, Width, Height),Vector2.zero);
        Index = 0;
    }
}
