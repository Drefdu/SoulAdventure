using System.Collections;
using UnityEngine;

public class CharactertControler : MonoBehaviour
{
    public float velocidad = 10f;
<<<<<<< HEAD
    //public float jumpForce;
    //public LayerMask capaSuelo;
=======
    public GameObject weapon;

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


    private string currentState = "Player_idle";

    //Animations

    private string PLAYER_WALKING = "Player_walking";
    private string PLAYER_IDLE = "Player_idle";
    private string PLAYER_DAMAGE = "Player_damage";
>>>>>>> main

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
<<<<<<< HEAD
        ProcesarMovimiento();
        //HandleJump();
=======

        if (!puedeMoverse)
        {
            return;
        }


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
            attackTimer += Time.deltaTime; // Contamos el cooldown
            ProcesarMovimiento();
            if (attackTimer >= attackCooldown && Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
            }
        }
>>>>>>> main
    }

    void ProcesarMovimiento()
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
        float axisX = Input.GetAxis("Horizontal");
        float axisY = Input.GetAxis("Vertical");

        if (axisX != 0 || axisY != 0)
        {
            rb.linearVelocity = new Vector2(axisX * velocidad, axisY * velocidad);
            GestionarOrientacion(axisX);
            handleAnimations(PLAYER_WALKING);
        } else
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

        Invoke(nameof(EndAttack), attackAnimationDuration);
    }

    private void EndAttack()
    {
        attackArea.SetActive(false);
        weapon.transform.Rotate(new Vector3(0, 0, 80)); // Volvemos a la rotaci�n original
    }

    public void SetHit()
    {
        puedeMoverse = false;
        handleAnimations(PLAYER_DAMAGE);
        rb.linearVelocity = Vector2.zero;
        StartCoroutine(EsperarYActivarMovimiento());
       
    }

    IEnumerator EsperarYActivarMovimiento()
    {
        yield return new WaitForSeconds(1f);
        handleAnimations(PLAYER_IDLE);
        puedeMoverse = true;
    }

    private void handleAnimations(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
    }
}
>>>>>>> main
