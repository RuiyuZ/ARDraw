using UnityEngine;

public class LineMaterialSetup : MonoBehaviour
{
    public Material lineMaterial;

    void Start()
    {
        LineRenderer lr = GetComponent<LineRenderer>();
        if (lr != null)
        {
            lr.material = lineMaterial;
        }
    }
}
