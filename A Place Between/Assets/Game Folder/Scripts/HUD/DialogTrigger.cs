using UnityEngine;
using TMPro;

public class DialogTrigger : MonoBehaviour
{
    private GameObject dialogBox;
    private TMP_Text dialogText;
    public string[] dialogLines;

    private int currentLineIndex = 0;
    private bool playerInRange = false;
    private bool dialogActive = false;

    private Canvas canvas;

    void Start()
    {
        GameObject dialogBox = GameObject.FindGameObjectWithTag("DialogBox");
        dialogText = FindObjectOfType<TMP_Text>();
        GameObject canvasObject = GameObject.FindGameObjectWithTag("Canvas");
        canvas = canvasObject.GetComponent<Canvas>();
        canvas.enabled = false;
    }

    void Update()
    {
        if (playerInRange && dialogActive && Input.GetKeyDown(KeyCode.E))
        {
            currentLineIndex++;

            if (currentLineIndex < dialogLines.Length)
            {
                dialogText.text = dialogLines[currentLineIndex];
            }
            else
            {
                canvas.enabled = false;
                dialogActive = false;
                currentLineIndex = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            canvas.enabled = true;
            dialogText.text = dialogLines[currentLineIndex];
            dialogActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            canvas.enabled = false;
            currentLineIndex = 0;
            dialogActive = false;
        }
    }
}
