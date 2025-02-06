using UnityEngine;

public class SoundPulse : MonoBehaviour
{
    public float maxRadius = 10f;
    public float expansionSpeed = 5f;
    public LayerMask obstacleMask;
    public Color pulseColor;

    private float currentRadius = 0f;
    private bool isExpanding = false;
    public Transform pulseVisual; // Reference to the visual effect

    private void Start()
    {
        pulseVisual.gameObject.GetComponent<MeshRenderer>().material.color = pulseColor;
    }

    void Update()
    {
        if (isExpanding)
        {
            currentRadius += expansionSpeed * Time.deltaTime;
            pulseVisual.gameObject.GetComponent<MeshRenderer>().material.SetFloat("_Radius", currentRadius);
            pulseVisual.localScale = Vector3.one * currentRadius * 2f; // Scale the visual

            DetectSurfaces();

            if (currentRadius >= maxRadius)
            {
                isExpanding = false;
                currentRadius = 0f;
                pulseVisual.gameObject.SetActive(false); // Hide after finishing
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartPulse();
        }
    }

    void StartPulse()
    {
        isExpanding = true;
        currentRadius = 0f;
        pulseVisual.gameObject.GetComponent<MeshRenderer>().material.SetVector("_Pulse_Origin", pulseVisual.position);
        pulseVisual.gameObject.SetActive(true);
    }

    void DetectSurfaces()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, currentRadius, obstacleMask);

        foreach (Collider2D hit in hitColliders)
        {
            Debug.Log("Sound hit: " + hit.gameObject.name);
        }
    }
}
