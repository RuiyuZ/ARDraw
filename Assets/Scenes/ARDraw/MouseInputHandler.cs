using System;
using UnityEngine;

public class MouseInputHandler : MonoBehaviour
{
    public static event Action<Vector2> OnMouseDown;
    public static event Action<Vector2> OnMouseDrag;
    public static event Action OnMouseUp;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseDown?.Invoke(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0))
        {
            OnMouseDrag?.Invoke(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            OnMouseUp?.Invoke();
        }
    }
}
