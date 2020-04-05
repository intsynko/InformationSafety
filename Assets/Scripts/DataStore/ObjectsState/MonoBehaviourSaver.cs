using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBehaviourSaver : StateSaver
{
    public MonoBehaviour ToSave;
    

    public override void Apply(string serializeObject)
    {
        MonoBehaviourState state = JsonUtility.FromJson<MonoBehaviourState>(serializeObject);
        ToSave.enabled = state.enabled;
    }

    public override string ToJson()
    {
        return JsonUtility.ToJson(new MonoBehaviourState()
        {
            enabled = ToSave.enabled
        });
    }
}

public struct MonoBehaviourState
{
    public bool enabled;
}
