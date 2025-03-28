using System.Collections;
using UnityEngine;
using TMPro;

public class NPCDialogue : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject emptyObject;  // Nueva referencia al objeto empty
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

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        characterController = GetComponent<CharacterController>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.Space))
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

        if (characterController != null) characterController.enabled = false;  // Desactiva el CharacterController
        if (emptyObject != null) emptyObject.SetActive(true);  // Activa el objeto empty
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
        if (characterController != null) characterController.enabled = true;  // Reactiva el CharacterController

        if (boxCollider != null)
        {
            boxCollider.enabled = false;  // Desactiva el BoxCollider2D para evitar que vuelva a activarse
        }

        if (emptyObject != null) emptyObject.SetActive(false);  // Desactiva el objeto empty
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
