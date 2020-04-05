using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public abstract class BaseInvertory : MonoBehaviour
{
    [SerializeField] public Transform baseContainer;

    public void Render(List<AssetItem> assetItems)
    {
        foreach (Transform child in baseContainer)
            Destroy(child.gameObject);
        assetItems.ForEach(item =>
        {
            var obj = Instantiate(GetPresenter(), baseContainer);
            Initialisation(obj, item);
        });
    }

    public abstract void Initialisation(AbstractInvertorItemPresenter obj, IItem item);

    public abstract AbstractInvertorItemPresenter GetPresenter();
}
