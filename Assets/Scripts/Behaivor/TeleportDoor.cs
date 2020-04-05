using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Zenject;

[Serializable]
public class MyEvent : UnityEvent { }

public class TeleportDoor : MonoBehaviour
{
    [Inject] private MySceneController _mySceneController;
    public Transform SafePoint;
    public TriggerZone TriggerZone;
    public string NextSceneName;

    public MyEvent OnTeleport;


    private void Start()
    {
        TriggerZone.TriggerEnter += TriggerZone_TriggerEnter;
    }

    private void TriggerZone_TriggerEnter(GameObject target)
    {
        OnTeleport.Invoke();
        _mySceneController.LoadScene(NextSceneName, SafePoint.position);
        //SceneManager.LoadScene(NextSceneName);
    }
}
