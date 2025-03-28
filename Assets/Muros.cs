using System.Collections.Generic;
using UnityEngine;

public class Muros : MonoBehaviour
{
    public List<GameObject> muros; // Lista de muros a activar/desactivar
    private bool murosActivados = false; // Evita múltiples activaciones
    private BoxCollider2D boxCollider; // Referencia al BoxCollider2D

    private void Start()
    {
        // Obtiene el componente BoxCollider2D automáticamente
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !murosActivados)
        {
            Debug.Log("🚀 Player ha activado los muros.");
            ActivarMuros();
            murosActivados = true; // Evita que se active más de una vez
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

        // Desactiva el BoxCollider2D cuando los muros se desactivan
        if (boxCollider != null)
        {
            boxCollider.enabled = false;
            Debug.Log("🛑 BoxCollider2D desactivado.");
        }
    }
}
