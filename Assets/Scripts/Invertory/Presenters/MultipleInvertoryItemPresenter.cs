using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MultipleInvertoryItemPresenter : AbstractInvertorItemPresenter
{
    private Transform additionContainer;

    public void Init(BoxInvertory boxInvertory, Transform draggingParent, Transform baseContainer, Transform additionContainer)
    {
        base.Init(boxInvertory);
        this.draggingParent = draggingParent;
        this.originalContainer = baseContainer;
        this.additionContainer = additionContainer;
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        ((BoxInvertory)Invertory).RemoveItem(this, transform.parent);
        UnAttachFromParent();
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        // Опеределяем, к какому контейнеру будем крепиться
        var self = transform.position;
        Transform targetContainer;
        if (Vector3.Distance(self, additionContainer.position) < Vector3.Distance(self, originalContainer.position))
            targetContainer = additionContainer;
        else
            targetContainer = originalContainer;

        ((BoxInvertory)Invertory).AddNewItem(this, targetContainer);
    }
}
