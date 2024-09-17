using UnityEngine;


public class ScreenRenderer : MonoBehaviour
{
    private SpriteRenderer Renderer;

    private int Width;
    private int Height;

    private Texture2D Texture;
    private Sprite ImageSprite;

    private void Awake()
    {
        Renderer = GetComponent<SpriteRenderer>();
        Width = SimulationSettings.Resolution.x;
        Height = SimulationSettings.Resolution.y;

        Texture = new Texture2D(Width, Height);

        Texture.filterMode = FilterMode.Point;

        ImageSprite = Sprite.Create(Texture, new Rect(0, 0, Width, Height), Vector2.zero);
        Renderer.sprite = ImageSprite;
    }

    public void RenderCellAtTexture(Cell cell)
    {
        Texture.SetPixel(cell.Position.x, cell.Position.y, cell.Color);
    }

    public void UpdateScreen()
    {
        Texture.Apply();
        ImageSprite = Sprite.Create(Texture, new Rect(0, 0, Width, Height),Vector2.zero);
    }
}
