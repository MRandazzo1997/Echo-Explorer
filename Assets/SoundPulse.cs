using UnityEngine;
using System.Collections.Generic;
using System;

public class SoundPulse : MonoBehaviour
{
    public float maxRadius = 10f;  // Maximum distance the sound travels
    public float expansionSpeed = 5f; // How fast the pulse expands
    public LayerMask obstacleMask; // Defines what objects the sound interacts with

    private float currentRadius = 0f;
    private bool isExpanding = false;

    void Update()
    {
        if (isExpanding)
        {
            currentRadius += expansionSpeed * Time.deltaTime;

            // Check for obstacles hit by the sound
            DetectSurfaces();

            // Stop expansion once it reaches max radius
            if (currentRadius >= maxRadius)
            {
                isExpanding = false;
                currentRadius = 0f;
            }
        }

        // Trigger a sound pulse manually with Space key
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartPulse();
        }
    }

    void StartPulse()
    {
        isExpanding = true;
        currentRadius = 0f;

        Debug.Log("Sound started");
    }

    void DetectSurfaces()
    {
        //Collider[] hitColliders = Physics.OverlapSphere(transform.position, currentRadius, obstacleMask);
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), currentRadius, obstacleMask);

        foreach (Collider2D hit in hitColliders)
        {
            Debug.Log("Sound hit: " + hit.gameObject.name); // For testing
            // Later, we'll handle reflections, absorption, etc.
        }
    }
}
