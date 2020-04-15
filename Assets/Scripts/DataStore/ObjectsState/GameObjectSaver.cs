using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectSaver : StateSaver
{
    public GameObject ToSave;

    protected override string GetSpecificName()
    {
        return base.GetSpecificName() + "_gameObject";
    }

    protected override void Apply(string serializeObject)
    {
        GameObjectState state = JsonUtility.FromJson<GameObjectState>(serializeObject);
        // применить все сохраненные переменные
        ToSave.SetActive(state.enabled);
    }

    protected override string ToJson()
    {
        // сохранить все переменные
        return JsonUtility.ToJson(new GameObjectState()
        {
            enabled = ToSave.activeSelf
        });
    }
}

public struct GameObjectState
{
    public bool enabled;
}
