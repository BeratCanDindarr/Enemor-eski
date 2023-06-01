using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFlash_MeshRenderer : MonoBehaviour
{
    // The duration of the hit flash effect in seconds
    public float flashDuration = 0.1f;

    // The amount to multiply the alpha of the flash color by
    public float flashAlphaMultiplier = 2.0f;

    // The original colors of each material in the object's mesh renderer
    private Color[][] originalColors;

    // The flash colors of each material in the object's mesh renderer
    private Color[][] flashColors;

    // The time that the hit flash effect started
    private float hitFlashStartTime;

    void Start()
    {
        // Store the original colors of each material in the object's mesh renderer
        MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();
        originalColors = new Color[meshRenderers.Length][];
        flashColors = new Color[meshRenderers.Length][];

        for (int i = 0; i < meshRenderers.Length; i++)
        {
            originalColors[i] = new Color[meshRenderers[i].materials.Length];
            flashColors[i] = new Color[meshRenderers[i].materials.Length];

            for (int j = 0; j < meshRenderers[i].materials.Length; j++)
            {
                originalColors[i][j] = meshRenderers[i].materials[j].color;
                flashColors[i][j] = new Color(1.0f, 1.0f, 1.0f, originalColors[i][j].a * flashAlphaMultiplier);
            }
        }

    }

    void Update()
    {
        // Set the original color of each material in the object's mesh renderer
        MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            float t = (Time.time - hitFlashStartTime) / flashDuration;
            for (int j = 0; j < meshRenderers[i].materials.Length; j++)
            {
                meshRenderers[i].materials[j].color = Color.Lerp(flashColors[i][j], originalColors[i][j], t);
            }
        }
    }

    // Trigger the hit flash effect when the object is hit by a skill or damage
    public void TriggerHitFlash()
    {
        hitFlashStartTime = Time.time;
        MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            for (int j = 0; j < meshRenderers[i].materials.Length; j++)
            {
                meshRenderers[i].materials[j].color = flashColors[i][j];
            }
        }
    }
}
