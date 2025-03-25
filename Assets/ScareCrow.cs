using UnityEngine;
using System.Collections;
using Pathfinding;

public class ScareCrow : MonoBehaviour
{
    public int damage = 10;
    public float cooldownAtaque;
    public int health = 50;
    public AIPath aiPath;
    public float attackRange = 2f;
    public int maxHealth = 50;
    public int currentHealth;
    public HealthBar healthBar;
    public GameObject weapon;

    private GameObject attackArea = default;
    private bool puedeAtacar = false;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Rigidbody2D rb;
    private Quaternion originalWeaponRotation;

    private float attackAnimationDuration = 1.9f;

    // Animaciones
    private string currentState = "Scarecrow_idle";
    private string ANI_DAMAGE = "Scarecrow_damage";
    private string ANI_IDLE = "Scarecrow_idle";
    private string ANI_WALKING = "Scarecrow_walking";
    // private string ANI_ATTACK = "Scarecrow_attack"; // Si tienes animación de ataque

    void Start()
    {   

        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        attackArea = transform.GetChild(0).gameObject;
        originalWeaponRotation = weapon.transform.rotation;

        currentHealth = maxHealth;
        healthBar.SetMaxHelth(maxHealth);

        attackArea.SetActive(false);
        handleAnimations(ANI_IDLE);
        aiPath.enabled = false;
        Invoke("FollowPlayer", 2.5f);
    }

    void Update()
    {
        // Manejo de dirección del enemigo
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        float distanceToPlayer = Vector3.Distance(transform.position, aiPath.destination);

        if (distanceToPlayer <= attackRange && puedeAtacar)
        {
            // Si tienes animación de ataque, puedes usar:
            // handleAnimations(ANI_ATTACK);

            attackArea.SetActive(true);
            aiPath.enabled = false;
            weapon.transform.Rotate(new Vector3(0, 0, -80));
            handleAnimations(ANI_IDLE);
            Invoke(nameof(EndAttack), attackAnimationDuration);
            puedeAtacar = false;
            Invoke(nameof(ReactivarAtaque), cooldownAtaque);
        }
    }

    private void EndAttack()
    {
        attackArea.SetActive(false);
        weapon.transform.rotation = originalWeaponRotation;
        aiPath.enabled = true;
        handleAnimations(ANI_WALKING);
        // handleAnimations(ANI_IDLE); // o volver a idle después del ataque
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        aiPath.enabled = false;
        puedeAtacar = false;

        if (health <= 0)
        {
            Destroy(gameObject);
            //StartCoroutine(Die());
            return;
        }
        else
        {
            handleAnimations(ANI_DAMAGE);
            Invoke(nameof(FollowPlayer), 0.5f);
        }
    }

    private void handleAnimations(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);
        currentState = newState;
    }

    void FollowPlayer()
    {
        if (health <= 0) return;
        handleAnimations(ANI_WALKING);
        aiPath.enabled = true;
        puedeAtacar = true;
    }

    IEnumerator Die()
    {
        aiPath.enabled = false;
        puedeAtacar = false;
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (attackArea.activeInHierarchy && other.CompareTag("Player"))
    //    {
    //        other.GetComponent<CharactertControler>().SetHit(damage);
    //    }
    //}

    //private void OnCollisionEnter2D(Collision2D other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        puedeAtacar = false;
    //        handleAnimations(ANI_IDLE);
    //        aiPath.enabled = false;

    //        Color color = spriteRenderer.color;
    //        color.a = 0.5f;
    //        spriteRenderer.color = color;

    //        other.gameObject.GetComponent<CharactertControler>().SetHit(damage);
    //    }

    //    Invoke(nameof(ReactivarAtaque), cooldownAtaque);
    //}

    void ReactivarAtaque()
    {
        puedeAtacar = true;
        aiPath.enabled = true;
        Color color = spriteRenderer.color;
        color.a = 1f;
        spriteRenderer.color = color;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
