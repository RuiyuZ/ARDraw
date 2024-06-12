using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    public GameObject linePrefab;
    public Camera arCamera;
    public float drawOffset = 0.5f;
    public float lineWidth = 0.01f;
    public Material[] materials; // Array to hold different materials for the line
    private int materialIndex = 0;

    private LineRenderer currentLine;
    private int pointIndex;
    private bool drawLine;
    private GameObject currentLineObject;
    private List<GameObject> allLines = new List<GameObject>();

    void Start()
    {
        drawLine = false;
    }

    void Update()
    {
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
        currentLineObject = Instantiate(linePrefab);
        currentLineObject.tag = "ARLine";
        currentLine = currentLineObject.GetComponent<LineRenderer>();
        currentLine.startWidth = lineWidth;
        currentLine.endWidth = lineWidth;
        currentLine.useWorldSpace = true;
        currentLine.material = materials[materialIndex]; // Set the current material
        pointIndex = 0;
        drawLine = true;
        allLines.Add(currentLineObject); // Add the line to the list
    }

    void UpdateDrawing(Vector2 inputPosition)
    {
        if (drawLine)
        {
            pointIndex++;
            currentLine.positionCount = pointIndex;
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

    public void ClearLines()
    {
        Debug.Log("Clear Lines button pressed");
        foreach (var line in allLines)
        {
            Destroy(line);
        }
        allLines.Clear();
    }

    public void ChangeLineColor()
    {
        Debug.Log("Change Color button pressed");
        materialIndex = (materialIndex + 1) % materials.Length; // Cycle through materials
    }
}
