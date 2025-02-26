using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // Singleton

    public HUD hud;
    private int vidas = 3;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // Asigna esta instancia
        }
        else
        {
            Destroy(gameObject); // Evita duplicados
            return;
        }

        DontDestroyOnLoad(gameObject); // Mantiene el GameManager entre escenas
    }

    public void perderVida()
    {
        if (vidas == 0)
        {
            SceneManager.LoadScene(0);
            return;
        }
        vidas -= 1;
        hud.desactivarVida(vidas);
    }

    public bool recuperarVida()
    {
        if (vidas == 3)
        {
            return false;
        }
        hud.activarVida(vidas);
        vidas += 1;
        return true;
    }
}
