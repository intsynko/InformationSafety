using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BoxInvertory : BaseInvertory
{
    [Inject] private SaveManager saveManager;
    [SerializeField] protected MultipleInvertoryItemPresenter presenter;
    [SerializeField] private Transform additionalContainer;
    public List<AssetItems> addtionalAssetItems;


    /// <summary>
    /// Отрисовать оба контейнера
    /// </summary>
    /// <param name="assetItems">То, что лежит в ящике</param>
    public override void Render(List<AssetItems> assetItems)
    {
        base.Render(assetItems); // рендерим коробку со всеми вещами коробки
        // в доп контейнере рендерим то, что есть и игрока
        addtionalAssetItems = GetPlayerItems();
        RenderContaier(addtionalAssetItems, additionalContainer); 
    }

    public override void Initialisation(AbstractInvertorItemPresenter obj, AssetItems assetItems)
    {
        var itemPresenter = (MultipleInvertoryItemPresenter)obj;
        itemPresenter.Init(this, transform.parent, baseContainer,  additionalContainer);
        itemPresenter.Render(assetItems);
    }

    public void RemoveItem(AbstractInvertorItemPresenter itemPresenter, Transform container)
    {
        // удаляем элемент из коллекции
        RemoveItem(itemPresenter, GetAssetItemListByContainer(container));
    }   

    public void AddNewItem(AbstractInvertorItemPresenter itemPresenter, Transform container)
    {
        // добавляем элемент в коллекции
        AddNewItem(itemPresenter, GetAssetItemListByContainer(container), container);
    }

    private List<AssetItems> GetAssetItemListByContainer(Transform container)
    {
        if (container == baseContainer)
            return baseAssetItems;
        else
            return addtionalAssetItems;
    }

    public override AbstractInvertorItemPresenter GetPresenter()
    {
        return presenter;
    }
    

    /// <summary>
    /// Собрать остатки в коробке
    /// </summary>
    /// <returns></returns>
    public List<AssetItems> CollectBoxRest()
    {
        return baseAssetItems;
        //return CollectRest(baseContainer);
    }
    /// <summary>
    /// Собрать остатки у игрока
    /// </summary>
    /// <returns></returns>
    public List<AssetItems> CollectPlayerRest()
    {
        return addtionalAssetItems;
        //return CollectRest(additionalContainer);
    }
    /// <summary>
    /// Собрать остатки в контейнере conrainer
    /// </summary>
    /// <param name="conrainer">контейнер для сбора остатков</param>
    /// <returns></returns>
    private List<AssetItems> CollectRest(Transform conrainer)
    {
        List<AssetItems> assetItems = new List<AssetItems>();
        for (int i = 0; i < conrainer.childCount; i++)
        {
            assetItems.Add((AssetItems)conrainer.GetChild(i).GetComponent<MultipleInvertoryItemPresenter>().AssetItem);
        }
        return assetItems;
    }
}
