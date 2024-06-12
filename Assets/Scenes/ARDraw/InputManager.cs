using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Camera arCamera;
    public LineManager lineManager;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    lineManager.StartDrawing(GetWorldPosition(touch.position));
                    break;
                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    lineManager.UpdateDrawing(GetWorldPosition(touch.position));
                    break;
                case TouchPhase.Ended:
                    lineManager.EndDrawing();
                    break;
            }
        }
    }

    Vector3 GetWorldPosition(Vector2 screenPosition)
    {
        return arCamera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, lineManager.drawOffset));
    }
}
