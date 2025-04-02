using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuInicial : MonoBehaviour
{
    [SerializeField] private GameObject panel1; // Panel 1
    [SerializeField] private GameObject panel2; // Panel 2
    [SerializeField] private GameObject botonSeleccionado; // El botón que deseas seleccionar automáticamente

    private bool isInteractingWithButton = false; // Variable para saber si el jugador está interactuando con un botón

    private void Update()
    {
        // Verifica si el Panel 2 está activado
        if (panel2.activeSelf)
        {
            // Verifica si se presiona el botón 1 para jugar cuando Panel 2 está activado
            if (Input.GetKeyDown(KeyCode.JoystickButton0))
            {
                Jugar();
            }

            // Verifica si se presiona el botón 2 para salir cuando Panel 2 está activado
            if (Input.GetKeyDown(KeyCode.JoystickButton1))
            {
                Salir();
            }
        }

        // Verifica si se presiona el botón 3 para activar Panel 1 y desactivar Panel 2
        if (Input.GetKeyDown(KeyCode.JoystickButton2))
        {
            ActivarPanel1YDesactivarPanel2();
        }

        // Si el Panel 1 está activado, al presionar el botón 1, cambia entre los paneles
        if (panel1.activeSelf && Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            ActivarPanel2YDesactivarPanel1();
        }

        // Si el jugador está interactuando con un botón, desactiva la navegación vertical
        if (isInteractingWithButton)
        {
            // Solo permitimos la navegación horizontal
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            if (horizontalInput != 0)
            {
                // Aquí podrías agregar la lógica para mover la selección horizontalmente
                // Ejemplo de lógica para mover la selección de izquierda a derecha
            }
        }
        else
        {
            // Habilitar la navegación vertical normalmente cuando no se está interactuando con un botón
            float verticalInput = Input.GetAxisRaw("Vertical");
            if (verticalInput > 0)
            {
                // Lógica para mover la selección hacia arriba
            }
            else if (verticalInput < 0)
            {
                // Lógica para mover la selección hacia abajo
            }
        }
    }

    // Activa el Panel 1 y desactiva el Panel 2
    public void ActivarPanel1YDesactivarPanel2()
    {
        panel1.SetActive(true);  // Activa Panel 1
        panel2.SetActive(false); // Desactiva Panel 2
        
        // Selecciona el botón automáticamente
        SeleccionarBoton(botonSeleccionado);
    }

    // Activa el Panel 2 y desactiva el Panel 1
    public void ActivarPanel2YDesactivarPanel1()
    {
        panel1.SetActive(false); // Desactiva Panel 1
        panel2.SetActive(true);  // Activa Panel 2
    }

    // Función para Jugar (Cargar la siguiente escena)
    public void Jugar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Carga la siguiente escena
    }

    // Función para Salir del juego
    public void Salir()
    {
        Debug.Log("Salir...");
        Application.Quit(); // Cierra la aplicación
    }

    // Función para seleccionar un botón automáticamente
    private void SeleccionarBoton(GameObject boton)
    {
        EventSystem.current.SetSelectedGameObject(boton);
        isInteractingWithButton = true; // Activar la interacción con el botón
    }

    // Función para desactivar la interacción con los botones
    public void DesactivarInteraccionConBoton()
    {
        isInteractingWithButton = false; // Desactivar la interacción con el botón
    }
}
