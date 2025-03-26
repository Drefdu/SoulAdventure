using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioDeMapa : MonoBehaviour
{
    public string nombreEscenaDestino;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(nombreEscenaDestino);
        }
    }
}

