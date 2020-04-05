using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public abstract class StateSaver : MonoBehaviour
{
    [Inject] protected JSONSaver jSONSaver;
    [Inject] protected SaveManager saveManager;
    public string SpecificName;

    private void Start()
    {
        Debug.Log("StateSaver: Start");
        if (saveManager.IsObjectExists(SpecificName))
            Apply(Read());
    }

    public void Save()
    {
        Debug.Log("StateSaver: Save");
        saveManager.SaveObject(ToJson(), SpecificName);
    }

    public string Read()
    {
        Debug.Log("StateSaver: Read");
        return saveManager.LoadObject(SpecificName);
    }

    public abstract string ToJson();

    public abstract void Apply(string serializeObject);

}
