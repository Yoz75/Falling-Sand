using UnityEngine;

public interface IBrush
{

    public void SpawnCell<T>(Vector2Int position) where T : Cell, new();

    public void EraseCell(Vector2Int position);
}
  