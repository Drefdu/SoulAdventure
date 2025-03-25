using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverMenu; // Panel de Game Over
    public GameObject[] elementosADesactivar; // Lista de objetos a ocultar

    private void Start()
    {
        gameOverMenu.SetActive(false); // Oculta el menú al inicio
    }

    public void ActivarGameOver()
    {
        gameOverMenu.SetActive(true); // Muestra el menú de Game Over
        Time.timeScale = 0f; // Pausa el juego

        // Desactiva todos los elementos debajo de Game_Over
        foreach (GameObject elemento in elementosADesactivar)
        {
            if (elemento != null)
            {
                elemento.SetActive(false);
            }
        }
    }

    public void ReiniciarNivel()
    {
        Time.timeScale = 1f; // Reactiva el juego
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void IrAlMenu()
    {
        Time.timeScale = 1f; // Reactiva el juego
        SceneManager.LoadScene(0); // Carga la escena con índice 0 (Menú Principal)
    }
}
