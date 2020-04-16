using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Zenject;

public abstract class AbstractInvertorItemPresenter : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    [SerializeField] protected Text text;
    [SerializeField] protected Image image;

    protected Transform draggingParent;
    protected Transform originalContainer;
    public IItem Item { get; private set; }

    
    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.parent = draggingParent;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public abstract void OnEndDrag(PointerEventData eventData);

    public void Render(IItem item)
    {
        text.text = item.Name;
        image.sprite = item.Sprite;
        this.Item = item;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Cliked");
    }
}
