using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class CharactertControler : MonoBehaviour
{
    public float velocidad = 10f;
<<<<<<< HEAD
    //public float jumpForce;
    //public LayerMask capaSuelo;
=======
    public GameObject weapon;
    public int maxHealth;
    public int currentHealth;
    public HealthBar healthBar;

>>>>>>> main
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private bool mirandoDerecha = true;
    private Animator animator;
<<<<<<< HEAD
=======
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
>>>>>>> main

    public event EventHandler onBossF;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
<<<<<<< HEAD:Assets/Scripts/characterController.cs
=======
        attackArea = transform.GetChild(0).gameObject;
        attackArea.SetActive(false);

        currentHealth = maxHealth;
        healthBar.SetMaxHelth(maxHealth);

        gameOverMenu = FindObjectOfType<GameOver>(); // Busca el menú de Game Over en la escena
>>>>>>> main:Assets/Scripts/CharactertController.cs
    }

    void Update()
    {
<<<<<<< HEAD:Assets/Scripts/characterController.cs
<<<<<<< HEAD
        ProcesarMovimiento();
        //HandleJump();
=======

=======
>>>>>>> main:Assets/Scripts/CharactertController.cs
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
            if (attackTimer >= attackCooldown && Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
            }
        }
>>>>>>> main
    }

    void ProcesarMovimiento()
<<<<<<< HEAD:Assets/Scripts/characterController.cs
    {       

<<<<<<< HEAD
        if (inputMovementHorizontal != 0f && inputMovementVertical == 0)
        {
            animator.SetBool("isRunning", true);
            animator.SetBool("isMoving", true);
            animator.SetBool("walkingUp", false);
            animator.SetBool("walkingDawn", false);
        }
        else if (inputMovementHorizontal == 0f && inputMovementVertical > 0f)
        {
            animator.SetBool("walkingUp", true);
            animator.SetBool("isMoving", true);
            animator.SetBool("isRunning", false);
            animator.SetBool("walkingDawn", false);
        }
        else if (inputMovementHorizontal == 0f && inputMovementVertical < 0f)
        {
            animator.SetBool("walkingDawn", true);
            animator.SetBool("isMoving", true);
            animator.SetBool("isRunning", false);
            animator.SetBool("walkingUp", false);
        }
        else if  (inputMovementHorizontal == 0f && inputMovementVertical == 0f) 
        {
            animator.SetBool("isMoving", false);
            animator.SetBool("isRunning", false);
            animator.SetBool("walkingUp", false);
            animator.SetBool("walkingDawn", false);
        }



        

        rb.linearVelocity = new Vector2(inputMovementHorizontal * velocidad, inputMovementVertical * velocidad);
        
        GestionarOrientacion(inputMovementHorizontal);
=======
=======
    {
>>>>>>> main:Assets/Scripts/CharactertController.cs
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
>>>>>>> main
    }

    void GestionarOrientacion(float inputMovement)
    {
        if ((mirandoDerecha && inputMovement < 0) || (!mirandoDerecha && inputMovement > 0))
        {
            mirandoDerecha = !mirandoDerecha;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }

<<<<<<< HEAD:Assets/Scripts/characterController.cs
    //void HandleJump()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space) && IsInTheGround())
    //    {
    //        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    //    }

    //    if(Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0)
    //    {
    //        rb.linearVelocity = new Vector2(rb.linearVelocityX, rb.linearVelocityY * 0.5f);
    //    }
    //} 

<<<<<<< HEAD
    //bool IsInTheGround()
    //{
    //    RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, new Vector2(boxCollider.bounds.size.x, boxCollider.bounds.size.y), 0f, Vector2.down, 0.2f, capaSuelo);
    //    return raycastHit.collider != null;
    //}
}
=======

=======
    private void Attack()
    {
        attacking = true;
        attackTimer = 0f;
        attackArea.SetActive(true);
        weapon.transform.Rotate(new Vector3(0, 0, -80));
>>>>>>> main:Assets/Scripts/CharactertController.cs
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
>>>>>>> main
