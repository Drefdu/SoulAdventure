using UnityEngine;
using UnityEngine.Audio;

public class Menu_opciones : MonoBehaviour
{

    [SerializeField] private AudioMixer audioMixer;
    public void CambiarVolumen(float volumen){
        audioMixer.SetFloat("Volumen",volumen);

    }

    public void CambiarCalidad(int index){
        QualitySettings.SetQualityLevel(index);
    }
}
