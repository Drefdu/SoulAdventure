using UnityEngine;

public class MagicBullet : MonoBehaviour
{
    [SerializeField] private float velocidad = 7f;
    [SerializeField] private int damage = 10;
    [SerializeField] private float tiempoDeVida = 3f;

    private Vector2 direccion;
    private Rigidbody2D rb;

    public void SetDireccion(Vector2 nuevaDireccion)
    {
        direccion = nuevaDireccion.normalized;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("❌ No se encontró Rigidbody2D en la bala.");
            Destroy(gameObject); // Destruye la bala si no tiene Rigidbody2D
            return;
        }

        Destroy(gameObject, tiempoDeVida); // Se autodestruye después del tiempo indicado
    }

    void FixedUpdate()
    {
        if (rb != null)
        {
            rb.linearVelocity = direccion * velocidad; // Cambio de velocity a linearVelocity
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent(out CharactertControler player))
            {
                player.SetHit(damage);
            }
            Destroy(gameObject);
        }
        else if (other.CompareTag("Muros_S")) // Colisión con muros
        {
            Debug.Log("🧱 Bala impactó contra un muro y será destruida.");
            Destroy(gameObject);
        }
    }
}
