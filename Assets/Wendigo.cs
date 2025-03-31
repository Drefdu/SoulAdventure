using UnityEngine;
using System.Collections;
using Pathfinding;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine.InputSystem.XR;

public class Wendigo : MonoBehaviour
{
    public int damage = 10;
    public float cooldownAtaque = 2f;
    public AIPath aiPath;
    public float attackRange = 5f;
    public int maxHealth = 100;
    public int currentHealth = 100;
    public HealthBar healthBar;
    public Muros zonaControl;

    private GameObject attackArea = default;
    private bool puedeAtacar = true;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Rigidbody2D rb;

    // Animaciones
    private string currentState = "Wendigo_idle";
    private string ANI_DAMAGE = "Wendigo_damage";
    private string ANI_IDLE = "Wendigo_idle";
    private string ANI_WALKING = "Wendigo_walking";
    private string ANI_ATTACK = "Wendigo_atacking";

    void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        attackArea = transform.GetChild(0).gameObject;

        currentHealth = maxHealth;
        healthBar.SetMaxHelth(maxHealth);
        attackRange = 3f;
        attackArea.SetActive(false);
        handleAnimations(ANI_IDLE);
        aiPath.enabled = false;
        puedeAtacar = false;
        //Invoke("FollowPlayer", 2.5f);
    }

    void Update()
    {
        // Manejo de direcci�n del enemigo
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

            Debug.Log("atacandoooo");
            if (!puedeAtacar)
            {
                return;
            }
            puedeAtacar = false;
            
            handleAnimations(ANI_ATTACK);
            attackArea.SetActive(true);
            aiPath.enabled = false;
            Invoke(nameof(EndAttack), 0.4f);
            Invoke(nameof(FollowPlayer), 1.5f);
        }
    }

    private void EndAttack()
    {
        attackArea.SetActive(false);
        puedeAtacar = false;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        aiPath.enabled = false;
        puedeAtacar = false;

        if (currentHealth <= 0)
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            if (!puedeAtacar) { return; }
            puedeAtacar = false;
            aiPath.enabled = false;
            other.gameObject.GetComponent<CharactertControler>().SetHit(damage);
        }

        Invoke("FollowPlayer", cooldownAtaque);
    }

    private void handleAnimations(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);
        currentState = newState;
    }

    public void FollowPlayer()
    {
        if (currentHealth <= 0) { return; };
        puedeAtacar = true;
        handleAnimations(ANI_WALKING);
        aiPath.enabled = true;    
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
