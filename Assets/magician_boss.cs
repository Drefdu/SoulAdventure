    using UnityEngine;
    using System.Collections;
    using System.Collections.Generic;
    using Pathfinding;
    using System;

    public class Magician : MonoBehaviour
    {
        public int damage = 10;
        public float cooldownAtaque;
        public int health = 50;
        public AIPath aiPath;
        public float attackRange = 4f;
        public int maxHealth = 50;
        public int currentHealth;
        public HealthBar healthBar;


        private GameObject attackArea = default;
        private bool puedeAtacar = true;
        private SpriteRenderer spriteRenderer;
        private Animator animator;
        private Rigidbody2D rb;
        public Muros murosController;
            
        // Ataque magico
        public GameObject proyectilPrefab;
        public float tiempoEntreDisparos = 3f;
        private float tiempoUltimoDisparo;
        private bool newShoot = true;

        // Animaciones
        private string currentState = "Magician_idle";

        private string ANI_DAMAGE = "Magician_damage";
        private string ANI_IDLE = "Magician_idle";
        private string ANI_WALKING = "Magician_walking";
        private string ANI_ATACK_1 = "Magician_atack_1";
        private string ANI_ATACK_2 = "Magician_atack_2";
        private string ANI_DIE = "Magician_die";


        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            attackArea = transform.GetChild(0).gameObject;
            currentHealth = maxHealth;
            healthBar.SetMaxHelth(maxHealth);

            attackArea.SetActive(false);
            handleAnimations(ANI_IDLE);
            aiPath.enabled = false;
            Invoke("FollowPlayer", 2.5f);
        }

        void Update()
        {

            if (health <= 0)
            {
                aiPath.enabled = false;
                return;
            };

            // Manejo de direcci�n del enemigo
            if (aiPath.desiredVelocity.x >= 0.01f)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (aiPath.desiredVelocity.x <= -0.01f)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }

            // Verifica si debe ejecutar el ataque de proyectiles
            if (PuedeDisparar() && newShoot)
            {
                StartCoroutine(AtaqueProyectiles());
                return;
            }

            // Movimiento y ataque normal si no est� atacando con proyectiles
            float distanceToPlayer = Vector3.Distance(transform.position, aiPath.destination);
            Debug.Log("Distancia al jugador: " + distanceToPlayer);

            // Si est� en rango, ejecuta otro ataque
            if (distanceToPlayer <= attackRange && puedeAtacar)
            {
                AtackOneLighting();
                puedeAtacar = false;
                Invoke("ReactivarAtaque", cooldownAtaque);
            }
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
                handleAnimations(ANI_DIE);
                StartCoroutine(Die());
                return;
            }
            else
            {
                handleAnimations(ANI_DAMAGE);
                Invoke("FollowPlayer", 0.5f);

            }
        }

        IEnumerator Die()
{
    aiPath.enabled = false;
    puedeAtacar = false;

    // 👉 Desactivar muros si se ha asignado el controlador
    if (murosController != null)
    {
        murosController.DesactivarMuros();
    }

    yield return new WaitForSeconds(2f);
    Destroy(gameObject);
}


        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                puedeAtacar = false;
                handleAnimations(ANI_IDLE);
                aiPath.enabled = false;

                Color color = spriteRenderer.color;
                color.a = 0.5f;
                spriteRenderer.color = color;

                other.gameObject.GetComponent<CharactertControler>().SetHit(damage);
            }

            Invoke("ReactivarAtaque", cooldownAtaque);
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

        //Ataques

        void FollowPlayer()
        {
            if (health <= 0) return;
            handleAnimations(ANI_WALKING);
            aiPath.enabled = true;
            puedeAtacar = true;
        }

        void AtackOneLighting()
        {
            aiPath.enabled = false;  // Detener el movimiento durante el ataque
            handleAnimations(ANI_ATACK_1);  // Reproducir animaci�n de ataque
            attackArea.SetActive(true);  // Activar el �rea de ataque (usando un collider)
            Invoke("DisableAttackArea", 0.5f);  // Desactivar el �rea de ataque despu�s de un tiempo
        }

        void DisableAttackArea()
        {
            attackArea.SetActive(false);
            handleAnimations(ANI_WALKING);
            aiPath.enabled = true;
        }

        // Ataque magico

        private bool PuedeDisparar()
        {
            return Time.time > tiempoUltimoDisparo + 5f; // Espera 5 segundos entre ataques
        }


        private void DispararEnTodasLasDirecciones()
        {
            Vector2[] direcciones = {
                Vector2.up, Vector2.down, Vector2.left, Vector2.right, // 4 direcciones principales
                new Vector2(1, 1), new Vector2(1, -1), new Vector2(-1, 1), new Vector2(-1, -1) // Diagonales
            };

            foreach (Vector2 dir in direcciones)
            {
                GameObject proyectil = Instantiate(proyectilPrefab, transform.position, Quaternion.identity);
                proyectil.GetComponent<MagicBullet>().SetDireccion(dir);
            }
        }

        private void DispararEnCirculo(int cantidad)
        {
            for (int i = 0; i < cantidad; i++)
            {
                float angulo = (360f / cantidad) * i;
                Vector2 direccion = new Vector2(Mathf.Cos(angulo * Mathf.Deg2Rad), Mathf.Sin(angulo * Mathf.Deg2Rad));
                GameObject proyectil = Instantiate(proyectilPrefab, transform.position, Quaternion.identity);
                proyectil.GetComponent<MagicBullet>().SetDireccion(direccion);
            }
        }

        IEnumerator AtaqueProyectiles()
        {
            if (health <= 0)
            {
                yield break;
            } else
            {
                aiPath.enabled = false; // Desactiva movimiento
                newShoot = !newShoot;
                handleAnimations(ANI_ATACK_2); // Activa animaci�n de ataque

                for (int i = 0; i <= 3; i++)
                {
                    DispararEnTodasLasDirecciones();
                    yield return new WaitForSeconds(0.5f);

                }
                DispararEnCirculo(15);
                handleAnimations(ANI_WALKING);
                newShoot = true;
                aiPath.enabled = true;
                tiempoUltimoDisparo = Time.time; // Resetea el temporizador
            }
            
        }


    }
