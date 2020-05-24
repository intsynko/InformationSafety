using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using Zenject;

public  delegate void CutsceneStart (string name);

public class CutSceneController : MonoBehaviour
{
    private static event CutsceneStart CutSceneStartEvent;
    public UnityEvent DialogStartEvent;
    public UnityEvent DialogEndEvent;

    [Inject] private DialogueSystemEvents dialogueSystemEvents;

    public async static Task RunCutsceneStatic(string name)
    {
        while (CutSceneStartEvent == null)
            await Task.Yield();
        CutSceneStartEvent(name);
        Debug.Log($"вызываю событие запуска кат сцены {name}");
    }


    private void Start()
    {
        dialogueSystemEvents.conversationEvents.
            onConversationStart.AddListener((Transform a) => { DialogStart(); });
        dialogueSystemEvents.conversationEvents.
            onConversationEnd.AddListener((Transform a) => { DialofEnd(); });
        CutSceneStartEvent += RunCutscene;
    }

    private void OnDestroy()
    {
        CutSceneStartEvent -= RunCutscene;
    }

    private void DialogStart()
    {
        DialogStartEvent.Invoke();
    }


    private void DialofEnd()
    {
        DialogEndEvent.Invoke();
    }

    

    public void RunCutscene(string name)
    {
        Debug.Log($"метод запуска кат сцены {name}");
        transform.Find(name).GetComponent<PlayableDirector>().Play();
    }
}
