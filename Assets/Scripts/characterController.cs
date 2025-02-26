using UnityEngine;

public class CharactertControler : MonoBehaviour
{
    public float velocidad = 10f;
    public float forceHit;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private bool mirandoDerecha = true;
    private Animator animator;
    private GameObject attackArea = default;
    private bool attacking = false;
    private float attackAnimationDuration = 0.15f;
    private float attackCooldown = 0.2f;
    private float attackTimer = 0f;

    // Weapon
    public GameObject weapon;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        attackArea = transform.GetChild(0).gameObject;
        attackArea.SetActive(false);
    }

    void Update()
    {

        if (attacking)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackAnimationDuration)
            {
                // Termina el ataque
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
    }

    void ProcesarMovimiento()
    {
        float inputMovementHorizontal = Input.GetAxis("Horizontal");
        float inputMovementVertical = Input.GetAxis("Vertical");

        rb.linearVelocity = new Vector2(inputMovementHorizontal * velocidad, inputMovementVertical * velocidad);

        animator.SetFloat("MovementX", inputMovementHorizontal);
        animator.SetFloat("MovementY", inputMovementVertical);
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

    private void Attack()
    {
        attacking = true;
        attackTimer = 0f; // Reiniciamos el tiempo del ataque
        attackArea.SetActive(true);

        // Rotamos el arma al atacar
        weapon.transform.Rotate(new Vector3(0, 0, -80));

        //// Ajustamos su posici�n correctamente
        //weapon.transform.localPosition = new Vector3(-0.318f, 0.381f, 0);

        // Desactivamos el ataque despu�s de attackAnimationDuration
        Invoke(nameof(EndAttack), attackAnimationDuration);
    }

    private void EndAttack()
    {
        attackArea.SetActive(false);
        weapon.transform.Rotate(new Vector3(0, 0, 80)); // Volvemos a la rotaci�n original
    }

    public void SetHit()
    {
        Vector2 directionHit;

        if(rb.linearVelocityX > 0)
        {
            directionHit = new Vector2(-1, 1);
        }
        else
        {
            directionHit = new Vector2(1, 1);
        }

        rb.AddForce(directionHit * forceHit);
    }
}
