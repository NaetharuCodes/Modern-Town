using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("Pan")]
    [SerializeField] private float panSpeed = 10f;

    [Header("Zoom")]
    [SerializeField] private float zoomSpeed = 5f;
    [SerializeField] private float minZoom = 3f;
    [SerializeField] private float maxZoom = 20f;

    private Camera _camera;

    void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    void Update()
    {
        HandlePan();
        HandleZoom();
    }

    private void HandlePan()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        float x = (keyboard.dKey.isPressed ? 1f : 0f) - (keyboard.aKey.isPressed ? 1f : 0f);
        float y = (keyboard.wKey.isPressed ? 1f : 0f) - (keyboard.sKey.isPressed ? 1f : 0f);

        transform.position += new Vector3(x, y, 0f) * panSpeed * Time.deltaTime;
    }

    private void HandleZoom()
    {
        var mouse = Mouse.current;
        if (mouse == null) return;

        float scroll = mouse.scroll.ReadValue().y;
        if (scroll == 0f) return;

        _camera.orthographicSize = Mathf.Clamp(
            _camera.orthographicSize - scroll * zoomSpeed,
            minZoom,
            maxZoom
        );
    }
}
