using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public abstract class BaseInvertory : MonoBehaviour
{
    [SerializeField] public Transform baseContainer;

    public virtual void Render(List<AssetItem> assetItems)
    {
        RenderContaier(assetItems, baseContainer);
    }

    protected void RenderContaier(List<AssetItem> assetItems, Transform container)
    {
        foreach (Transform child in container)
            Destroy(child.gameObject);
        assetItems.ForEach(item =>
        {
            var obj = Instantiate(GetPresenter(), container);
            Initialisation(obj, item);
        });
    }

    public abstract void Initialisation(AbstractInvertorItemPresenter obj, IItem item);

    public abstract AbstractInvertorItemPresenter GetPresenter();
}
