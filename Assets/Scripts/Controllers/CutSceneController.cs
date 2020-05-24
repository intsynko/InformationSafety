using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public  delegate void CutsceneStart (string name);

public class CutSceneController : MonoBehaviour
{
    private static event CutsceneStart CutSceneStartEvent;
    private bool sceneReady;

    public async static Task RunCutsceneStatic(string name)
    {
        while (CutSceneStartEvent == null)
            await Task.Yield();
        CutSceneStartEvent(name);
        Debug.Log($"вызываю событие запуска кат сцены {name}");
    }

    private void Start()
    {
        CutSceneStartEvent += RunCutscene;
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        sceneReady = true;
    }

    private void OnDestroy()
    {
        CutSceneStartEvent -= RunCutscene;
    }

    public void RunCutscene(string name)
    {
        Debug.Log($"метод запуска кат сцены {name}");
        transform.Find(name).GetComponent<PlayableDirector>().Play();
    }
}
