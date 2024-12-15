using UnityEngine;

public class TargetingArrow : MonoBehaviour
{
    public static TargetingArrow Instance { get; private set; }

    private LineRenderer lineRenderer;
    public GameObject arrowheadInstance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        lineRenderer = GetComponent<LineRenderer>();
        arrowheadInstance.SetActive(false);
    }

    public void UpdateArrow(Vector3 start, Vector3 end)
    {
        // Calculate the mid-point for the curve
        Vector3 midPoint = (start + end) / 2 + Vector3.up * Vector3.Distance(start, end) * 0.5f;

        // Set positions for the curve
        lineRenderer.positionCount = 50; // Higher value = smoother curve
        for (int i = 0; i < 50; i++)
        {
            float t = i / 49f; // Normalize between 0 and 1
            Vector3 position = Mathf.Pow(1 - t, 2) * start +
                               2 * (1 - t) * t * midPoint +
                               Mathf.Pow(t, 2) * end; // Quadratic Bézier formula
            lineRenderer.SetPosition(i, position);
        }

        // Position and rotate the arrowhead
        arrowheadInstance.SetActive(true);
        arrowheadInstance.transform.position = end;
        arrowheadInstance.transform.rotation = Quaternion.LookRotation(Vector3.forward, end - midPoint);
    }

    public void HideArrow()
    {
        lineRenderer.positionCount = 0;
        arrowheadInstance.SetActive(false);
    }
}