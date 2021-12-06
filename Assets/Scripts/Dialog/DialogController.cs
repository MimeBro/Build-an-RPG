using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dialogText, nameText;

    [SerializeField] GameObject dialogBox;

    [SerializeField] string[] dialogSentences;
    [SerializeField] int currentSentence;

    public static DialogController instance;

    private bool dialogJustStarted;

    void Start()
    {
        instance = this;
        dialogText.text = dialogSentences[currentSentence];
    }

    void Update()
    {
        if (dialogBox.activeInHierarchy)
        {
            if (Input.GetButtonUp("Fire1"))
            {
                if (!dialogJustStarted)
                {
                    currentSentence++;
                    if (currentSentence >= dialogSentences.Length)
                    {
                        dialogBox.SetActive(false);
                        Player.instance.deactivatedMovement = false;
                    }
                    else
                    {
                        dialogText.text = dialogSentences[currentSentence];
                    }
                }
                else
                {
                    dialogJustStarted = false;
                }
            }
        }
    }

    public void ActivateDialog(string[] newSentencesToUse)
    {
        dialogSentences = newSentencesToUse;
        currentSentence = 0;

        dialogText.text = dialogSentences[currentSentence];
        dialogBox.SetActive(true);

        dialogJustStarted = true;
        Player.instance.deactivatedMovement = true;
    }

    public bool IsDialogBoxActive()
    {
        return dialogBox.activeInHierarchy;
    }
}
