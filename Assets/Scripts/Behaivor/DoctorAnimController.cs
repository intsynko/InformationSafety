using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DoctorAnimController : MonoBehaviour
{
    private Animator doctorAnim;
    [Inject] private JoysticController _joysticController;
    private Move move;
    private Vector2 Direction { get { return move.Direction; }  }
    private static readonly int IsRunning = Animator.StringToHash("IsRunning");

    void Start()
    {
        move = transform.parent.GetComponent<Move>();
        doctorAnim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Direction.magnitude > 0) doctorAnim.SetBool(IsRunning, true);
        else doctorAnim.SetBool(IsRunning, false);
    }
}
