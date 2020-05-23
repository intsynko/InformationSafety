using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class MonoInvertoryItemPresenter : AbstractInvertorItemPresenter
{
    public void Init(Invertory invertory, Transform draggingParent)
    {
        base.Init(invertory);
        this.draggingParent = draggingParent;
        originalContainer = transform.parent;
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        // определение ближайшей позиции
        int closestIndex = 0;
        for (int i=0; i < originalContainer.transform.childCount; i++)
        {
            if (Vector3.Distance(transform.position, originalContainer.GetChild(i).position) <
                Vector3.Distance(transform.position, originalContainer.GetChild(closestIndex).position))
                closestIndex = i;
        }
        transform.parent = originalContainer;
        transform.SetSiblingIndex(closestIndex);
    }
}
