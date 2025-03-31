using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    public Transform fillBar; // arrástrale el FillBar desde el editor
    private SpriteRenderer fillRenderer;

    void Start()
    {
        if (fillBar != null)
            fillRenderer = fillBar.GetComponent<SpriteRenderer>();
    }

    public void SetHealth(float normalizedHealth)
    {
        normalizedHealth = Mathf.Clamp01(normalizedHealth); // evita valores inválidos

        fillBar.localScale = new Vector3(normalizedHealth, 1f, 1f);

        // Cambiar color según el porcentaje
        if (fillRenderer != null)
        {
            if (normalizedHealth > 0.5f)
                fillRenderer.color = Color.green;
            else if (normalizedHealth > 0.25f)
                fillRenderer.color = Color.yellow;
            else
                fillRenderer.color = Color.red;
        }
    }
}
