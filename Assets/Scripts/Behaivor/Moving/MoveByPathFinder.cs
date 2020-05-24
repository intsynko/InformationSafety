using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MoveByPathFinder : Move
{
    [SerializeField] private MyAIPath myAIPath;
    [SerializeField] private AIDestinationSetter destinationSetter;
    public override Vector2 Direction {
        get {
            return myAIPath.desiredVelocity;
        }
    }

    public async void MoveToEntityE(Transform transform)
    {
        await MoveToEntity(transform);
    }

    public async Task MoveToEntity(Transform transform)
    {
        destinationSetter.target = transform;
        myAIPath.enabled = true;
        myAIPath.isComplited = false;
        boxCollider2D.enabled = false;
        await Task.Yield();
        while (!myAIPath.isComplited) await Task.Yield();
        myAIPath.enabled = false;
        boxCollider2D.enabled = true;
    }
}
