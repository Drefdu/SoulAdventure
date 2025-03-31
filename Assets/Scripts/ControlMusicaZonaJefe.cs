using UnityEngine;

public class ControlMusicaZonaJefe : MonoBehaviour
{
    public GameObject musicaOriginalGO;  // El GameObject que tiene el AudioSource de la música original
    public GameObject musicaJefeFinalGO; // El GameObject que tiene el AudioSource de la música del jefe final
    public GameObject jefeFinal;         // El GameObject del jefe final

    private AudioSource musicaOriginalAS;  // Referencia al AudioSource de la música original
    private AudioSource musicaJefeFinalAS; // Referencia al AudioSource de la música del jefe final

    void Start()
    {
        // Obtener los componentes AudioSource de los GameObjects
        musicaOriginalAS = musicaOriginalGO.GetComponent<AudioSource>();
        musicaJefeFinalAS = musicaJefeFinalGO.GetComponent<AudioSource>();

        // Asegurarse de que la música original se esté reproduciendo al inicio
        musicaOriginalAS.Play();
        musicaJefeFinalAS.Stop();  // Asegurarse de que la música del jefe final no se reproduce al inicio
    }

    void Update()
    {
        // Si el jefe final está deshabilitado, volver a la música original
        if (jefeFinal != null && !jefeFinal.activeSelf)
        {
            if (!musicaOriginalAS.isPlaying) // Si no está sonando la música original, comenzamos a reproducirla
            {
                musicaOriginalAS.Play();
                musicaJefeFinalAS.Stop();
            }
        }
    }

    // Este método se llama cuando otro GameObject entra en la zona del trigger
    void OnTriggerEnter(Collider other)
    {
        // Si el jugador entra en la zona del jefe, cambiar a la música del jefe
        if (other.CompareTag("Player")) // Asegúrate de que el jugador tiene la etiqueta "Player"
        {
            musicaOriginalAS.Stop();  // Detener la música original
            musicaJefeFinalAS.Play(); // Reproducir la música del jefe final
        }
    }

    // Este método se llama cuando otro GameObject sale de la zona del trigger
    void OnTriggerExit(Collider other)
    {
        // Si el jugador sale de la zona, volver a la música original
        if (other.CompareTag("Player"))
        {
            musicaJefeFinalAS.Stop();  // Detener la música del jefe final
            musicaOriginalAS.Play();  // Reproducir la música original
        }
    }
}
