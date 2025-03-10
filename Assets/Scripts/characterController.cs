using System.Collections;
using UnityEngine;

public class CharactertControler : MonoBehaviour
{
    public float velocidad = 10f;
    public GameObject weapon;

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


    private string currentState = "Player_idle";

    //Animations

    private string PLAYER_WALKING = "Player_walking";
    private string PLAYER_IDLE = "Player_idle";
    private string PLAYER_DAMAGE = "Player_damage";

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
        } else
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
        attackTimer = 0f; // Reiniciamos el tiempo del ataque
        attackArea.SetActive(true);

        // Rotamos el arma al atacar
        weapon.transform.Rotate(new Vector3(0, 0, -80));


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
