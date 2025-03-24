using UnityEngine;

public class MagicBullet : MonoBehaviour
{
    public float velocidad = 7f;
    public int damage = 10;
    public float tiempoDeVida = 3f; // Tiempo antes de que se destruya
    private Vector2 direccion;

    public void SetDireccion(Vector2 nuevaDireccion)
    {
        direccion = nuevaDireccion.normalized;
    }

    void Start()
    {
        Destroy(gameObject, tiempoDeVida); // Se autodestruye después del tiempo indicado
    }

    void Update()
    {
        transform.position += (Vector3)direccion * velocidad * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<CharactertControler>().SetHit(damage);
        }
    }


}
