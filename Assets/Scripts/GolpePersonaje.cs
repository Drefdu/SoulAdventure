using UnityEngine;

public class GolpePersonaje : MonoBehaviour
{
    public AudioClip sonidoGolpe;  // El sonido de golpe
    private AudioSource audioSource; // Referencia al AudioSource
    public GameObject objectControl; // El GameObject cuyo estado queremos verificar

    void Start()
    {
        // Obtener el componente AudioSource del personaje
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Verificar si la tecla espacio fue presionada
        if (Input.GetKeyDown(KeyCode.Space)|| Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            // Verificar si el GameObject que controlamos está desactivado
            if (objectControl != null && !objectControl.activeSelf)
            {
                // Reproducir el sonido de golpe solo si el GameObject está desactivado
                audioSource.PlayOneShot(sonidoGolpe);
            }
        }
    }
}
