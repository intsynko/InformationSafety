using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invertory : BaseInvertory
{
    [SerializeField] protected MonoInvertoryItemPresenter presenter;
    [SerializeField] private List<AssetItem> assetItems;
    public bool IsOpened = false;

    private void OnEnable()
    {
        Render(assetItems);
    }

    public override void Initialisation(AbstractInvertorItemPresenter obj, IItem item)
    {
        var itemPresenter = (MonoInvertoryItemPresenter)obj;
        itemPresenter.Init(transform.parent);
        itemPresenter.Render(item);
    }

    public override AbstractInvertorItemPresenter GetPresenter()
    {
        return presenter;
    }
}
