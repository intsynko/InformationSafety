using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RunningAnimController : MonoBehaviour
{
    [SerializeField] private Animator doctorAnim;
    [SerializeField] private Move move;
    protected virtual Vector2 Direction { get { return move.Direction; }  }
    private static readonly int IsRunning = Animator.StringToHash("IsRunning");

    void Update()
    {
        if (Direction.magnitude > 0) doctorAnim.SetBool(IsRunning, true);
        else doctorAnim.SetBool(IsRunning, false);
    }
}
