using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using System;

public class ScareCrow : MonoBehaviour
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

    // Animaciones
    private string currentState = "Scarecrow_idle";

    private string ANI_DAMAGE = "Scarecrow_damage";
    private string ANI_IDLE = "Scarecrow_idle";
    private string ANI_WALKING = "Scarecrow_walking";
   

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


        // Manejo de dirección del enemigo
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }


 
    }


    public void TakeDamage(int damage)
    {

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


}
