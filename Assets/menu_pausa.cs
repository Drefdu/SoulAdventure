using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class menu_pausa : MonoBehaviour
{
    [SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject menuPausa;

    private bool juegoP = false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(juegoP){
                Reanudar();
            }else{
                Pausa();
            }
        }
    }

    public void Pausa(){
        Time.timeScale = 0f;
        juegoP = true;
        botonPausa.SetActive(false);
        menuPausa.SetActive(true);
    }

    public void Reanudar(){
        Time.timeScale = 1f;
        juegoP = false;
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);

    }

    public void Reiniciar(){
        juegoP = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    public void Cerrar(){
        Debug.Log("cerrando");
        Application.Quit(); 
    }
}
