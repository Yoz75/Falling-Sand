using UnityEngine;
using UnityEngine.UI;


public class ScreenRenderer : MonoBehaviour
{
    private Image Image;
    private int Width;
    private int Height;

    private Texture2D Texture;
    private Sprite ImageSprite;

    private void Awake()
    {
        Image = GetComponent<Image>();
        Width = Screen.width / SimulationSettings.RenderScale;
        Height = Screen.height / SimulationSettings.RenderScale;

        Texture = new Texture2D(Width, Height);

        Texture.filterMode = FilterMode.Point;

        ImageSprite = Sprite.Create(Texture, new Rect(0, 0, Width, Height), Vector2.zero);
        Image.sprite = ImageSprite;
    }

    public void RenderCellAtTexture(Cell cell)
    {
        Texture.SetPixel(cell.Position.x, cell.Position.y, cell.Color);
    }

    public void UpdateScreen()
    {
        Texture.Apply();
        ImageSprite = Sprite.Create(Texture, new Rect(0, 0, Width, Height), Vector2.zero);
    }
}
