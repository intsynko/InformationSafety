using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invertory : BaseInvertory
{
    [SerializeField] protected MonoInvertoryItemPresenter presenter;
    [SerializeField] private List<AssetItems> assetItems;
    //public bool IsOpened = false;

    private void OnEnable()
    {
        Render(assetItems);
    }

    public override void Initialisation(AbstractInvertorItemPresenter obj, AssetItems assetItems)
    {
        var itemPresenter = (MonoInvertoryItemPresenter)obj;
        itemPresenter.Init(transform.parent);
        itemPresenter.Render(assetItems);
    }

    public override AbstractInvertorItemPresenter GetPresenter()
    {
        return presenter;
    }
}
