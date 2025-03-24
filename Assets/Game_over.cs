using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverMenu;

    private void Start()
    {
        gameOverMenu.SetActive(false); // Oculta el menú al inicio
    }

    public void ActivarGameOver()
    {
        gameOverMenu.SetActive(true); // Muestra el menú de Game Over
        Time.timeScale = 0f; // Pausa el juego
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
