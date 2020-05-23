using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Invertory : BaseInvertory
{
    [SerializeField] protected MonoInvertoryItemPresenter presenter;
    [SerializeField] private List<AssetItems> assetItems;
    //public bool IsOpened = false;
    

    public void Render()
    {
        Render(GetPlayerItems());
    }

    public override void Initialisation(AbstractInvertorItemPresenter obj, AssetItems assetItems)
    {
        var itemPresenter = (MonoInvertoryItemPresenter)obj;
        itemPresenter.Init(this, transform.parent);
        itemPresenter.Render(assetItems);
    }

    public override AbstractInvertorItemPresenter GetPresenter()
    {
        return presenter;
    }
}
