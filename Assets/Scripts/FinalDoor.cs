using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoor : MonoBehaviour
{
    public Transform targetTransform; // The transform to lerp towards
    public float lerpSpeed = 1f; // The speed at which to lerp towards the target position

    private bool isLerping; // Flag to track whether we're currently lerping
    private Vector3 targetPosition; // The target position to lerp towards

    private void Start()
    {
        // Set the target position to the target transform's position, but with the Y value replaced with our own Y value
        targetPosition = new Vector3(transform.position.x, 0.65f, transform.position.z);
    }
    
    void Update()
    {
        // If we're not currently lerping, do nothing
        if (!isLerping) return;

        // Lerp towards the target position in the Y axis only
        transform.position = Vector3.Lerp(transform.position, targetPosition, lerpSpeed * Time.deltaTime);

        // Check if we're within a certain distance of the target position, and if so, stop lerping
        float distance = Vector3.Distance(transform.position, targetPosition);
        if (distance < 0.1f)
        {
            isLerping = false;
        }
    }

    public void StartLerping()
    {
        isLerping = true;
    }


}
