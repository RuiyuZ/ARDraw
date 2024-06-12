using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    public GameObject linePrefab;
    public float drawOffset = 0.5f;
    public float lineWidth = 0.01f;
    public Material[] materials; // Array to hold different materials for the line

    private int materialIndex = 0;
    private LineRenderer currentLine;
    private int pointIndex;
    private GameObject currentLineObject;
    private List<GameObject> allLines = new List<GameObject>();

    public void StartDrawing(Vector3 startPosition)
    {
        currentLineObject = Instantiate(linePrefab);
        currentLineObject.tag = "ARLine";
        currentLine = currentLineObject.GetComponent<LineRenderer>();
        currentLine.startWidth = lineWidth;
        currentLine.endWidth = lineWidth;
        currentLine.useWorldSpace = true;
        currentLine.material = materials[materialIndex]; // Set the current material
        pointIndex = 0;
        allLines.Add(currentLineObject); // Add the line to the list
        UpdateDrawing(startPosition);
    }

    public void UpdateDrawing(Vector3 newPosition)
    {
        if (currentLine != null)
        {
            pointIndex++;
            currentLine.positionCount = pointIndex;
            currentLine.SetPosition(pointIndex - 1, newPosition);
        }
    }

    public void EndDrawing()
    {
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
