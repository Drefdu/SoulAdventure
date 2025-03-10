using UnityEngine;
using TMPro;
public class HUD : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public TextMeshProUGUI puntos;
    public GameObject[] vidas;


    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        puntos.text = "0";
    }

    public void desactivarVida(int indice)
    {
        vidas[indice].SetActive(false);
    }

    public void activarVida(int indice)
    {
        vidas[indice].SetActive(true);
    }
}
