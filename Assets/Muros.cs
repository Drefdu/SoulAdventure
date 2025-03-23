using System;
using System.Collections.Generic;
using UnityEngine;

public class Muros : MonoBehaviour
{
    public List<GameObject> muros;
    private CharactertControler charactertControler;

    private void OnEnable()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            charactertControler = player.GetComponent<CharactertControler>();
            if (charactertControler != null)
            {
                charactertControler.onBossF += Activar;
            }
        }
    }

    private void OnDisable()
    {
        if (charactertControler != null)
        {
            charactertControler.onBossF -= Activar;
        }
    }

    private void Activar(object sender, EventArgs e)
    {
        if (muros == null || muros.Count == 0) return;

        foreach (GameObject muro in muros)
        {
            if (muro != null)
            {
                muro.SetActive(!muro.activeSelf);
            }
        }
    }
}
