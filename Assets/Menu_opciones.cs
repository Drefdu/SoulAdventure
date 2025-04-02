using UnityEngine;
using UnityEngine.Audio;

public class Menu_opciones : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    private float volumenActual; // Guarda el volumen actual

    // Para mantener el volumen entre 0 y 1
    private float minVolumen = -80f; // Volumen mínimo (en dB, 0 = volumen silenciado)
    private float maxVolumen = 0f;   // Volumen máximo (en dB, 0 = volumen al máximo)
    
    [SerializeField] private float velocidadCambio = 10f; // Controla la rapidez del cambio de volumen

    private void Update()
    {
        // Obtener la entrada del joystick en el eje horizontal (movimiento hacia la izquierda/derecha)
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput != 0f) // Solo si hay input
        {
            // Cambiar el volumen de forma gradual
            volumenActual += horizontalInput * velocidadCambio * Time.deltaTime; // Usa Time.deltaTime para suavizar el cambio
            CambiarVolumen(volumenActual);
        }
    }

    // Cambia el volumen en el AudioMixer
    public void CambiarVolumen(float volumen)
    {
        // Asegúrate de que el volumen esté dentro del rango válido
        volumenActual = Mathf.Clamp(volumen, minVolumen, maxVolumen);
        audioMixer.SetFloat("Volumen", volumenActual);
    }

    public void CambiarCalidad(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }
}
