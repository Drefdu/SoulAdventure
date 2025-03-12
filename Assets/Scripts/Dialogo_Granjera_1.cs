using System.Collections;
using UnityEngine;
using TMPro;

public class Dialogo_Granjera_1 : MonoBehaviour
{
        [SerializeField] private GameObject dialoguePanel;
        [SerializeField] private AudioClip npcVoice;
        [SerializeField] private float typingTime;
        [SerializeField] private int TimeChatSong;

        [SerializeField] private TMP_Text dialogueText;
        [SerializeField, TextArea(4,6)] private string[] dialogueLines;
        
    private bool isPlayerInRange;
    private bool didDialogoStart;
    private int lineIndex;

    
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = npcVoice;
    
    }


    // Update is called once per frame
    void Update()
    {
       if(isPlayerInRange && Input.GetKeyDown(KeyCode.Space)){
        if(!didDialogoStart){
            Startdialogue();
        }
        else if(dialogueText.text == dialogueLines[lineIndex])
        {
           NexDialogueLine();
        }
        else{
            StopAllCoroutines();
            dialogueText.text=dialogueLines[lineIndex];

        }
        
       } 
    }
    
    private void Startdialogue(){
        didDialogoStart =true;
        dialoguePanel.SetActive(true);
        lineIndex=0;
        Time.timeScale =0f;
        StartCoroutine(ShowLine());
        
    }

    private void NexDialogueLine(){
        lineIndex++;
       if(lineIndex < dialogueLines.Length)
       {
        StartCoroutine(ShowLine());

       }
       else{
         didDialogoStart = false;
         dialoguePanel.SetActive(false);
         Time.timeScale = 1f;
       } 
    }

    private IEnumerator ShowLine(){
        dialogueText.text = string.Empty;
        int charIndex = 0;

        foreach(char ch in dialogueLines[lineIndex]){
            dialogueText.text += ch;
            if(charIndex % TimeChatSong==0){
                audioSource.Play();
            }
            charIndex++;
            yield return new WaitForSecondsRealtime(typingTime);
        }
    }

        private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            Debug.Log("Se puede iniciar un diálogo");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            Debug.Log("No se puede iniciar un diálogo");
        }
    }

}
