using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerDependPosition : MonoBehaviour
{
    public SpriteRenderer sprite;
    public bool isStatic = true;

    private void Start()
    {
        UpdateLayerOrder();
    }

    private void Update()
    {
        if (!isStatic)
        {
            UpdateLayerOrder();
        }
    }

    private void UpdateLayerOrder()
    {
        sprite.sortingOrder = -(int)(transform.position.y * 100);
    }
}
