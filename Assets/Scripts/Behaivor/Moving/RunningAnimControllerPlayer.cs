using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningAnimControllerPlayer : RunningAnimController
{
    [SerializeField] private Player player;
    protected override Vector2 Direction { get { return player.CurrentMove.Direction; } }
}
