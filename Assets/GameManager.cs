using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

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

        if (hud == null)
        {
            hud = FindObjectOfType<HUD>();
        }
    }

    public void perderVida()
    {
        vidas -= 1;
        Debug.Log(vidas);
        hud.desactivarVida(vidas);

        if (vidas == 0)
        {
            SceneManager.LoadScene(0);
            hud.reiniciarVidas();
            vidas = 3;
            return;
        }
        
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
