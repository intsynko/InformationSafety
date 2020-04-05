using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MultipleInvertoryItemPresenter : AbstractInvertorItemPresenter
{
    protected Transform additionContainer;

    public void Init(Transform draggingParent, Transform additionContainer)
    {
        this.draggingParent = draggingParent;
        this.originalContainer = transform.parent;
        this.additionContainer = additionContainer;
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        var self = transform.position;
        Transform targetContainer;
        if (Vector3.Distance(self, additionContainer.position) <
            Vector3.Distance(self, originalContainer.position))
            targetContainer = additionContainer;
        else
            targetContainer = originalContainer;

        int closestIndex = 0;
        for (int i = 0; i < targetContainer.transform.childCount; i++)
        {
            if (Vector3.Distance(transform.position, targetContainer.GetChild(i).position) <
                Vector3.Distance(transform.position, targetContainer.GetChild(closestIndex).position))
                closestIndex = i;
        }
        transform.parent = targetContainer;
        transform.SetSiblingIndex(closestIndex);
    }
}
