using UnityEngine;

public class CharactertControler : MonoBehaviour
{
    public float velocidad = 10f;
    //public float jumpForce;
    //public LayerMask capaSuelo;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private bool mirandoDerecha = true;
    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        ProcesarMovimiento();
        //HandleJump();
    }

    void ProcesarMovimiento()
    {
        float inputMovementHorizontal = Input.GetAxis("Horizontal");
        float inputMovementVertical = Input.GetAxis("Vertical");

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

    //bool IsInTheGround()
    //{
    //    RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, new Vector2(boxCollider.bounds.size.x, boxCollider.bounds.size.y), 0f, Vector2.down, 0.2f, capaSuelo);
    //    return raycastHit.collider != null;
    //}
}