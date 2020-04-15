using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBehaviourSaver : StateSaver
{
    public MonoBehaviour ToSave;

    protected override string GetSpecificName()
    {
        return base.GetSpecificName() + "_monoBehaivor";
    }

    protected override void Apply(string serializeObject)
    {
        MonoBehaviourState state = JsonUtility.FromJson<MonoBehaviourState>(serializeObject);
        ToSave.enabled = state.enabled;
    }

    protected override string ToJson()
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
