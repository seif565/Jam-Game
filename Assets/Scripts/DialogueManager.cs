using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] Conversation conversation;
    [SerializeField] InputActionMap actionMap;
    [SerializeField] TextMeshProUGUI dialogueText;

    private void Start()
    {
        dialogueText.SetText(conversation.conversationDialogue.ToString());
    }

    private void Update()
    {
        var keyboard = Keyboard.current;
        if(keyboard.digit1Key.wasPressedThisFrame)
        {
            ChooseAndDisplay(conversation.options[0]);
        }
    }


    public void ChooseAndDisplay(Conversation currentConv)
    {
        conversation = currentConv;
        dialogueText.SetText(conversation.conversationDialogue.ToString());
    }
}
