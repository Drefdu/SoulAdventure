using UnityEngine;

public class GameOverMusicManager : MonoBehaviour
{
    public AudioSource gameOverMusic;   // Música específica del Game Over
    public GameObject gameOverMenu;     // Menú de Game Over
    public GameObject objectToDisable;  // Primer objeto que se desactivará con Game Over
    public GameObject objectToDisable2; // Segundo objeto que se desactivará con Game Over
    public GameObject objectToDisable3; // Tercer objeto que se desactivará con Game Over

    void Update()
    {
        if (gameOverMenu.activeSelf) // Si el Game Over está activo
        {
            if (!gameOverMusic.isPlaying) 
                gameOverMusic.Play(); // Reproduce la música de Game Over

            if (objectToDisable != null)
                objectToDisable.SetActive(false); // Desactiva el primer objeto

            if (objectToDisable2 != null)
                objectToDisable2.SetActive(false); // Desactiva el segundo objeto

            if (objectToDisable3 != null)
                objectToDisable3.SetActive(false); // Desactiva el tercer objeto
        }
        else // Si el Game Over está desactivado
        {
            if (gameOverMusic.isPlaying) 
                gameOverMusic.Stop(); // Detiene la música de Game Over

            if (objectToDisable != null)
                objectToDisable.SetActive(true); // Activa el primer objeto nuevamente

            if (objectToDisable2 != null)
                objectToDisable2.SetActive(true); // Activa el segundo objeto nuevamente

            if (objectToDisable3 != null)
                objectToDisable3.SetActive(true); // Activa el tercer objeto nuevamente
        }
    }
}
