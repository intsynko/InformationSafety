using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerDependPosition : MonoBehaviour
{
    public SpriteRenderer sprite;

    private void Update()
    {
        sprite.sortingOrder = - (int) (transform.position.y * 100);
    }
}
