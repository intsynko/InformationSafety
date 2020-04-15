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
    public SpecificName SpecificName;

    private void Start()
    {
        Debug.Log("StateSaver: Start");
        if (saveManager.IsObjectExists(GetSpecificName()))
            Apply(Load());
        else
            FirstRun();
    }

    protected virtual void FirstRun()
    {

    }

    protected virtual string GetSpecificName()
    {
        return SpecificName.SpecificObjectName;
    }

    public void Save()
    {
        Debug.Log("StateSaver: Save");
        saveManager.SaveObject(ToJson(), GetSpecificName());
    }

    public string Load()
    {
        Debug.Log("StateSaver: Read");
        return saveManager.LoadObject(GetSpecificName());
    }

    protected abstract string ToJson();

    protected abstract void Apply(string serializeObject);

    

}
