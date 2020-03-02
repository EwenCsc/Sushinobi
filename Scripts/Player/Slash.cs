using UnityEngine;

public class Slash : MonoBehaviour
{
    public Material slashMaterial;
    private Color c1 = Color.yellow;
    private Color c2 = Color.red;
    private GameObject lineGO;
    private LineRenderer lineRenderer;
    private int i = 0;

    void Start()
    {
        lineGO = new GameObject("Line");
        lineGO.AddComponent<LineRenderer>();
        lineRenderer = lineGO.GetComponent<LineRenderer>();
        lineRenderer.material = new Material(slashMaterial);
        lineRenderer.startColor = c1;
        lineRenderer.endColor = c2;
        lineRenderer.startWidth = 0.0f;
        lineRenderer.endWidth = 0.3f;
        lineRenderer.positionCount = 0;
    }

    public void Move(Vector3 pos)
    {
        if (i <= 15)
        {
            lineRenderer.positionCount = i + 1;
            pos = Camera.main.ScreenToWorldPoint(pos);
            pos = new Vector3(pos.x, pos.y, -5);
            lineRenderer.SetPosition(i, pos);
            i++;
        }
        else
        {
            lineRenderer.positionCount = 0;
        }
    }

    public void Destruct()
    {
        lineRenderer.positionCount = 0;
        i = 0;
    }
}
