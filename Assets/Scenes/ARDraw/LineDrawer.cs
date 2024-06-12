using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARDraw : MonoBehaviour
{
    public GameObject linePrefab; // Prefab with a LineRenderer component
    public Camera arCamera; // The AR camera
    public float drawOffset = 0.5f; // Distance in front of the camera to draw
    public float lineWidth = 0.01f; // Width of the line

    private LineRenderer currentLine;
    private int pointIndex;
    private bool drawLine;
    private GameObject currentLineObject;

    void Start()
    {
        drawLine = false;
    }

    void Update()
    {
        // Handle touch input for drawing in AR
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    StartDrawing(touch.position);
                    break;
                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    UpdateDrawing(touch.position);
                    break;
                case TouchPhase.Ended:
                    EndDrawing();
                    break;
            }
        }
    }

    void StartDrawing(Vector2 inputPosition)
    {
        // Instantiate a new line object and initialize the LineRenderer
        currentLineObject = Instantiate(linePrefab);
        currentLineObject.tag = "ARLine";
        currentLine = currentLineObject.GetComponent<LineRenderer>();
        currentLine.startWidth = lineWidth;
        currentLine.endWidth = lineWidth;
        currentLine.useWorldSpace = true;
        pointIndex = 0;
        drawLine = true;
    }

    void UpdateDrawing(Vector2 inputPosition)
    {
        if (drawLine)
        {
            pointIndex++;
            currentLine.positionCount = pointIndex;
            // Convert screen point to world point and add it to the LineRenderer
            Vector3 newPoint = arCamera.ScreenToWorldPoint(new Vector3(inputPosition.x, inputPosition.y, drawOffset));
            currentLine.SetPosition(pointIndex - 1, newPoint);
        }
    }

    void EndDrawing()
    {
        drawLine = false;
        currentLine = null;
        currentLineObject = null;
    }
}
