using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor; // Solo en el editor para permitir arrastrar escenas
#endif

public class CambioEscenaCinematica : MonoBehaviour
{
    public PlayableDirector director;

    #if UNITY_EDITOR
    public SceneAsset escenaDestino; // Arrastrar la escena en el Inspector
    #endif

    [SerializeField] private string nombreEscena; // Guardar el nombre real de la escena

    void Start()
    {
        director.stopped += OnCinematicaFinalizada;
    }

    void OnCinematicaFinalizada(PlayableDirector pd)
    {
        if (!string.IsNullOrEmpty(nombreEscena))
        {
            SceneManager.LoadScene(nombreEscena);
        }
        else
        {
            Debug.LogError("No se ha asignado una escena en el Inspector.");
        }
    }
}
