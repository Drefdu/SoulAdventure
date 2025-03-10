using UnityEngine;

public class Enemy : MonoBehaviour
{   
    private bool puedeAtacar = true;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public float cooldownAtaque;
    public int health = 30;
    private Rigidbody2D rb;



    private string currentState = "Slime_idle";

    private string SLIME_DAMAGE = "Slime_damage";
    private string SLIME_IDLE = "Slime_idle";


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        handleAnimations(SLIME_DAMAGE);
        rb.linearVelocity = Vector2.zero;
        Invoke("ResolveHealth", 0.5f);

        
    }

    private void ResolveHealth()
    {
        if (health <= 0)
        {
            Die();
        } 
        else
        {
            handleAnimations(SLIME_IDLE);
        }
    }

    private void Die()
    {
        Destroy(gameObject); // Destruye al enemigo
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

    private void handleAnimations(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
    }
}
