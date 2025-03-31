using UnityEngine;

public class weaponAtack : MonoBehaviour
{
    public int damage = 10;

    private void OnCollisionEnter2D(Collision2D other)
    {   
        Debug.Log(other.gameObject.ToString());

        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hola");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hola desde el trigger 2d");

        Debug.Log(collision.gameObject.CompareTag("Enemy"));

        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        }

        if (collision.gameObject.CompareTag("Magician"))
        {
            collision.gameObject.GetComponent<Magician>().TakeDamage(damage);
        }

        if (collision.gameObject.CompareTag("Scarecrow"))
        {
            collision.gameObject.GetComponent<ScareCrow>().TakeDamage(damage);
        }

        if (collision.gameObject.CompareTag("Wendigo"))
        {
            collision.gameObject.GetComponent<Wendigo>().TakeDamage(damage);
        }
    }
}
