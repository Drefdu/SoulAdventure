using UnityEngine;
using TMPro;
using System;
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
        Debug.Log(indice.ToString());
        vidas[indice].SetActive(false);
    }

    public void activarVida(int indice)
    {
        vidas[indice].SetActive(true);
    }

    public void reiniciarVidas()
    {
        foreach (var item in vidas)
        {
            item.SetActive(true);
        }
    }
}
