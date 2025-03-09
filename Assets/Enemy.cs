using UnityEngine;

public class Enemy : MonoBehaviour
{   
    private bool puedeAtacar = true;
    private SpriteRenderer spriteRenderer;
    public float cooldownAtaque;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player")){
            puedeAtacar = false;
            GameManager.Instance.perderVida();

            Color color  = spriteRenderer.color;
            color.a = 0.5f;
            spriteRenderer.color = color;

            other.gameObject.GetComponent<CharactertControler>().SetHit();
        }

        Invoke("ReactivarAtaque", cooldownAtaque);
    }

    void ReactivarAtaque()
    {
        puedeAtacar = true;
        Color color = spriteRenderer.color;
        color.a = 1f;
        spriteRenderer.color = color;
    }
}
