using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    public int damage = 10;
    public float cooldownAtaque;
    public int health = 30;
    public AIPath aiPath;
    public Muros zonaControl;
    public bool puedeAtacar = true;
    public bool isTakingDamage = false;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Rigidbody2D rb;

    public EnemyHealthBar healthBar; // ← Asigna esto desde el Inspector
    private int maxHealth;

    private string currentState = "Slime_idle";

    private string SLIME_DAMAGE = "Slime_damage";
    private string SLIME_IDLE = "Slime_idle";

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        aiPath.enabled = false;
        puedeAtacar = true;

        maxHealth = health;

        if (healthBar != null)
        {
            healthBar.SetHealth(1f);
        }
    }

    void Update()
    {
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    public void TakeDamage(int damage)
    {
        if (isTakingDamage) return;

        health -= damage;

        if (healthBar != null)
            healthBar.SetHealth((float)health / maxHealth); // actualiza la barra

        handleAnimations(SLIME_DAMAGE);
        aiPath.enabled = false;
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
            aiPath.enabled = true;
            isTakingDamage = false;
            handleAnimations(SLIME_IDLE);
        }
    }

    private void Die()
    {
        // Si prefieres ocultar la barra en lugar de destruirla directamente:
        // if (healthBar != null) healthBar.gameObject.SetActive(false);

        zonaControl?.NotificarMuerteEnemigo(gameObject);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!puedeAtacar) return;

            puedeAtacar = false;
            aiPath.enabled = false;

            Color color = spriteRenderer.color;
            color.a = 0.5f;
            spriteRenderer.color = color;

            other.gameObject.GetComponent<CharactertControler>().SetHit(damage);

            Invoke("ReactivarAtaque", cooldownAtaque);
        }
    }

    void ReactivarAtaque()
    {
        puedeAtacar = true;
        aiPath.enabled = true;

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

    public void ActivateMovement()
    {
        aiPath.enabled = true;
    }
}
