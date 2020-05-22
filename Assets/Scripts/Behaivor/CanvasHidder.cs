using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

/// <summary>
/// Класс, который прячет UI основного канваса на время диалога и возвращает после диалога
/// </summary>
public class CanvasHidder : MonoBehaviour
{
    [Inject] private DialogueSystemEvents dialogueSystemEvents;
    void Start()
    {
        dialogueSystemEvents.conversationEvents.
            onConversationStart.AddListener((Transform a) => { Off(); });

        dialogueSystemEvents.conversationEvents.
            onConversationEnd.AddListener((Transform a) => { On(); });
    }

    private void OnDestroy()
    {
        dialogueSystemEvents.conversationEvents.
            onConversationStart.RemoveAllListeners();
        dialogueSystemEvents.conversationEvents.
            onConversationEnd.RemoveAllListeners();
    }

    public void Off()
    {
        GetComponent<Canvas>().enabled = false;
    }

    public void On()
    {
        GetComponent<Canvas>().enabled = true;
    }
}
