using UnityEngine;

public class HitFlash : MonoBehaviour
{

    // The duration of the hit flash effect in seconds
    public float flashDuration = 0.1f;

    // The amount to multiply the alpha of the flash color by
    public float flashAlphaMultiplier = 2.0f;

    // The original colors of each material in the enemy's skinned mesh renderer
    private Color[][] originalColors;

    // The flash colors of each material in the enemy's skinned mesh renderer
    private Color[][] flashColors;

    // The time that the hit flash effect started
    private float hitFlashStartTime;

    void Start()
    {
        // Store the original colors of each material in all child skinned mesh renderers
        SkinnedMeshRenderer[] skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        originalColors = new Color[skinnedMeshRenderers.Length][];
        flashColors = new Color[skinnedMeshRenderers.Length][];

        for (int i = 0; i < skinnedMeshRenderers.Length; i++)
        {
            originalColors[i] = new Color[skinnedMeshRenderers[i].materials.Length];
            flashColors[i] = new Color[skinnedMeshRenderers[i].materials.Length];

            for (int j = 0; j < skinnedMeshRenderers[i].materials.Length; j++)
            {
                originalColors[i][j] = skinnedMeshRenderers[i].materials[j].color;
                flashColors[i][j] = new Color(1.0f, 1.0f, 1.0f, originalColors[i][j].a * flashAlphaMultiplier);
            }
        }

    }

    void Update()
    {
        // Set the original color of each material in all child skinned mesh renderers
        SkinnedMeshRenderer[] skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        for (int i = 0; i < skinnedMeshRenderers.Length; i++)
        {
            float t = (Time.time - hitFlashStartTime) / flashDuration;
            for (int j = 0; j < skinnedMeshRenderers[i].materials.Length; j++)
            {
                skinnedMeshRenderers[i].materials[j].color = Color.Lerp(flashColors[i][j], originalColors[i][j], t);
            }
        }
    }

    // Trigger the hit flash effect when the enemy is hit by a skill or damage
    public void TriggerHitFlash()
    {
        hitFlashStartTime = Time.time;
        SkinnedMeshRenderer[] skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        for (int i = 0; i < skinnedMeshRenderers.Length; i++)
        {
            for (int j = 0; j < skinnedMeshRenderers[i].materials.Length; j++)
            {
                skinnedMeshRenderers[i].materials[j].color = flashColors[i][j];
            }
        }
    }

}
