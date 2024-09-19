using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class Drawer : MonoBehaviour
{
    [SerializeField] private Simulation Simulation;
    [SerializeField] private GameObject Screen;
    private Camera Camera;
    private IBrush Brush;

    private const int LeftMouseButtonIndex = 0;
    private const int RightMouseButtonIndex = 1;

    private void Start()
    {
        Camera = GetComponent<Camera>();
        Simulation.UpdatedSimulation += OnUpdatedSimulation;
    }

    private void OnUpdatedSimulation()
    {
        Brush = Simulation.PaintBrush;

        if(Input.GetMouseButton(LeftMouseButtonIndex) || Input.GetMouseButton(RightMouseButtonIndex))
        {
            //https://discussions.unity.com/t/how-do-i-select-a-sprites-pixel-by-mouse-click/252475/2
            //(modified)

            Vector2 position = Camera.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hitCollider = Physics2D.OverlapPoint(position);

            GameObject gameObject = hitCollider.gameObject;
            Sprite sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
            Rect rect = sprite.textureRect;

            // calculate the distance of the mouse from the center of the sprite's transform
            float rowX = position.x - gameObject.transform.position.x;
            float rowY = position.y - gameObject.transform.position.y;

            // convert the x and y values from units to pixels
            rowX *= sprite.pixelsPerUnit;
            rowY *= sprite.pixelsPerUnit;

            // modify so pixel distance from bottom left corner instead of from center
            rowX += rect.width / 2;
            rowY += rect.height / 2;

            // adjust for location of sprite on original texture
            rowX += rect.x;
            rowY += rect.y;

            int x = Mathf.FloorToInt(rowX);
            int y = Mathf.FloorToInt(rowY);

            Vector2Int cellPosition = new Vector2Int(x, y);

            if(Simulation.Cells[cellPosition].CanBeDeleted)
            {
                if(Input.GetMouseButton(LeftMouseButtonIndex))
                {
                    Brush.SpawnCell<Powder>(cellPosition);
                }
                else
                {
                    Brush.EraseCell(cellPosition);
                }
            }
        }
    }
}
