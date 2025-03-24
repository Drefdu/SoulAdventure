using System.Collections.Generic;
using UnityEngine;

public class Muros : MonoBehaviour
{
    public List<GameObject> muros; // Lista de muros a activar

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Verifica si el jugador entra en la zona
        {
            Debug.Log("🚀 Player ha activado los muros.");
            ActivarMuros();
        }
    }

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
}
