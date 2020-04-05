using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickController : MonoBehaviour
{
    public MyEvent OnClick;
    //public TriggerZone TriggerZone;
    public Collider2D collider;
    public static bool GlobalEnabled = true;

    //private bool isPlayerInZone;

    //private void Start()
    //{
    //    TriggerZone.TriggerEnter += TriggerZone_TriggerEnter;
    //    TriggerZone.TriggerExit += TriggerZone_TriggerExit;
    //}

    //private void TriggerZone_TriggerExit(GameObject target)
    //{
    //    isPlayerInZone = false;
    //}

    //private void TriggerZone_TriggerEnter(GameObject target)
    //{
    //    isPlayerInZone = true;
    //}

    private void OnGUI()
    {
        if (GlobalEnabled)
            if (OnClick != null)
                if (Input.GetMouseButtonDown(0))
                    if (collider.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
                        OnClick.Invoke();
    }
}
