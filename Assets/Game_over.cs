using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverMenu; // Asigna el panel del menú de Game Over en el Inspector
    private bool isGameOver = false; // Controla si el personaje está muerto

    void Start()
    {
        gameOverMenu.SetActive(false); // Oculta el menú al inicio
    }

    void Update()
    {
        // Simula la muerte del personaje con la tecla "G" (solo para pruebas)
        if (Input.GetKeyDown(KeyCode.G))
        {
            ActivarGameOver();
        }
    }

    // Llama a esta función cuando el personaje muera
    public void ActivarGameOver()
    {
        isGameOver = true;
        gameOverMenu.SetActive(true); // Muestra el menú de Game Over
        Time.timeScale = 0f; // Pausa el juego
    }

    // Método para reiniciar el nivel actual
    public void ReiniciarNivel()
    {
        Time.timeScale = 1f; // Reactiva el juego
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Método para ir al menú principal
public void IrAlMenu()
{
    Time.timeScale = 1f; // Reactiva el juego
    SceneManager.LoadScene(0); // Siempre carga la escena con índice 0 (Menú Principal)
}

}
