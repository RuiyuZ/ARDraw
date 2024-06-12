using UnityEngine;

public class UIManager : MonoBehaviour
{
    public LineManager lineManager;

    public void OnClearLinesButtonPressed()
    {
        lineManager.ClearLines();
    }

    public void OnChangeColorButtonPressed()
    {
        lineManager.ChangeLineColor();
    }
}
