using System.Collections.Generic;
using UnityEngine;

public class Muros : MonoBehaviour
{
    public List<GameObject> muros; // Lista de muros a activar/desactivar

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("🚀 Player ha activado los muros.");
            ActivarMuros();
        }
    }

    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Space))
    //     {
    //         Debug.Log("🔴 Tecla ESPACIO presionada. Desactivando muros.");
    //         DesactivarMuros();
    //     }
    // }

    private void ActivarMuros()
    {
        if (muros == null || muros.Count == 0) return;

        foreach (GameObject muro in muros)
        {
            if (muro != null)
            {
                muro.SetActive(true);
                Debug.Log($"🧱 Muro {muro.name} ha sido activado.");
            }
        }
    }

    public void DesactivarMuros()
    {
        if (muros == null || muros.Count == 0) return;

        foreach (GameObject muro in muros)
        {
            if (muro != null)
            {
                muro.SetActive(false);
                Debug.Log($"❌ Muro {muro.name} ha sido desactivado.");
            }
        }
    }
}
