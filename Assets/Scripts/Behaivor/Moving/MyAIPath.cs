using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyAIPath : AIPath
{
    public bool isComplited;

    public override void OnTargetReached()
    {
        base.OnTargetReached();
        isComplited = true;
    }
}
