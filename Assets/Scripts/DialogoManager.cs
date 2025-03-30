using System.Collections;
using UnityEngine;
using TMPro;

public class DialogoManager : MonoBehaviour
{
    [Header("Referencias UI")]
    public TextMeshProUGUI dialogoTexto; // Texto del diálogo
    public GameObject panelDialogo; // Panel donde se muestra el diálogo

    [Header("Configuración de Diálogos")]
    [TextArea(4, 6)]
    public string[] dialogos; // Lista de diálogos
    private int indice = 0;

    [Header("Efecto de Escritura")]
    public float velocidadEscritura = 0.05f;
    public int frecuenciaSonido = 3; // Cada cuántas letras suena la voz

    [Header("Sonido de Voz")]
    public AudioClip npcVoice; // Sonido de la voz
    private AudioSource audioSource;

    private void Start()
    {
        panelDialogo.SetActive(false); // Oculta el diálogo al inicio
        audioSource = GetComponent<AudioSource>();
    }

    // Método que el Timeline llamará para cambiar el diálogo
    public void MostrarSiguienteDialogo()
    {
        if (!panelDialogo.activeSelf)
        {
            panelDialogo.SetActive(true);
        }

        if (indice < dialogos.Length)
        {
            StopAllCoroutines();
            StartCoroutine(MostrarTextoLetraPorLetra(dialogos[indice]));
            indice++;
        }
        else
        {
            CerrarDialogo();
        }
    }

    // Método para escribir el texto poco a poco con sonido
private IEnumerator MostrarTextoLetraPorLetra(string texto)
{
    dialogoTexto.text = "";
    int charIndex = 0;

    foreach (char letra in texto)
    {
        dialogoTexto.text += letra;

        if (charIndex % frecuenciaSonido == 0 && npcVoice != null)
        {
            audioSource.PlayOneShot(npcVoice);
        }

        charIndex++;
        yield return new WaitForSecondsRealtime(velocidadEscritura);  // Cambié WaitForSeconds a WaitForSecondsRealtime
    }
}


    // Método para cerrar el diálogo cuando termina
    private void CerrarDialogo()
    {
        panelDialogo.SetActive(false);
    }
}
