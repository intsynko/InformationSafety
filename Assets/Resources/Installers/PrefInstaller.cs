using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PrefInstaller : MonoInstaller
{
    public GameObject dialoSystemControlelerPref;

    public override void InstallBindings()
    {
        Container.BindInstance<DialogueSystemController>(dialoSystemControlelerPref.GetComponent<DialogueSystemController>());
        Container.BindInstance<DialogueSystemEvents>(dialoSystemControlelerPref.GetComponent<DialogueSystemEvents>());
    }
}
