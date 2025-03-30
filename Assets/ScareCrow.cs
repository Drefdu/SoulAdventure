using UnityEngine;
using System.Collections;
using Pathfinding;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor.Rendering;
using UnityEngine.InputSystem.XR;

public class ScareCrow : MonoBehaviour
{
    public int damage = 10;
    public float cooldownAtaque = 2f;
    public int health = 50;
    public AIPath aiPath;
    public float attackRange = 2f;
    public int maxHealth = 50;
    public int currentHealth;
    public HealthBar healthBar;
    public GameObject weapon;
    public Muros zonaControl;

    private GameObject attackArea = default;
    private bool puedeAtacar = true;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Rigidbody2D rb;
    private Quaternion originalWeaponRotation;

    private float attackAnimationDuration = 0.5f;

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
        attackRange = 2f;
        attackArea.SetActive(false);
        handleAnimations(ANI_IDLE);
        aiPath.enabled = false;
        puedeAtacar = false;
        //Invoke("FollowPlayer", 2.5f);
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

        if ((distanceToPlayer <= attackRange) && (puedeAtacar))
        {
            // Si tienes animación de ataque, puedes usar:
            // handleAnimations(ANI_ATTACK);
            if (!puedeAtacar)
            {
                return;
            }
            puedeAtacar = false;
            handleAnimations(ANI_IDLE);
            attackArea.SetActive(true);
            aiPath.enabled = false;
            weapon.transform.Rotate(new Vector3(0, 0, -80));
            Invoke(nameof(EndAttack), 0.5f);
            Invoke(nameof(FollowPlayer), 1.5f);
        }
    }

    private void EndAttack()
    {
        attackArea.SetActive(false);
        puedeAtacar = false;
        weapon.transform.rotation = originalWeaponRotation;
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
            zonaControl?.NotificarMuerteEnemigo(gameObject);
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

    public void FollowPlayer()
    {
        if (health <= 0) { return; };
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


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
