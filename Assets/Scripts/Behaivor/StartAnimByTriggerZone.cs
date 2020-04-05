using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAnimByTriggerZone : MonoBehaviour
{
    public TriggerZone TriggerZone;
    public Animation animation;
    public string OnEnter;
    public string OnContinue;
    public string OnExit;

    private void Start()
    {
        TriggerZone.TriggerEnter += TriggerZone_TriggerEnter;
    }

    private void TriggerZone_TriggerEnter(GameObject target)
    {
        StartCoroutine(animateThis());
    }

    private IEnumerator animateThis()
    {
        if (OnEnter!="") animation.Play(OnEnter);
        while (TriggerZone.isTriggered && this.enabled)
        {
            if (!animation.isPlaying)
                if (OnContinue != "")
                    animation.Play(OnContinue);
            yield return null;
        }
        if (OnExit != "") animation.Play(OnExit);
    }
}
