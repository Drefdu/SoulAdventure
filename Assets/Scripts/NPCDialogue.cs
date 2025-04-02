using System.Collections;
using UnityEngine;
using TMPro;

public class NPCDialogue : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject emptyObject;  // Objeto vacío opcional
    [SerializeField] private AudioClip npcVoice;
    [SerializeField] private float typingTime = 0.05f;
    [SerializeField] private int timeChatSong = 3;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;

    private bool didDialogueStart;
    private bool isPlayerInRange;
    private int lineIndex;
    private AudioSource audioSource;
    private CharacterController characterController;
    private BoxCollider2D boxCollider;
    private Rigidbody2D playerRb;  // Referencia al Rigidbody2D del jugador

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        characterController = GetComponent<CharacterController>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.Space) || isPlayerInRange && Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            if (didDialogueStart)
            {
                if (dialogueText.text == dialogueLines[lineIndex])
                {
                    NextDialogueLine();
                }
                else
                {
                    StopAllCoroutines();
                    dialogueText.text = dialogueLines[lineIndex];
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !didDialogueStart)
        {
            isPlayerInRange = true;
            playerRb = collision.GetComponent<Rigidbody2D>(); // Obtener el Rigidbody2D del jugador
            StartDialogue();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    private void StartDialogue()
    {
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        lineIndex = 0;

        // Congelar el movimiento del jugador si tiene Rigidbody2D
        if (playerRb != null)
        {
            playerRb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        }

        if (characterController != null) characterController.enabled = false;  
        if (emptyObject != null) emptyObject.SetActive(true);  
        
        StartCoroutine(ShowLine());
    }

    private void NextDialogueLine()
    {
        lineIndex++;
        if (lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        didDialogueStart = false;
        dialoguePanel.SetActive(false);

        // Descongelar el movimiento del jugador
        if (playerRb != null)
        {
            playerRb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        if (characterController != null) characterController.enabled = true;
        if (boxCollider != null) boxCollider.enabled = false;  
        if (emptyObject != null) emptyObject.SetActive(false);  
    }

    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;
        int charIndex = 0;

        foreach (char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch;

            if (charIndex % timeChatSong == 0 && npcVoice != null && audioSource != null)
            {
                audioSource.PlayOneShot(npcVoice);
            }

            charIndex++;
            yield return new WaitForSeconds(typingTime);
        }
    }
}
