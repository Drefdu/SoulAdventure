using UnityEngine;
using UnityEngine.SceneManagement;

public class ActivarCinematica : MonoBehaviour
{
    public string nombreEscenaCinematica = "Cinematica";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(nombreEscenaCinematica);
        }
    }
}

