using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Zenject;

public abstract class AbstractInvertorItemPresenter : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    [Inject] private MessageBox messageBox;
    [SerializeField] protected Text amount;
    [SerializeField] protected Text itemName;
    [SerializeField] protected Image image;

    protected Transform draggingParent;
    protected Transform originalContainer;
    protected BaseInvertory Invertory;

    public AssetItems AssetItem { get; private set; }

    protected void Init(BaseInvertory baseInvertory)
    {
        this.Invertory = baseInvertory;
    }

    public void Click()
    {
        Debug.Log("Click");
        if (AssetItem.ItemType is AssetItemWithInfo)
        {
            var a = AssetItem.ItemType as AssetItemWithInfo;
            messageBox.ShowObjectInfo(a.Name, a.Description, a.Content, a.Sprite);
        }
    }


    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        UnAttachFromParent();
        Invertory.RemoveItem(this);
    }

    public void UnAttachFromParent()
    {
        transform.parent = draggingParent;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        //FindAndSetInClosestPosition(originalContainer);
        originalContainer.parent.parent.GetComponent<BaseInvertory>().AddNewItem(this);
    }

    public void FindAndSetInClosestPosition(Transform container)
    {
        // определение ближайшей позиции внутри контейнера
        int closestIndex = 0;
        for (int i = 0; i < container.transform.childCount; i++)
        {
            if (Vector3.Distance(transform.position, container.GetChild(i).position) <
                Vector3.Distance(transform.position, container.GetChild(closestIndex).position))
                closestIndex = i;
        }
        // встаем в эту позицию
        transform.parent = container;
        transform.SetSiblingIndex(closestIndex);
    }

    public void Render(AssetItems items)
    {
        amount.text = items.Count.ToString();
        itemName.text = items.ItemType.Name;
        image.sprite = items.ItemType.Sprite;
        this.AssetItem = items;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Cliked");
    }
}
