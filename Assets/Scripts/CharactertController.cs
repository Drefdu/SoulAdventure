using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class CharactertControler : MonoBehaviour
{
    public float velocidad = 10f;
    public GameObject weapon;
    public int maxHealth;
    public int currentHealth;
    public HealthBar healthBar;

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private bool mirandoDerecha = true;
    private Animator animator;
    private GameObject attackArea = default;
    private bool attacking = false;
    private float attackAnimationDuration = 0.15f;
    private float attackCooldown = 0.2f;
    private float attackTimer = 0f;
    private bool puedeMoverse = true;


    private GameOver gameOverMenu;

    private string currentState = "Player_idle";

    // Animaciones
    private string PLAYER_WALKING = "Player_walking";
    private string PLAYER_IDLE = "Player_idle";
    private string PLAYER_DAMAGE = "Player_damage";

    public event EventHandler onBossF;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        attackArea = transform.GetChild(0).gameObject;
        attackArea.SetActive(false);

        currentHealth = maxHealth;
        healthBar.SetMaxHelth(maxHealth);

        gameOverMenu = FindObjectOfType<GameOver>(); // Busca el menú de Game Over en la escena
    }

    void Update()
    {
        if (!puedeMoverse)
            return;

        if (attacking)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackAnimationDuration)
            {
                attacking = false;
                attackArea.SetActive(false);
            }
        }
        else
        {
            attackTimer += Time.deltaTime;
            ProcesarMovimiento();
            if (attackTimer >= attackCooldown && (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire1")))
            {
                Attack();
            }
        }
    }

    void ProcesarMovimiento()
    {
        float axisX = Input.GetAxis("Horizontal");
        float axisY = Input.GetAxis("Vertical");

        if (axisX != 0 || axisY != 0)
        {
            rb.linearVelocity = new Vector2(axisX * velocidad, axisY * velocidad);
            GestionarOrientacion(axisX);
            handleAnimations(PLAYER_WALKING);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            handleAnimations(PLAYER_IDLE);
        }
    }

    void GestionarOrientacion(float inputMovement)
    {
        if ((mirandoDerecha && inputMovement < 0) || (!mirandoDerecha && inputMovement > 0))
        {
            mirandoDerecha = !mirandoDerecha;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }

    private void Attack()
    {
        attacking = true;
        attackTimer = 0f;
        attackArea.SetActive(true);
        weapon.transform.Rotate(new Vector3(0, 0, -80));
        Invoke(nameof(EndAttack), attackAnimationDuration);
    }

    private void EndAttack()
    {
        attackArea.SetActive(false);
        weapon.transform.Rotate(new Vector3(0, 0, 80));
    }

    public void SetHit(int damage)
    {
        puedeMoverse = false;
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            gameOverMenu.ActivarGameOver(); // 🔴 Activa el menú de Game Over en vez de reiniciar la escena
            return;
        }

        handleAnimations(PLAYER_DAMAGE);
        rb.linearVelocity = Vector2.zero;
        StartCoroutine(EsperarYActivarMovimiento());
    }

    IEnumerator EsperarYActivarMovimiento()
    {
        yield return new WaitForSeconds(0.4f);
        handleAnimations(PLAYER_IDLE);
        puedeMoverse = true;
    }

    private void handleAnimations(string newState)
    {
        if (currentState == newState) return;
        animator.Play(newState);
        currentState = newState;
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("La tecla B ha sido presionada y el evento onBossF será invocado.");
            onBossF?.Invoke(this, EventArgs.Empty);
        }
    }
}
