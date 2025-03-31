using UnityEngine;

public class MusicZone : MonoBehaviour
{
    public AudioSource mainMusic;  // Música del mapa principal
    public AudioSource areaMusic;  // Música dentro del área azul

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Verifica si el objeto que entra es el personaje
        {
            mainMusic.Stop();    // Detiene la música principal
            areaMusic.Play();    // Activa la música del área azul
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Verifica si el objeto que sale es el personaje
        {
            areaMusic.Stop();    // Detiene la música del área azul
            mainMusic.Play();    // Reanuda la música principal
        }
    }
}
