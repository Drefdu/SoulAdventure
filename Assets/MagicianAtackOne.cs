using UnityEngine;

public class MagicianAtackOne : MonoBehaviour
{
    public int damage = 40;

    
    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<CharactertControler>().SetHit(damage);
        }

    }
}
