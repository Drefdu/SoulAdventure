using UnityEngine;
using UnityEngine.SceneManagement;

public class menu_pausa : MonoBehaviour
{
    [SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject menuPausa;

    private bool juegoP = false;

    private void Update()
    {
        // Verifica si se presiona el botón 9 para pausar
        if (Input.GetKeyDown(KeyCode.JoystickButton9) && !juegoP)
        {
            Pausa();
        }

        // Solo permite ejecutar las siguientes acciones si el menú de pausa está activo
        if (menuPausa.activeSelf)
        {
            // Verifica si se presiona el botón 0 para reanudar
            if (Input.GetKeyDown(KeyCode.JoystickButton0))
            {
                Reanudar();
            }

            // Verifica si se presiona el botón 1 para reiniciar el nivel
            if (Input.GetKeyDown(KeyCode.JoystickButton1))
            {
                Reiniciar();
            }

            // Verifica si se presiona el botón 2 para cerrar el juego
            if (Input.GetKeyDown(KeyCode.JoystickButton2))
            {
                Cerrar();
            }
        }

        // También puedes usar la tecla Escape para pausar o reanudar
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegoP)
            {
                Reanudar();
            }
            else
            {
                Pausa();
            }
        }
    }

    public void Pausa()
    {
        Time.timeScale = 0f;
        juegoP = true;
        botonPausa.SetActive(false);
        menuPausa.SetActive(true);
    }

    public void Reanudar()
    {
        Time.timeScale = 1f;
        juegoP = false;
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);
    }

    public void Reiniciar()
    {
        juegoP = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reinicia el nivel
    }

    public void Cerrar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1); // Vuelve al menú principal
    }
}
