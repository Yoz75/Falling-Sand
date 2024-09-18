using UnityEngine;

[RequireComponent (typeof(Camera))]
public class CameraMover : MonoBehaviour
{
    [SerializeField] private float MoveSensitivity;
    [SerializeField] private float ZoomSensitivity;

    private const int MouseWheelIndex = 2;

    private const float BaseMoveSensitivityMultiplier = 0.01f;

    private Camera Camera;

    private string ZoomAxis = "Zoom";

    private void Start()
    {
        Camera = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if(Input.GetMouseButton(MouseWheelIndex))
        {
            Vector2 rowBias = Input.mousePosition;


            Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);

            Vector3 bias = Camera.ScreenToWorldPoint(rowBias ) - Camera.ScreenToWorldPoint(screenCenter);

            Camera.transform.position += 
                bias * MoveSensitivity * BaseMoveSensitivityMultiplier;
        }

        if(Input.GetAxis(ZoomAxis) != 0)
        {
            Camera.orthographicSize += Input.GetAxis(ZoomAxis) * ZoomSensitivity;
            Camera.orthographicSize = Mathf.Clamp(Camera.orthographicSize, 0.1f, 1000f);
        }
    }
}
